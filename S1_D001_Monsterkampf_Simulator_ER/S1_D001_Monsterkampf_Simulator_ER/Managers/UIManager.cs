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
using System.ComponentModel;

namespace S1_D001_Monsterkampf_Simulator_ER.Managers
{
    internal class UIManager
    {
        private readonly SymbolManager _symbol;


        public UIManager(SymbolManager symbol)
        {
            _symbol = symbol;
        }
        //TODO könnte veraltet sein muss geändert werden später viel.
        public void ShowStatDistributionMenu(int unassignedStatPoints)
        {
            new NotImplementedException();
        }
        //TODO könnte veraltet sein muss geändert werden später viel.
        public void PrintOptions()
        {
            new NotImplementedException();
        }

        public void PrintOutlineLayout(int maxRow, int maxColl)
        {
            for (int row = 0; row < maxRow; row++)
            {
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
        public void PrintSkillBoxLayout(int maxRow, int maxColl, (int x, int y) position)
        {
            for (int row = 0; row < maxRow; row++)
            {
                Console.SetCursorPosition(position.x, position.y + row);

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
            }
        }
        public void DrawMonsterBoxFrame(int x, int y, int height, int width)
        {
            for (int row = 0; row < height; row++)
            {
                Console.SetCursorPosition(x, y + row);

                for (int col = 0; col < width; col++)
                {
                    bool top = row == 0;
                    bool bottom = row == height - 1;
                    bool left = col == 0;
                    bool right = col == width - 1;

                    if (top && left) Console.Write(_symbol.InfoBoxCornerTopLeftSymbol);
                    else if (top && right) Console.Write(_symbol.InfoBoxCornerTopRightSymbol);
                    else if (bottom && left) Console.Write(_symbol.InfoBoxCornerBottomLeftSymbol);
                    else if (bottom && right) Console.Write(_symbol.InfoBoxCornerBottomRightSymbol);
                    else if (top || bottom) Console.Write(_symbol.InfoBoxHorizontalLineSymbol);
                    else if (left || right) Console.Write(_symbol.InfoBoxVerticalLineSymbol);
                    else Console.Write(" ");
                }
            }
        }
        public void DrawMonsterLabelStats(int x, int y)
        {
            const int LabelOffsetLeft = 2; ;
            const int LableOffsetRight = 20;
            int LableStartY = 1;
            int yOffset = LableStartY;
           

            Console.SetCursorPosition(x + LabelOffsetLeft, y + yOffset);
            Console.Write("HP :");
            yOffset++;

            Console.SetCursorPosition(x + LabelOffsetLeft, y + yOffset);
            Console.Write("HP :");
            yOffset++;

            Console.SetCursorPosition(x + LabelOffsetLeft, y + yOffset);
            Console.Write("LVL:");

            Console.SetCursorPosition(x + LableOffsetRight, y + yOffset);
            Console.Write("Speed:");
            yOffset++;

            Console.SetCursorPosition(x + LabelOffsetLeft, y + yOffset);
            Console.Write("AP :");

            Console.SetCursorPosition(x + LableOffsetRight, y + yOffset);
            Console.Write("DP   :");
        }
        private void ClearMonsterLabelStats(int x, int y)
        {
            const int DrawLabelOffsetLeft = 2; ;
            const int DrawLableOffsetRight = 20;
            // "HP :", "AP :", "LVL:"
            const int StatStringOffset_1 = 4;
            // "Speed:", "DP   :"
            const int StatStringOffset_2 = 6;
            const int maxValueCharSpace = 6;
            const int maxValueCharSpaceHP = 16;

            int LableStartY = 1;
            int yOffset = LableStartY;

            Console.SetCursorPosition(x + DrawLabelOffsetLeft + StatStringOffset_2, y + yOffset);
            Console.Write(new string(' ', maxValueCharSpaceHP));
            yOffset++;

            Console.SetCursorPosition(x + DrawLabelOffsetLeft + StatStringOffset_1, y + yOffset);
            Console.Write(new string(' ', maxValueCharSpace));
            yOffset++;

            Console.SetCursorPosition(x + DrawLabelOffsetLeft + StatStringOffset_1, y + yOffset);
            Console.Write(new string(' ', maxValueCharSpace));

            Console.SetCursorPosition(x + DrawLableOffsetRight + StatStringOffset_2, y + yOffset);
            Console.Write(new string(' ', maxValueCharSpace));
            yOffset++;

            Console.SetCursorPosition(x + DrawLabelOffsetLeft + StatStringOffset_1, y + yOffset);
            Console.Write(new string(' ', maxValueCharSpace));

            Console.SetCursorPosition(x + DrawLableOffsetRight + StatStringOffset_2, y + yOffset);
            Console.Write(new string(' ', maxValueCharSpace));
        }
        public void PrintMonsterInfoBoxPlayer()
        {
            const int BoxHeight = 6;
            const int BoxWidth = 34;
            const int BoxPositionX = 20;
            const int BoxPositionY = 3;
            DrawMonsterBoxFrame(BoxPositionX, BoxPositionY, BoxHeight, BoxWidth);
            DrawMonsterLabelStats(BoxPositionX, BoxPositionY);
        }
        public void PrintMonsterInfoBoxEnemy()
        {
            const int BoxHeight = 6;
            const int BoxWidth = 34;
            const int BoxPositionX = 63;
            const int BoxPositionY = 3;
            DrawMonsterBoxFrame(BoxPositionX, BoxPositionY, BoxHeight, BoxWidth);
            DrawMonsterLabelStats(BoxPositionX, BoxPositionY);
        }
        public void PrintMessageBoxLayout(int x, int y)
        {
            int height = 6;
            int width = 80;
            for (int row = 0; row < height; row++)
            {
                Console.SetCursorPosition(x, y + row);

                for (int col = 0; col < width; col++)
                {
                    bool top = row == 0;
                    bool bottom = row == height - 1;
                    bool left = col == 0;
                    bool right = col == width - 1;

                    if (top && left) Console.Write(_symbol.WallTDownSymbol);
                    else if (top && right) Console.Write(_symbol.WallTLeftSymbol);
                    else if (bottom && left) Console.Write(_symbol.WallTUpSymbol);
                    else if (bottom && right) Console.Write(_symbol.WallCornerBottomRightSymbol);
                    else if (top || bottom) Console.Write(_symbol.WallHorizontalSymbol);
                    else if (left || right) Console.Write(_symbol.WallVerticalSymbol);
                    else Console.Write(" ");
                }
            }
        }
        public void PrintSkillBoxLayout(int x, int y)
        {
            int height = 6;
            int width = 21;
            for (int row = 0; row < height; row++)
            {
                Console.SetCursorPosition(x, y + row);

                for (int col = 0; col < width; col++)
                {
                    bool top = row == 0;
                    bool bottom = row == height - 1;
                    bool left = col == 0;
                    bool right = col == width - 1;

                    if (top && left) Console.Write(_symbol.WallTRightSymbol);
                    else if (top && right) Console.Write(_symbol.WallTDownSymbol);
                    else if (bottom && left) Console.Write(_symbol.WallCornerBottomLeftSymbol);
                    else if (bottom && right) Console.Write(_symbol.WallTUpSymbol);
                    else if (top || bottom) Console.Write(_symbol.WallHorizontalSymbol);
                    else if (left || right) Console.Write(_symbol.WallVerticalSymbol);
                    else Console.Write(" ");
                }
            }
        }
        public void UpdateMonsterBox(MonsterBase monster, int x, int y)
        {
            UpdateMonsterLabelStats(x, y, monster);
        }
        private void UpdateMonsterLabelStats(int x, int y, MonsterBase monster)
        {
            const int MaxValue = 999999;
            const int maxBarSegments = 20;
            const int LabelOffsetLeft = 2; ;
            const int LableOffsetRight = 20;

            int LableStartY = 1;
            int yOffset = LableStartY;

            ClearMonsterLabelStats(x, y);

            Console.SetCursorPosition(x + LabelOffsetLeft, y + yOffset);
            Console.Write("HP : ");
            DrawHPBar(monster.Meta.CurrentHP, monster.Meta.MaxHP, maxBarSegments);
            yOffset++;

            Console.SetCursorPosition(x + LabelOffsetLeft, y + yOffset);
            if (monster.Meta.MaxHP < MaxValue)
            {
                Console.Write($"HP : {monster.Meta.CurrentHP:0}/{monster.Meta.MaxHP}");
            }
            else Console.Write("HP  : < 9999");
            yOffset++;

            Console.SetCursorPosition(x + LabelOffsetLeft, y + yOffset);
            if (monster.Level < MaxValue)
            {
                Console.Write($"LVL: {monster.Level:0}");
            }
            else Console.Write("LVL: < 9999");

            Console.SetCursorPosition(x + LableOffsetRight, y + yOffset);
            if (monster.Meta.Speed < MaxValue)
            {
                Console.Write($"Speed: {monster.Meta.Speed}");
            }
            else Console.Write("Speed: < 9999");
            yOffset++;

            Console.SetCursorPosition(x + LabelOffsetLeft, y + yOffset);
            if (monster.Meta.AP < MaxValue)
            {
                Console.Write($"AP : {monster.Meta.AP:0.0}");
            }
            else Console.Write("AP : < 9999");

            Console.SetCursorPosition(x + LableOffsetRight, y + yOffset);
            if (monster.Meta.DP < MaxValue)
            {
                Console.Write($"DP   : {monster.Meta.DP:0.0}");
            }
            else Console.Write("DP   : < 9999");
        }
        private void DrawHPBar(float currentHP, float maxHP, int maxBar)
        {
            float singleBarValue = maxHP / maxBar;
            float currentBars = currentHP / singleBarValue;
            int filledBars = (int)Math.Round(currentBars, MidpointRounding.AwayFromZero);
            int unfilledBars = (int)(Math.Round(maxBar - currentBars, MidpointRounding.AwayFromZero));
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write(new string(_symbol.filledHpBar, filledBars));
            Console.Write(new string(_symbol.unfilledBar, unfilledBars));
            Console.ResetColor();
        }






        //TODO kommt später in die richtige monsterklasse
        private readonly string[] SlimeSpriteE =
        {
            "     ____",
            "    (    )",
            "   (O O   )",
            "   (__     )",
            "   (V V    )",
            "  (________)"
        };


        //TODO kommt später in die richtige monsterklasse
        private readonly string[] SlimeSpriteP =
        {
            "     ____",
            "    (    )",
            "   (   O O)",
            "  (    __ )",
            "  (    V V)",
            "  (________)"
        };

        public void PrintSlimeE(int x, int y)
        {
            for (int i = 0; i < SlimeSpriteE.Length; i++)
            {
                Console.SetCursorPosition(x, y + i);
                Console.Write(SlimeSpriteE[i]);
            }
        }

        public void PrintSlimeP(int x, int y)
        {
            for (int i = 0; i < SlimeSpriteP.Length; i++)
            {
                Console.SetCursorPosition(x, y + i);
                Console.Write(SlimeSpriteP[i]);
            }

        }

      
    }

}
