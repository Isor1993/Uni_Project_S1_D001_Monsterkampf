namespace S1_D001_Monsterkampf_Simulator_ER.Managers
{
    internal class PrintManager
    {
        private readonly DiagnosticsManager diagnostics;

        public PrintManager(DiagnosticsManager diagnostics)
        {
            this.diagnostics = diagnostics;
        }

        public void Line(string message)
        {
            Console.WriteLine($"{message}");
        }
    }
}