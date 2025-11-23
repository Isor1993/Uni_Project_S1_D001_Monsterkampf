using S1_D001_Monsterkampf_Simulator_ER.Balancing;
using S1_D001_Monsterkampf_Simulator_ER.Dependencies;
using S1_D001_Monsterkampf_Simulator_ER.Factories;
using S1_D001_Monsterkampf_Simulator_ER.Managers;
using S1_D001_Monsterkampf_Simulator_ER.Monsters;
using S1_D001_Monsterkampf_Simulator_ER.Skills;
using S1_D001_Monsterkampf_Simulator_ER.Systems.StatusEffects;


namespace S1_D001_Monsterkampf_Simulator_ER
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            DiagnosticsManager diagnostics=new DiagnosticsManager();
            SymbolManager symbol=new SymbolManager();
            UIManager ui = new UIManager(symbol,diagnostics);
            Console.WriteLine($"Width: {Console.WindowWidth},  Height: {Console.WindowHeight}");
           // ui.PrintOutlineLayout();
           // ui.PrintMonsterInfoBoxPlayer();
           // ui.PrintMonsterInfoBoxEnemy();
            //ui.PrintMessageBoxLayout();
            //ui.PrintSkillBoxLayout();
            //ui.PrintSlimeP(30,10);
            //ui.PrintSlimeE(73,10);
            MonsterBalancing balancing = new MonsterBalancing(diagnostics);
            MonsterFactory factory = new MonsterFactory(diagnostics,balancing);
            MonsterBase Player = factory.Create(RaceType.Slime, 3);
            MonsterBase Enemy = factory.Create(RaceType.Orc, 3);
            //Player.TakeDamage(40);
            //Enemy.TakeDamage(33);
            

            //ui.UpdateMonsterBoxPlayer(Player);
            //ui.UpdateMonsterBoxEnemy(Enemy);
            //ui.UpdateSkillBox(Player.SkillPackage,0,);
            SkillBase chosen = Player.SkillPackage.ActiveSkills[0];
            //ui.UpdateMessageBoxForChooseSkill(chosen);
            //ui.UpdateMessageBoxForAttack(Player,Enemy,chosen,40,new PoisonEffect(3,2.5f,diagnostics));
            //ui.UpdateMessageBoxForTakeDamage(Player, Enemy, chosen, 40, new PoisonEffect(3, 2.5f, diagnostics));
             //ui.PrintMonsterInfoBoxLayoutPlayer();
             //ui.PrintMonsterInfoBoxLayoutEnemy();
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
