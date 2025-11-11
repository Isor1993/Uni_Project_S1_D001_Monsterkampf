using S1_D001_Monsterkampf_Simulator_ER.Dependencies;
using S1_D001_Monsterkampf_Simulator_ER.Managers;
using Semester1_D001_Escape_Room_Rosenberg;

namespace S1_D001_Monsterkampf_Simulator_ER
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Monsterkampf-Simulator 🧩";
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            DiagnosticsManager diagnostic=new DiagnosticsManager();
            PrintManager print = new PrintManager(diagnostic);
            BattleManager battle = new BattleManager();
            RulesManager rules = new RulesManager();
            UIManager ui = new UIManager();
            InputManager input = new InputManager();
            RandomManager random = new RandomManager();


            GameManager gameManager = new GameManager(new GameDependencies (battle, rules, ui, input, random, diagnostic,print));
            gameManager.RunGame();

            Console.WriteLine("\nDrücke eine beliebige Taste, um das Programm zu beenden...");
            Console.ReadKey();
        }
    }
}
