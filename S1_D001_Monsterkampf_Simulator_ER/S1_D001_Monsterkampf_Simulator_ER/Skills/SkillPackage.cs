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
using S1_D001_Monsterkampf_Simulator_ER.Skills.Goblin;

namespace S1_D001_Monsterkampf_Simulator_ER.Skills
{
    internal class SkillPackage
    {
        private readonly DiagnosticsManager _diagnostics;
        public SkillBase PassiveSkill { get; private set; }


        public List<SkillBase> ActiveSkills { get; private set; }

        public List<PassiveSkill_Greed> EventPassives { get; } = new();

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
        public IEnumerable<SkillBase> AllSkills
        {
            get
            {
                if (PassiveSkill != null)
                {
                    yield return PassiveSkill;
                }

                foreach (SkillBase skill in ActiveSkills)
                {
                    yield return skill;
                }
            }
        }
        public void AddEventPassive(SkillBase passive)
        {
            if (passive is PassiveSkill_Greed greed)
            {
                EventPassives.Add(greed);
                _diagnostics.AddCheck($"{nameof(SkillPackage)}.{nameof(AddEventPassive)}: Added event passive {greed.Name}");
            }
            else
            {
                _diagnostics.AddError($"{nameof(SkillPackage)}.{nameof(AddEventPassive)}: Tried to add non-event passive skill {passive.Name}");
            }
        }
    }
}