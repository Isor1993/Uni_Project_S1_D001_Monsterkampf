/*****************************************************************************
* Project : Monsterkampf-Simulator (K1, S1, S4)
* File    : ScreenManager.cs
* Date    : 03.12.2025
* Author  : Eric Rosenberg
*
* Description :
*   Handles all full-screen console renderings such as Start-Screen,
*   Tutorial-Screen and End-Screen. Responsible for drawing headers,
*   decorative ASCII sprites, tutorial text, end text and frame layouts.
*
* Responsibilities :
*   - Render start screen including header and monster sprites
*   - Render tutorial screen including frame and tutorial instructions
*   - Render end screen including stats, contact info and frame
*   - Manage layout offsets and positioning for all screen elements
*
* History :
*   03.12.2025 ER Created
******************************************************************************/
using S1_D001_Monsterkampf_Simulator_ER.Controllers;
using S1_D001_Monsterkampf_Simulator_ER.Monsters;

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

        private string tutorialLine_0 = " Welcome to my monster simulation fighter project!";
        private string tutorialLine_1 = " [W] or [↑]    = Move up in the selection to choose options/skills";
        private string tutorialLine_2 = " [S] or [↓]    = Move down in the selection to choose options/skills";
        private string tutorialLine_3 = " [ENTER]       = Confirm a selection";
        private string tutorialLine_4 = " Monsters      : There are currently 4 monsters";
        private string tutorialLine_5 = " Passive skills: Every monster has one which is always active at the start";
        private string tutorialLine_6 = " Active skills : Basic or special attacks with effects and cooldowns";
        private string tutorialLine_7 = " Cooldowns     : Cooldowns are measured in rounds";
        private string tutorialLine_8 = " Stats         : HP    = Health";
        private string tutorialLine_9 = "                 AP    = Attack Power";
        private string tutorialLine_10 = "                 DP    = Defense";
        private string tutorialLine_11 = "                 Speed = Decides who will start the round";
        private string tutorialLine_12 = " I wish you a lot of fun! After each won fight, you get a new enemy.";
        private string tutorialLine_13 = " You also gain a level-up and a stat point to make your monster stronger.";

        private string endLine_0 = " I hope you had a little bit of fun! :)  ";
        private string endLine_1 = " This small project was created during my studies at ";
        private string endLine_2 = " the university.In the ReadMe file, you’ll find information";
        private string endLine_3 = " about the Project. Your Final Stats below ";
        private string endLine_4 = " Final Stats   ";
        private string endLine_7 = " Contact info:  ";
        private string endLine_8 = " Instagram: https://www.instagram.com/isor_gamedev  ";
        private string endLine_9 = " GitHub: https://github.com/Isor1993 ";
        private string endLine_10 = " E-mail: IsorDev@email.de    ";

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

        /// <summary>
        /// Creates a new ScreenManager which controls all start, tutorial,
        /// and end screen console renderings.
        /// </summary>
        /// <param name="symbol">Reference to the SymbolManager providing wall and UI characters.</param>
        /// <param name="playerController">Reference to the player controller for accessing the player's monster and stats.</param>
        public ScreenManager(SymbolManager symbol, PlayerController playerController)
        {
            _symbol = symbol;

            _playerController = playerController;
        }

        /// <summary>
        /// Renders the full start screen including header and all monster sprites.
        /// </summary>
        public void PrintScreenStart()
        {
            PrintStartHeader();
            PrintGoblinP();
            PrintOrcE();
            PrintTrollP();
            PrintSlimeE();
        }

        /// <summary>
        /// Renders the full tutorial screen including the header, frame,
        /// and all tutorial instruction text lines.
        /// </summary>
        public void PrintScreenTutorial()
        {
            PrintTutorialHeader();
            PrintTutorialFrame();
            PrintTutorialText();

        }

        /// <summary>
        /// Renders the full end screen including frame, header,
        /// end text, statistics display and contact information.
        /// </summary>
        public void PrintScreenEnd()
        {
            PrintEndFrame();
            PrintEndHeader();
            PrintEndText();

        }

        /// <summary>
        /// Draws the end text including final stats and contact information.
        /// </summary>
        private void PrintEndText()
        {
            int posX = 23;
            int posY = 10;
            Console.SetCursorPosition(posX, posY);
            Console.WriteLine(endLine_0);
            posY++;
            Console.SetCursorPosition(posX, posY);
            Console.WriteLine(endLine_1);
            posY++;
            Console.SetCursorPosition(posX, posY);
            Console.WriteLine(endLine_2);
            posY++;
            Console.SetCursorPosition(posX, posY);
            Console.WriteLine(endLine_3);
            posY++;
            posY++;
            Console.SetCursorPosition(posX, posY);
            Console.WriteLine(endLine_4);
            posY++;
            Console.SetCursorPosition(posX, posY);
            Console.WriteLine($" Total Fights : {GameManager.TotalFights}");
            posY++;
            Console.SetCursorPosition(posX, posY);
            Console.WriteLine($" Monster Lvl  : {_playerController.Monster.Level}");
            posY++;
            posY++;
            Console.SetCursorPosition(posX, posY);
            Console.WriteLine(endLine_7);
            posY++;
            Console.SetCursorPosition(posX, posY);
            Console.WriteLine(endLine_8);
            posY++;
            Console.SetCursorPosition(posX, posY);
            Console.WriteLine(endLine_9);
            posY++;
            Console.SetCursorPosition(posX, posY);
            Console.WriteLine(endLine_10);
            posY++;
        }

        /// <summary>
        /// Draws the ASCII header for the end screen.
        /// </summary>
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

        /// <summary>
        /// Draws the rectangular frame used in the end screen.
        /// </summary>
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

        /// <summary>
        /// Writes all tutorial descriptive text lines into the tutorial frame.
        /// </summary>
        private void PrintTutorialText()
        {
            int posX = 23;
            int posY = 10;
            Console.SetCursorPosition(posX, posY);
            posY++;
            posY++;
            Console.WriteLine(tutorialLine_0);
            Console.SetCursorPosition(posX, posY);
            posY++;
            Console.WriteLine(tutorialLine_1);
            Console.SetCursorPosition(posX, posY);
            posY++;
            Console.WriteLine(tutorialLine_2);
            Console.SetCursorPosition(posX, posY);
            posY++;
            Console.WriteLine(tutorialLine_3);
            Console.SetCursorPosition(posX, posY);
            posY++;
            Console.WriteLine(tutorialLine_4);
            Console.SetCursorPosition(posX, posY);
            posY++;
            Console.WriteLine(tutorialLine_5);
            Console.SetCursorPosition(posX, posY);
            posY++;
            Console.WriteLine(tutorialLine_6);
            Console.SetCursorPosition(posX, posY);
            posY++;
            Console.WriteLine(tutorialLine_7);
            Console.SetCursorPosition(posX, posY);
            posY++;
            Console.WriteLine(tutorialLine_8);
            Console.SetCursorPosition(posX, posY);
            posY++;
            Console.WriteLine(tutorialLine_9);
            Console.SetCursorPosition(posX, posY);
            posY++;
            Console.WriteLine(tutorialLine_10);
            Console.SetCursorPosition(posX, posY);
            posY++;
            posY++;
            Console.WriteLine(tutorialLine_11);
            Console.SetCursorPosition(posX, posY);
            posY++;
            Console.WriteLine(tutorialLine_12);
            Console.SetCursorPosition(posX, posY);
            posY++;
            Console.WriteLine(tutorialLine_13);
            Console.SetCursorPosition(posX, posY);
        }

        /// <summary>
        /// Draws the rectangular frame used in the tutorial screen.
        /// </summary>
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

        /// <summary>
        /// Draws the ASCII header for the tutorial screen.
        /// </summary>
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

        /// <summary>
        /// Draws the large ASCII Start-Screen header consisting of two stacked words.
        /// </summary>
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

        /// <summary>
        /// Draws the Goblin player-side sprite on the Start-Screen.
        /// </summary>
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

        /// <summary>
        /// Draws the Orc enemy-side sprite on the Start-Screen.
        /// </summary>
        private void PrintOrcE()
        {
            const int OffsetX = 20 + StartSpriteOffsetX;
            const int OffsetY = StartSpriteOffsetY - 1;
            int x = OffsetX;
            int y = OffsetY;
            for (int i = 0; i < Orc.OrcSpriteE.Length; i++)
            {
                Console.SetCursorPosition(x, y + i);
                Console.Write(Orc.OrcSpriteE[i]);
            }
        }

        /// <summary>
        /// Draws the Troll player-side sprite on the Start-Screen.
        /// </summary>
        private void PrintTrollP()
        {
            const int OffsetX = 55 + StartSpriteOffsetX;
            const int OffsetY = StartSpriteOffsetY - 1;
            int x = OffsetX;
            int y = OffsetY;
            for (int i = 0; i < Troll.TrollSpriteP.Length; i++)
            {
                Console.SetCursorPosition(x, y + i);
                Console.Write(Troll.TrollSpriteP[i]);
            }
        }

        /// <summary>
        /// Draws the Slime enemy-side sprite on the Start-Screen.
        /// </summary>
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