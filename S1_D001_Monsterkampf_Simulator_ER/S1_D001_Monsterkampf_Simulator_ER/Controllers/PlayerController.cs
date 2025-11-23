/*****************************************************************************
* Project : Monsterkampf-Simulator (K1, S1, S4)
* File    : PlayerController.cs
* Date    : xx.xx.2025
* Author  : Eric Rosenberg
*
* Description :
* Handles all player skill selection via pointer movement.
* - Pointer movement (up/down)
* - Live UI updates (SkillBox + MessageBox)
* - Confirming a selected skill
*
* History :
* xx.xx.2025 ER Created
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
        private const int MaxSkillsShown = 4;

        private bool _skillConfirmed = false;

        public PlayerController(MonsterBase monster, DiagnosticsManager diagnostics, UIManager ui, IPlayerInput input)
            : base(monster, diagnostics)
        {
            _ui = ui ?? throw new ArgumentNullException(nameof(ui));
            _input = input ?? throw new ArgumentNullException(nameof(input));
        }

        /// <summary>
        /// SKILL CHOICE (MAIN ENTRY POINT)
        /// </summary>
        /// <returns></returns>
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
        /// POINTER MOVEMENT
        /// </summary>
        /// <param name="direction"></param>
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
        /// UI REFRESH
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