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

using S1_D001_Monsterkampf_Simulator_ER.Monsters;


namespace S1_D001_Monsterkampf_Simulator_ER.Systems.StatusEffects
{
    internal class PoisonEffect : StatusEffectBase
    {
        private readonly float _damagePercent;

        public PoisonEffect(int duration, float damagePercent)
            : base("Poison", duration)
        {
            _damagePercent = damagePercent;
        }

        public override void ApplyEffect(MonsterBase target)
        {
            //TODO max hp or current hp ??
            float damage = target.Meta.HP * _damagePercent;
            target.TakeDamage(damage);
        }
    }
}