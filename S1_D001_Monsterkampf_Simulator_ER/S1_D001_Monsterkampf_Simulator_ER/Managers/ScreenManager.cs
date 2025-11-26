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
using S1_D001_Monsterkampf_Simulator_ER.Controllers;
using S1_D001_Monsterkampf_Simulator_ER.Monsters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S1_D001_Monsterkampf_Simulator_ER.Managers
{
    internal class ScreenManager
    {
        // === Dependencies ===
        private readonly SymbolManager _symbol;        
        private readonly PlayerController _playerController;

        // === Fields ===


        const int HeaderPositionX = 0;
        const int HeaderPositionY = 0;
        const int StartOffsetX = 23;
        const int StartOffsetY = 0;
        const int StartSpriteOffsetX = 16;
        const int StartSpriteOffsetY = 14;
        const int TutorialOffsetX = 39;
        const int EndOffsetX = 39;
        private string TutorialLine_0 = " Welcome to my monster simulation fighter project!";
        private string TutorialLine_1 = " [W] or [↑]    = Move up in the selection to choose options/skills";
        private string TutorialLine_2 = " [S] or [↓]    = Move down in the selection to choose options/skills";
        private string TutorialLine_3 = " [ENTER]       = Confirm a selection";
        private string TutorialLine_4 = " Monsters      : There are currently 4 monsters";
        private string TutorialLine_5 = " Passive skills: Every monster has one which is always active at the start";
        private string TutorialLine_6 = " Active skills : Basic or special attacks with effects and cooldowns";
        private string TutorialLine_7 = " Cooldowns     : Cooldowns are measured in rounds";
        private string TutorialLine_8 = " Stats         : HP    = Health";
        private string TutorialLine_9 = "                 AP    = Attack Power";
        private string TutorialLine_10 = "                 DP    = Defense";
        private string TutorialLine_11 = "                 Speed = Decides who will start the round";
        private string TutorialLine_12 = " I wish you a lot of fun! After each won fight, you get a new enemy.";
        private string TutorialLine_13 = " You also gain a level-up and a stat point to make your monster stronger.";

        private string EndLine_0 =  " I hope you had a little bit of fun! :)  ";
        private string EndLine_1 =  " This small project was created during my studies at ";
        private string EndLine_2 =  " the university.In the ReadMe file, you’ll find information";
        private string EndLine_3 =  " about the Project. Your Final Stats below ";
        private string EndLine_4 =  " Final Stats   ";
        private string EndLine_7 =  " Contact info:  ";
        private string EndLine_8 =  " Instagram: https://www.instagram.com/isor_gamedev  ";
        private string EndLine_9 =  " GitHub: https://github.com/Isor1993 ";
        private string EndLine_10 = " E-mail: IsorDev@email.de    ";







        private readonly string[] HeaderStartScreen_1 =
        {

            @" __  __                 _            ",
            @"|  \/  |               | |           ",
            @"| \  / | ___  _ __  ___| |_ ___ _ __ ",
            @"| |\/| |/ _ \| '_ \/ __| __/ _ \ '__|",
            @"| |  | | (_) | | | \__ \ ||  __/ |   ",
            @"|_|  |_|\___/|_| |_|___/\__\___|_|   "
        };
        private readonly string[] HeaderStartScreen_2 =
        {

            @" ____        _   _   _           ",
            @"|  _ \      | | | | | |          ",
            @"| |_) | __ _| |_| |_| | ___ _ __ ",
            @"|  _ < / _` | __| __| |/ _ \ '__|",
            @"| |_) | (_| | |_| |_| |  __/ |   ",
            @"|____/ \__,_|\__|\__|_|\___|_|   "
        };
        private readonly string[] HeaderTutorialScreen =
        {

            @" _______    _             _       _ ",
            @"|__   __|  | |           (_)     | |",
            @"   | |_   _| |_ ___  _ __ _  __ _| |",
            @"   | | | | | __/ _ \| '__| |/ _` | |",
            @"   | | |_| | || (_) | |  | | (_| | |",
            @"   |_|\__,_|\__\___/|_|  |_|\__,_|_|"
        };

        private readonly string[] HeaderEndScreen =
        {
            @"  _______ _             ______           _ ",
            @" |__   __| |           |  ____|         | |",
            @"    | |  | |__   ___   | |__   _ __   __| |",
            @"    | |  | '_ \ / _ \  |  __| | '_ \ / _` |",
            @"    | |  | | | |  __/  | |____| | | | (_| |",
            @"    |_|  |_| |_|\___|  |______|_| |_|\__,_|"
        };


        public ScreenManager(SymbolManager symbol, PlayerController playerController)
        {
            _symbol = symbol;
           
            _playerController = playerController;
        }




        public void PrintScreenStart()
        {
            PrintStartHeader();
            PrintGoblinP();
            PrintOrcE();
            PrintTrollP();
            PrintSlimeE();
        }
        public void PrintScreenTutorial()
        {
            PrintTutorialHeader();
            PrintTutorialFrame();
            PrintTutorialText();

        }
        public void PrintScreenEnd()
        {
            PrintEndFrame();
            PrintEndHeader();
            PrintEndText();

        }
        private void PrintEndText()
        {
            int posX = 23;
            int posY = 10;
            Console.SetCursorPosition(posX, posY);
            Console.WriteLine(EndLine_0);
            posY++;
            Console.SetCursorPosition(posX, posY);
            Console.WriteLine(EndLine_1);
            posY++;
            Console.SetCursorPosition(posX, posY);
            Console.WriteLine(EndLine_2);
            posY++;
            Console.SetCursorPosition(posX, posY);
            Console.WriteLine(EndLine_3);
            posY++;
            posY++;
            Console.SetCursorPosition(posX, posY);
            Console.WriteLine(EndLine_4);
            posY++;            
            Console.SetCursorPosition(posX, posY);
            Console.WriteLine($" Total Fights : {GameManager.totalFights}");
            posY++;
            Console.SetCursorPosition(posX, posY);
            Console.WriteLine($" Monster Lvl  : {_playerController.Monster.Level}");
            posY++;
            posY++;
            Console.SetCursorPosition(posX, posY);
            Console.WriteLine(EndLine_7);
            posY++;
            Console.SetCursorPosition(posX, posY);
            Console.WriteLine(EndLine_8);
            posY++;
            Console.SetCursorPosition(posX, posY);
            Console.WriteLine(EndLine_9);
            posY++;
            Console.SetCursorPosition(posX, posY);
            Console.WriteLine(EndLine_10);
            posY++;
           



        }
        private void PrintEndHeader()
        {
            int posX = HeaderPositionX + EndOffsetX;
            int posY = HeaderPositionY;
            for (int i = 0; i < HeaderEndScreen.Length; i++)
            {
                Console.SetCursorPosition(posX, posY);
                Console.Write(HeaderEndScreen[i]);
                posY++;
            }
        }
        private void PrintEndFrame()
        {
            int maxRow = 16;
            int maxColl = 76;
            int posX = 22;
            int posY = 8;
            for (int row = 0; row < maxRow; row++)
            {
                Console.SetCursorPosition(posX, posY);
                posY++;

                for (int col = 0; col < maxColl; col++)
                {
                    bool top = row == 0;
                    bool bottom = row == maxRow - 1;
                    bool left = col == 0;
                    bool right = col == maxColl - 1;

                    if (top && left) Console.Write(_symbol.WallCornerTopLeftSymbol);
                    else if (top && right) Console.Write(_symbol.WallCornerTopRightSymbol);
                    else if (bottom && left) Console.Write(_symbol.WallCornerBottomLeftSymbol);
                    else if (bottom && right) Console.Write(_symbol.WallCornerBottomRightSymbol);
                    else if (top || bottom) Console.Write(_symbol.WallHorizontalSymbol);
                    else if (left || right) Console.Write(_symbol.WallVerticalSymbol);
                    else Console.Write(" ");
                }
                Console.WriteLine();
            }
        }
        private void PrintTutorialText()
        {
            int posX = 23;
            int posY = 10;
            Console.SetCursorPosition(posX, posY);
            posY++;
            posY++;
            Console.WriteLine(TutorialLine_0);
            Console.SetCursorPosition(posX, posY);
            posY++;
            Console.WriteLine(TutorialLine_1);
            Console.SetCursorPosition(posX, posY);
            posY++;
            Console.WriteLine(TutorialLine_2);
            Console.SetCursorPosition(posX, posY);
            posY++;
            Console.WriteLine(TutorialLine_3);
            Console.SetCursorPosition(posX, posY);
            posY++;
            Console.WriteLine(TutorialLine_4);
            Console.SetCursorPosition(posX, posY);
            posY++;
            Console.WriteLine(TutorialLine_5);
            Console.SetCursorPosition(posX, posY);
            posY++;
            Console.WriteLine(TutorialLine_6);
            Console.SetCursorPosition(posX, posY);
            posY++;
            Console.WriteLine(TutorialLine_7);
            Console.SetCursorPosition(posX, posY);
            posY++;
            Console.WriteLine(TutorialLine_8);
            Console.SetCursorPosition(posX, posY);
            posY++;
            Console.WriteLine(TutorialLine_9);
            Console.SetCursorPosition(posX, posY);
            posY++;
            Console.WriteLine(TutorialLine_10);
            Console.SetCursorPosition(posX, posY);
            posY++;
            posY++;
            Console.WriteLine(TutorialLine_11);
            Console.SetCursorPosition(posX, posY);
            posY++;
            Console.WriteLine(TutorialLine_12);
            Console.SetCursorPosition(posX, posY);
            posY++;
            Console.WriteLine(TutorialLine_13);
            Console.SetCursorPosition(posX, posY);
        }
        private void PrintTutorialFrame()
        {
            int maxRow = 19;
            int maxColl = 76;
            int posX = 22;
            int posY = 8;
            for (int row = 0; row < maxRow; row++)
            {
                Console.SetCursorPosition(posX, posY);
                posY++;

                for (int col = 0; col < maxColl; col++)
                {
                    bool top = row == 0;
                    bool bottom = row == maxRow - 1;
                    bool left = col == 0;
                    bool right = col == maxColl - 1;

                    if (top && left) Console.Write(_symbol.WallCornerTopLeftSymbol);
                    else if (top && right) Console.Write(_symbol.WallCornerTopRightSymbol);
                    else if (bottom && left) Console.Write(_symbol.WallCornerBottomLeftSymbol);
                    else if (bottom && right) Console.Write(_symbol.WallCornerBottomRightSymbol);
                    else if (top || bottom) Console.Write(_symbol.WallHorizontalSymbol);
                    else if (left || right) Console.Write(_symbol.WallVerticalSymbol);
                    else Console.Write(" ");
                }
                Console.WriteLine();
            }
        }
        private void PrintTutorialHeader()
        {
            int posX = HeaderPositionX + TutorialOffsetX;
            int posY = HeaderPositionY;
            for (int i = 0; i < HeaderTutorialScreen.Length; i++)
            {
                Console.SetCursorPosition(posX, posY);
                Console.Write(HeaderTutorialScreen[i]);
                posY++;
            }
        }
        private void PrintStartHeader()
        {
            const int WordOffsetX = 40;
            int posX = HeaderPositionX + StartOffsetX;
            int posY = HeaderPositionY + StartOffsetY;
            for (int i = 0; i < HeaderStartScreen_1.Length; i++)
            {
                Console.SetCursorPosition(posX, posY);
                Console.Write(HeaderStartScreen_1[i]);
                posY++;
            }
            posY = HeaderPositionY + StartOffsetY;
            for (int i = 0; i < HeaderStartScreen_2.Length; i++)
            {
                Console.SetCursorPosition(posX + WordOffsetX, posY);
                Console.Write(HeaderStartScreen_2[i]);
                posY++;
            }
        }
        private void PrintGoblinP()
        {
            const int OffsetX = StartSpriteOffsetX;
            const int OffsetY = StartSpriteOffsetY;
            int x = OffsetX;
            int y = OffsetY;
            for (int i = 0; i < Goblin.GoblinSpriteP.Length; i++)
            {
                Console.SetCursorPosition(x, y + i);
                Console.Write(Goblin.GoblinSpriteP[i]);
            }
        }
        private void PrintOrcE()
        {
            const int OffsetX = 20 + StartSpriteOffsetX;
            const int OffsetY = StartSpriteOffsetY - 2;
            int x = OffsetX;
            int y = OffsetY;
            for (int i = 0; i < Orc.OrcSpriteE.Length; i++)
            {
                Console.SetCursorPosition(x, y + i);
                Console.Write(Orc.OrcSpriteE[i]);
            }
        }
        private void PrintTrollP()
        {
            const int OffsetX = 55 + StartSpriteOffsetX;
            const int OffsetY = StartSpriteOffsetY - 2;
            int x = OffsetX;
            int y = OffsetY;
            for (int i = 0; i < Troll.TrollSpriteP.Length; i++)
            {
                Console.SetCursorPosition(x, y + i);
                Console.Write(Troll.TrollSpriteP[i]);
            }
        }
        private void PrintSlimeE()
        {
            const int OffsetX = 75 + StartSpriteOffsetX;
            const int OffsetY = 7 + StartSpriteOffsetY;
            int x = OffsetX;
            int y = OffsetY;
            for (int i = 0; i < Slime.SlimeSpriteE.Length; i++)
            {
                Console.SetCursorPosition(x, y + i);
                Console.Write(Slime.SlimeSpriteE[i]);
            }
        }
    }
}
