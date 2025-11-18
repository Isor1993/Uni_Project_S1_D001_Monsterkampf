/*****************************************************************************
* Project : Monsterkampf-Simulator (K1, S1, S4)
* File    : PlayerController.cs
* Date    : xx.xx.2025
* Author  : Eric Rosenberg
*
* Description :
* Player controller that receives input from KeyboardInputManager (IPlayerInput).
* Responsible only for choosing a skill based on UI-provided selection.
*
* History :
* xx.xx.2025 ER Created
******************************************************************************/
using S1_D001_Monsterkampf_Simulator_ER.Controllers.Input;
using S1_D001_Monsterkampf_Simulator_ER.Managers;
using S1_D001_Monsterkampf_Simulator_ER.Monsters;
using S1_D001_Monsterkampf_Simulator_ER.Skills;

namespace S1_D001_Monsterkampf_Simulator_ER.Controllers
{
    internal class PlayerController:ControllerBase
    {

        // === Dependencies ===       
        private readonly IPlayerInput _input;

        // === Fields ===

        public PlayerController(MonsterBase monster,DiagnosticsManager diagnostics,IPlayerInput input):
            base(monster,diagnostics)
        {
            _input= input ?? throw new ArgumentNullException(nameof(input));
        }

        public override SkillBase ChooseSkill()
        {
            List<SkillBase> options = BuildSkillList();

            string[] optionTexts = BuildOptionTexts(options);

            int index = _input.ChooseIndex(optionTexts);

            SkillBase chosen = options[index];

            _diagnostics.AddCheck($"{nameof(PlayerController)}.{nameof(ChooseSkill)}: {Monster.Race} PLAYER chose skill '{chosen.Name}'.");           

            return chosen;

        }

        private List<SkillBase> BuildSkillList()
        {
            List<SkillBase> list = Monster.SkillPackage.ActiveSkills
                .Where(skill => skill.IsReady)
                .ToList();
            list.Add(new BasicAttack(_diagnostics));

            _diagnostics.AddCheck($"{nameof(PlayerController)}.{nameof(BuildSkillList)}: Built {list.Count} skills for player.");
           
            return list;
        }

        private string[] BuildOptionTexts(List<SkillBase> options)
        {
            string[] texts =new string[options.Count];

            for (int i =0;i<options.Count; i++)
            {
                SkillBase skill = options[i];
                texts[i] = skill.Name;
            }
            _diagnostics.AddCheck($"{nameof(PlayerController)}.{nameof(BuildOptionTexts)}: Successfully built {texts.Length} option texts.");

            return texts;
        }
    }
}
