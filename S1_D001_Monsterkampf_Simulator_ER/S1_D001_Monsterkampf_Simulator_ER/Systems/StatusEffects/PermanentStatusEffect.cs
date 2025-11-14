using S1_D001_Monsterkampf_Simulator_ER.Managers;


namespace S1_D001_Monsterkampf_Simulator_ER.Systems.StatusEffects
{
    internal abstract class PermanentStatusEffect : StatusEffectBase
    {
        
        protected PermanentStatusEffect(string name,DiagnosticsManager diagnostics)
            : base(name, int.MaxValue, diagnostics ) // unendlich
        {

        }

        public override void Tick()
        {

           _diagnostics.AddCheck($"{nameof(PermanentStatusEffect)}.{nameof(Tick)}: Permanent status does not tick.");

        }
    }
}