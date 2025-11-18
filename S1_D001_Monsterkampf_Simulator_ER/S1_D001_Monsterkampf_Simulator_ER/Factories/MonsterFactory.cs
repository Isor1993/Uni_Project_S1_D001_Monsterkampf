/*****************************************************************************
* Project : Monsterkampf-Simulator (K1, S1, S4)
* File    : MonsterFactory.cs
* Date    : xx.xx.2025
* Author  : Eric Rosenberg
*
* Description :
* Erzeugt vollständig konfigurierte Monster-Instanzen basierend auf Race & Level.
* Nutzt Balancing-Werte und fügt Skills & Resistenzen hinzu.
*
* History :
* xx.xx.2025 ER Created
******************************************************************************/
using S1_D001_Monsterkampf_Simulator_ER.Balancing;
using S1_D001_Monsterkampf_Simulator_ER.Managers;
using S1_D001_Monsterkampf_Simulator_ER.Monsters;
using S1_D001_Monsterkampf_Simulator_ER.Skills;
using S1_D001_Monsterkampf_Simulator_ER.Skills.Goblin;
using S1_D001_Monsterkampf_Simulator_ER.Skills.Orc;
using S1_D001_Monsterkampf_Simulator_ER.Skills.Slime;
using S1_D001_Monsterkampf_Simulator_ER.Skills.Troll;
using S1_D001_Monsterkampf_Simulator_ER.Systems.StatusEffects;

namespace S1_D001_Monsterkampf_Simulator_ER.Factories
{
    internal class MonsterFactory
    {

        // === Dependencies ===
        private readonly DiagnosticsManager _diagnostics;
        private readonly MonsterBalancing _balancing;

        // === Fields ===


        public MonsterFactory(DiagnosticsManager diagnostics, MonsterBalancing balancing)
        {
            _diagnostics = diagnostics;
            _balancing = balancing;

        }

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

        //TODO Soll man hier viell Basickattack mit hinzufügen oder viel in monsterbase?
        private void AssignGoblinSkills(SkillPackage package)
        {
            package.AddEventPassive(new PassiveSkill_Greed(_diagnostics));

            package.AddActiveSkill(new PoisonDagger(_diagnostics));
            package.AddActiveSkill(new ThrowStone(_diagnostics));
           

            _diagnostics.AddCheck($"{nameof(MonsterFactory)}.{nameof(AssignGoblinSkills)}: Goblin skills assigned.");

        }
        private void AssignSlimeSkills(SkillPackage package)
        {
            package.SetPassiveSkill(new PassiveSkill_Absorb(_diagnostics));

            package.AddActiveSkill(new Fireball(_diagnostics));
            package.AddActiveSkill(new Waterball(_diagnostics));

            _diagnostics.AddCheck($"{nameof(MonsterFactory)}.{nameof(AssignSlimeSkills)}: Slime skills assigned.");
        }
        private void AssignTrollSkills(SkillPackage package)
        {
            package.SetPassiveSkill(new PassiveSkill_Regeneration(_diagnostics));

            package.AddActiveSkill(new PowerSmash(_diagnostics));

            _diagnostics.AddCheck($"{nameof(MonsterFactory)}.{nameof(AssignTrollSkills)}: Troll skills assigned.");
        }
        private void AssignOrcSkills(SkillPackage package)
        {
            package.SetPassiveSkill(new PassiveSkill_Fear(_diagnostics));

            package.AddActiveSkill(new TribeScream(_diagnostics));

            _diagnostics.AddCheck($"{nameof(MonsterFactory)}.{nameof(AssignOrcSkills)}: Orc skills assigned.");
        }
    }
}
