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
using S1_D001_Monsterkampf_Simulator_ER.Systems.StatusEffects;

namespace S1_D001_Monsterkampf_Simulator_ER.Skills.Goblin
{
    internal class PosionDagger : SkillBase
    {

        // === Fields ===


        public PosionDagger(DiagnosticsManager diagnostics) : base(
            "Posion Dagger",
            "Attack with a posion dagger and inflict 10% poision damage for 2 rounds.",
            SkillType.Aktive,
            DamageType.Poision,
            0.1f,
            diagnostics)
        {
        }

        public override void Apply(MonsterBase user, MonsterBase target)
        {
            target.AddStatusEffect(new PoisonEffect(2, 0.1f, _diagnostics));
            _diagnostics.AddCheck($"{nameof(PosionDagger)}.{nameof(Apply)}: Applied poisoneffect");
        }

        public override float CalculateDamage(MonsterBase attacker, MonsterBase target)
        {
            float rawDamage = attacker.Meta.AP * Power;

            float resistance = target.Resistance.Poision;

            float finalDamage = rawDamage * (1f - resistance);

            finalDamage = Math.Max(1, finalDamage);

            _diagnostics.AddCheck($"{nameof(PosionDagger)}.{nameof(CalculateDamage)}: Calculated damage is {finalDamage}");

            return Math.Max(1, finalDamage);
        }
    }
}
