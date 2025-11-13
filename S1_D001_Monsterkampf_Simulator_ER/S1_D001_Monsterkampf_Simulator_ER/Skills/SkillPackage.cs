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

namespace S1_D001_Monsterkampf_Simulator_ER.Skills
{
    internal class SkillPackage
    {

        public SkillBase PassiveSkill { get; private set; }


        public List<SkillBase> ActiveSkills { get; private set; }


        public SkillPackage()
        {
            ActiveSkills = new List<SkillBase>();
        }


        public void SetPassiveSkill(SkillBase passive)
        {
            PassiveSkill = passive;
        }


        public void AddActiveSkill(SkillBase active)
        {
            ActiveSkills.Add(active);
        }
    }
}