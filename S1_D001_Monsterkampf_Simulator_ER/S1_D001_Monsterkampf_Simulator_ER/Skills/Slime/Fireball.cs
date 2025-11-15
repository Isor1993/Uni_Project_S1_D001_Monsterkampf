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
using System.Diagnostics.Tracing;

namespace S1_D001_Monsterkampf_Simulator_ER.Skills.Slime
{
    internal class Fireball : SkillBase
    {
        // === Dependencies ===

        // === Fields ===
        


        public Fireball(DiagnosticsManager diagnostics)
            : base(
                 "Fireball",
                 "Shoots a fireball which deals 50% more damage.",
                 SkillType.Aktive,
                 DamageType.Fire,
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

            float resistance = target.Resistance.Fire;

            float finalDamage = afterDefense * (1f - resistance);
            
            finalDamage = Math.Max(1, finalDamage);

            _diagnostics.AddCheck($"{nameof(Fireball)}.{nameof(CalculateDamage)}: Calculated fireball damage {finalDamage}.");

            return finalDamage;
        }
    }
}
