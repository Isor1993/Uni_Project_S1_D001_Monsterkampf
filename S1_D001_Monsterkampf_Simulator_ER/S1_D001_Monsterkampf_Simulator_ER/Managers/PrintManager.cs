using S1_D001_Monsterkampf_Simulator_ER.Dependencies;
using Semester1_D001_Escape_Room_Rosenberg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S1_D001_Monsterkampf_Simulator_ER.Managers
{
    internal class PrintManager
    {

        private readonly DiagnosticsManager diagnostics;

        public PrintManager( DiagnosticsManager diagnostics)
        {
            this.diagnostics = diagnostics;
        }



        public void Line(string message)
        {
            Console.WriteLine($"{message}");
        }

    }
}
