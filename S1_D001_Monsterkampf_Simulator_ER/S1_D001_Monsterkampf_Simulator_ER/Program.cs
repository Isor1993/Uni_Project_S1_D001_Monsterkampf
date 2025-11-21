using S1_D001_Monsterkampf_Simulator_ER.Balancing;
using S1_D001_Monsterkampf_Simulator_ER.Dependencies;
using S1_D001_Monsterkampf_Simulator_ER.Factories;
using S1_D001_Monsterkampf_Simulator_ER.Managers;
using S1_D001_Monsterkampf_Simulator_ER.Monsters;


namespace S1_D001_Monsterkampf_Simulator_ER
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DiagnosticsManager diagnostics=new DiagnosticsManager();
            SymbolManager symbol=new SymbolManager();
            UIManager ui = new UIManager(symbol);
            Console.WriteLine($"Width: {Console.WindowWidth},  Height: {Console.WindowHeight}");
            ui.PrintOutlineLayout(29, 100);
            ui.PrintMonsterInfoBoxPlayer();
            ui.PrintMonsterInfoBoxEnemy();
            ui.PrintMessageBoxLayout(20,23);
            ui.PrintSkillBoxLayout(0, 23);
            ui.PrintSlimeP(30,10);
            ui.PrintSlimeE(73,10);
            MonsterBalancing balancing = new MonsterBalancing(diagnostics);
            MonsterFactory factory = new MonsterFactory(diagnostics,balancing);
            MonsterBase Player = factory.Create(RaceType.Slime, 3);
            MonsterBase Enemy = factory.Create(RaceType.Orc, 3);
            Player.TakeDamage(40);
            Enemy.TakeDamage(33);


            ui.UpdateMonsterBox(Player, 20, 3);
            ui.UpdateMonsterBox(Enemy,63,3);
            // ui.PrintMonsterInfoLayout(6,34,(20,3));
            // ui.PrintMonsterInfoLayout(6, 34, (63, 3));
            Console.ReadKey(true);
                        /*
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
            */
        }
    }
}
