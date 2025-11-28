/*****************************************************************************
* Project : Monsterkampf-Simulator (K1, S1, S4)
* File    : MonsterFactory.cs
* Date    : 03.12.2025
* Author  : Eric Rosenberg
*
* Description :
*   Factory responsible for creating fully configured monster instances
*   based on race and level. Applies balancing data, resistances, passive
*   and active skills. Central point for monster initialization.
*
* Responsibilities :
*   - Create MonsterMeta & MonsterResistance via MonsterBalancing
*   - Assign skill packages (passive + active skills)
*   - Return final monster instance with all combat-ready data
*
* History :
*   03.12.2025 ER Created
******************************************************************************/

using S1_D001_Monsterkampf_Simulator_ER.Balancing;
using S1_D001_Monsterkampf_Simulator_ER.Managers;
using S1_D001_Monsterkampf_Simulator_ER.Monsters;
using S1_D001_Monsterkampf_Simulator_ER.Skills;
using S1_D001_Monsterkampf_Simulator_ER.Skills.Goblin;
using S1_D001_Monsterkampf_Simulator_ER.Skills.Orc;
using S1_D001_Monsterkampf_Simulator_ER.Skills.Slime;
using S1_D001_Monsterkampf_Simulator_ER.Skills.Troll;

namespace S1_D001_Monsterkampf_Simulator_ER.Factories
{
    internal class MonsterFactory
    {

        // === Dependencies ===
        private readonly DiagnosticsManager _diagnostics;
        private readonly MonsterBalancing _balancing;

        /// <summary>
        /// Creates a new MonsterFactory.
        /// </summary>
        /// <param name="diagnostics">Diagnostics manager for logging output.</param>
        /// <param name="balancing">Balancing object for meta, stats and resistances.</param>
        public MonsterFactory(DiagnosticsManager diagnostics, MonsterBalancing balancing)
        {
            _diagnostics = diagnostics;
            _balancing = balancing;

        }

        /// <summary>
        /// Creates a monster instance based on race and level.
        /// Applies meta, resistance values and assigns skills.
        /// </summary>
        /// <param name="race">Race of the monster to create.</param>
        /// <param name="level">Level the monster should have.</param>
        /// <returns>Fully configured MonsterBase instance.</returns>
        /// <exception cref="Exception">Thrown if race is not defined.</exception>
        public MonsterBase Create(RaceType race, int level)
        {
            MonsterMeta meta = _balancing.GetMeta(race, level);
            MonsterResistance resistance = _balancing.GetResistance(race);

            SkillPackage package = new SkillPackage(_diagnostics);

            switch (race)
            {
                case RaceType.Goblin:
                    AssignGoblinSkills(package);
                    _diagnostics.AddCheck($"{nameof(MonsterFactory)}.{nameof(Create)}: Successfully created {race}.");
                    return new Goblin(meta, resistance, level, package, _diagnostics);

                case RaceType.Slime:
                    AssignSlimeSkills(package);
                    _diagnostics.AddCheck($"{nameof(MonsterFactory)}.{nameof(Create)}: Successfully created {race}.");
                    return new Slime(meta, resistance, level, package, _diagnostics);

                case RaceType.Troll:
                    AssignTrollSkills(package);
                    _diagnostics.AddCheck($"{nameof(MonsterFactory)}.{nameof(Create)}: Successfully created  {race} .");
                    return new Troll(meta, resistance, level, package, _diagnostics);

                case RaceType.Orc:
                    AssignOrcSkills(package);
                    _diagnostics.AddCheck($"{nameof(MonsterFactory)}.{nameof(Create)}: Successfully created  {race} .");
                    return new Orc(meta, resistance, level, package, _diagnostics);

                default:
                    throw new Exception($"No monster class found for race: {race}.");
            }
        }

        /// <summary>
        /// Assigns passive and active skills for Goblin monsters.
        /// </summary>
        /// <param name="package">SkillPackage to fill.</param>
        private void AssignGoblinSkills(SkillPackage package)
        {
            package.SetPassiveSkill(new PassiveSkill_Greed(_diagnostics));

            package.AddActiveSkill(new BasicAttack(_diagnostics));
            package.AddActiveSkill(new PoisonDagger(_diagnostics));
            package.AddActiveSkill(new ThrowStone(_diagnostics));

            _diagnostics.AddCheck($"{nameof(MonsterFactory)}.{nameof(AssignGoblinSkills)}: Goblin skills assigned.");
        }

        /// <summary>
        /// Assigns passive and active skills for Slime monsters.
        /// </summary>
        /// <param name="package">SkillPackage to fill.</param>
        private void AssignSlimeSkills(SkillPackage package)
        {
            package.SetPassiveSkill(new PassiveSkill_Absorb(_diagnostics));

            package.AddActiveSkill(new BasicAttack(_diagnostics));
            package.AddActiveSkill(new Fireball(_diagnostics));
            package.AddActiveSkill(new Waterball(_diagnostics));

            _diagnostics.AddCheck($"{nameof(MonsterFactory)}.{nameof(AssignSlimeSkills)}: Slime skills assigned.");
        }

        /// <summary>
        /// Assigns passive and active skills for Troll monsters.
        /// </summary>
        /// <param name="package">SkillPackage to fill.</param>
        private void AssignTrollSkills(SkillPackage package)
        {
            package.SetPassiveSkill(new PassiveSkill_Regeneration(_diagnostics));

            package.AddActiveSkill(new BasicAttack(_diagnostics));
            package.AddActiveSkill(new PowerSmash(_diagnostics));

            _diagnostics.AddCheck($"{nameof(MonsterFactory)}.{nameof(AssignTrollSkills)}: Troll skills assigned.");
        }

        /// <summary>
        /// Assigns passive and active skills for Orc monsters.
        /// </summary>
        /// <param name="package">SkillPackage to fill.</param>
        private void AssignOrcSkills(SkillPackage package)
        {
            package.SetPassiveSkill(new PassiveSkill_Fear(_diagnostics));

            package.AddActiveSkill(new BasicAttack(_diagnostics));
            package.AddActiveSkill(new TribeScream(_diagnostics));

            _diagnostics.AddCheck($"{nameof(MonsterFactory)}.{nameof(AssignOrcSkills)}: Orc skills assigned.");
        }
    }
}