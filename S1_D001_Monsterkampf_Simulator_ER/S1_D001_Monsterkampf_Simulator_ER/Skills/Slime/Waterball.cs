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
using S1_D001_Monsterkampf_Simulator_ER.Monsters;


namespace S1_D001_Monsterkampf_Simulator_ER.Skills.Slime
{
    internal class Waterball:SkillBase
    {
        // === Dependencies ===

        // === Fields ===



        public Waterball(DiagnosticsManager diagnostics)
            : base(
                 "Waterball",
                 "Shoots a waterball which deals 50% more damage.",
                 SkillType.Aktive,
                 DamageType.Water,
                 1.5f,
                 diagnostics)
        {
            Cooldown = 2;
        }

        public override float CalculateDamage(MonsterBase attacker, MonsterBase target)
        {
            float rawDamage = attacker.Meta.AP * Power;

            float afterDefense = rawDamage - target.Meta.DP;
            afterDefense = Math.Max(1, afterDefense);

            float resistance = target.Resistance.Water;

            float finalDamage = afterDefense * (1f - resistance);

            finalDamage = Math.Max(1, finalDamage);

            _diagnostics.AddCheck($"{nameof(Waterball)}.{nameof(CalculateDamage)}: Calculated waterball damage {finalDamage}.");

            return finalDamage;
        }
    }
}
