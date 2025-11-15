/*****************************************************************************
* Project : Monsterkampf-Simulator (K1, S1, S4)
* File    : 
* Date    : xx.xx.2025
* Author  : Eric Rosenberg
*
* Description :
* *
* History :
* xx.xx.2025 ER Created
******************************************************************************/

using S1_D001_Monsterkampf_Simulator_ER.Managers;

namespace S1_D001_Monsterkampf_Simulator_ER.Skills
{
    internal class SkillPackage
    {
        private readonly DiagnosticsManager _diagnostics;
        public SkillBase PassiveSkill { get; private set; }


        public List<SkillBase> ActiveSkills { get; private set; }


        public SkillPackage(DiagnosticsManager diagnosticsManager)
        {
            _diagnostics = diagnosticsManager;
            ActiveSkills = new List<SkillBase>();
        }


        public void SetPassiveSkill(SkillBase passive)
        {
            PassiveSkill = passive;
            _diagnostics.AddCheck($"{nameof(SkillPackage)}.{nameof(SetPassiveSkill)}: Set skill as passive {passive}");
        }


        public void AddActiveSkill(SkillBase active)
        {
            ActiveSkills.Add(active);
            _diagnostics.AddCheck($"{nameof(SkillPackage)}.{nameof(AddActiveSkill)}: Added skill {active.Name} in 'ActiveSkills'");
        }
    }
}