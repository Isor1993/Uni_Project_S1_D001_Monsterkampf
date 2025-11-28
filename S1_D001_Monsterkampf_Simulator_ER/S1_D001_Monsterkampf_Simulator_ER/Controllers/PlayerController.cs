/*****************************************************************************
* Project : Monsterkampf-Simulator (K1, S1, S4)
* File    : PlayerController.cs
* Date    : xx.xx.2025
* Author  : Eric Rosenberg
*
* Description :
*   Manages all player-driven combat decisions.
*   - Handles pointer-based navigation through available skills.
*   - Updates the UI (SkillBox + MessageBox) live during navigation.
*   - Validates cooldown restrictions.
*   - Confirms and returns the selected skill.
*
* History :
*   xx.xx.2025 ER Created
******************************************************************************/

using S1_D001_Monsterkampf_Simulator_ER.Managers;
using S1_D001_Monsterkampf_Simulator_ER.Monsters;
using S1_D001_Monsterkampf_Simulator_ER.Skills;

namespace S1_D001_Monsterkampf_Simulator_ER.Controllers
{
    internal class PlayerController : ControllerBase
    {
        // === Dependencies ===
        private readonly UIManager _ui;
        private readonly IPlayerInput _input;

        // === Fields ===
        private int _pointerIndex = 0;
        private bool _skillConfirmed = false;

        /// <summary>
        /// Creates a PlayerController that allows the player to choose skills
        /// via keyboard input and automatically updates all relevant UI boxes.
        /// </summary>
        /// <param name="monster">The player's monster instance.</param>
        /// <param name="diagnostics">DiagnosticsManager for logging and checks.</param>
        /// <param name="ui">UIManager responsible for updating SkillBox & MessageBox.</param>
        /// <param name="input">Player input reader (W/S/Enter).</param>
        /// <exception cref="ArgumentNullException">Thrown if UI or input is null.</exception>
        public PlayerController(MonsterBase monster, DiagnosticsManager diagnostics, UIManager ui, IPlayerInput input)
            : base(monster, diagnostics)
        {
            _ui = ui ?? throw new ArgumentNullException(nameof(ui));
            _input = input ?? throw new ArgumentNullException(nameof(input));
        }

        /// <summary>
        /// Handles the complete process of selecting a skill:
        /// - Shows all skills in the UI
        /// - Allows pointer movement
        /// - Confirms the chosen skill
        /// - Prevents choosing skills that are on cooldown
        /// </summary>
        /// <returns>The chosen SkillBase.</returns>
        public override SkillBase ChooseSkill()
        {
            _skillConfirmed = false;

            RefreshSkillUI();
            _diagnostics.AddCheck($"{nameof(PlayerController)}.{nameof(ChooseSkill)}: Starting skill selection for {Monster.Race}.");

            while (!_skillConfirmed)
            {
                PlayerCommand cmd = _input.ReadCommand();

                switch (cmd)
                {
                    case PlayerCommand.MoveUp:
                        MovePointer(-1);
                        _diagnostics.AddCheck($"{nameof(PlayerController)}.{nameof(ChooseSkill)}: Input → MoveUp.");
                        break;

                    case PlayerCommand.MoveDown:
                        MovePointer(+1);
                        _diagnostics.AddCheck($"{nameof(PlayerController)}.{nameof(ChooseSkill)}: Input → MoveDown.");
                        break;

                    case PlayerCommand.Confirm:
                        _skillConfirmed = true;
                        _diagnostics.AddCheck($"{nameof(PlayerController)}.{nameof(ChooseSkill)}: Input → Confirm.");
                        break;
                }
            }

            SkillBase chosen = Monster.SkillPackage.ActiveSkills[_pointerIndex];
            _pointerIndex = 0;
            if (chosen.CurrentCooldown > 0)
            {

                _diagnostics.AddCheck($"{nameof(PlayerController)}: Skill '{chosen.Name}' is on cooldown. Cannot select.");
                return ChooseSkill(); // Nochmal öffnen
            }
            _diagnostics.AddCheck($"{nameof(PlayerController)}.{nameof(ChooseSkill)}: Selected '{chosen.Name}' at index {_pointerIndex}.");

            return chosen;
        }

        /// <summary>
        /// Moves the pointer through the skill list and skips skills
        /// that currently cannot be selected (cooldown > 0).
        /// </summary>
        /// <param name="direction">-1 = up, +1 = down.</param>
        private void MovePointer(int direction)
        {
            SkillPackage pack = Monster.SkillPackage;

            int next = _pointerIndex + direction;

            // Suche nach dem nächsten skillbaren Skill
            while (next >= 0 &&
                   next < pack.ActiveSkills.Count &&
                   pack.ActiveSkills[next].CurrentCooldown > 0)
            {
                next += direction;
            }

            // Wenn gültig → setzen
            if (next >= 0 && next < pack.ActiveSkills.Count)
            {
                _pointerIndex = next;
            }


            RefreshSkillUI();
            _diagnostics.AddCheck($"{nameof(PlayerController)}.{nameof(MovePointer)}: Pointer  → {_pointerIndex}.");
        }

        /// <summary>
        /// Updates the SkillBox and MessageBox to reflect the current pointer
        /// position and the currently highlighted skill.
        /// </summary>
        private void RefreshSkillUI()
        {
            SkillPackage pack = Monster.SkillPackage;

            if (pack.ActiveSkills.Count == 0)
            {
                _diagnostics.AddWarning($"{nameof(PlayerController)}.{nameof(RefreshSkillUI)}: No skills available.");
                return;
            }

            SkillBase current = pack.ActiveSkills[_pointerIndex];

            _ui.UpdateSkillBox(pack, _pointerIndex);
            _ui.UpdateMessageBoxForChooseSkill(current);
            _diagnostics.AddCheck($"{nameof(PlayerController)}.{nameof(RefreshSkillUI)}: Refreshed skill UI.");
        }
    }
}