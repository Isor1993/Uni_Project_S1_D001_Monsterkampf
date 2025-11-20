using S1_D001_Monsterkampf_Simulator_ER.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S1_D001_Monsterkampf_Simulator_ER.Managers
{
    internal class UIManager
    {
        private readonly SymbolManager _symbol;
        public UIManager(SymbolManager symbol)
        {
            _symbol = symbol;
        }
        //TODO
        public void ShowStatDistributionMenu(int unassignedStatPoints)
        {
            new NotImplementedException();
        }
        public void PrintOptions()
        {

        }

        public void PrintOutlineLayout(int maxRow, int maxColl)
        {

            const int offsetSymbol = 1;

            for (int row = 0; row < maxRow; row++)
            {
                for (int col = 0; col < maxColl; col++)
                {
                    // Eckpunkte
                    if (row == 0 && col == 0)
                    {
                        Console.Write(_symbol.WallCornerTopLeftSymbol);
                    }
                    else if (row == 0 && col == maxColl - offsetSymbol)
                    {
                        Console.Write(_symbol.WallCornerTopRightSymbol);
                    }
                    else if (row == maxRow - offsetSymbol && col == 0)
                    {
                        Console.Write(_symbol.WallCornerBottomLeftSymbol);
                    }
                    else if (row == maxRow - offsetSymbol && col == maxColl - offsetSymbol)
                    {
                        Console.Write(_symbol.WallCornerBottomRightSymbol);
                    }
                    // obere oder untere horizontale Linie
                    else if (row == 0 || row == maxRow - offsetSymbol)
                    {
                        Console.Write(_symbol.WallHorizontalSymbol);
                    }
                    // vertikale Linien links & rechts
                    else if (col == 0 || col == maxColl - offsetSymbol)
                    {
                        Console.Write(_symbol.WallVerticalSymbol);
                    }
                    else
                    {
                        Console.Write(" "); // innen leer
                    }
                }

                Console.WriteLine();
            }
        }

        public void PrintSkillBoxLayout(int maxRow, int maxColl, (int x, int y) position)
        {
            const int offsetSymbol = 1;

            Console.SetCursorPosition(position.x, position.y);

            for (int row = 0; row < maxRow; row++)
            {
                for (int col = 0; col < maxColl; col++)
                {
                    // Eckpunkte
                    if (row == 0 && col == 0)
                    {
                        Console.Write(_symbol.WallCornerTopLeftSymbol);
                    }
                    else if (row == 0 && col == maxColl - offsetSymbol)
                    {
                        Console.Write(_symbol.WallCornerTopRightSymbol);
                    }
                    else if (row == maxRow - offsetSymbol && col == 0)
                    {
                        Console.Write(_symbol.WallCornerBottomLeftSymbol);
                    }
                    else if (row == maxRow - offsetSymbol && col == maxColl - offsetSymbol)
                    {
                        Console.Write(_symbol.WallCornerBottomRightSymbol);
                    }
                    // obere oder untere horizontale Linie
                    else if (row == 0 || row == maxRow - offsetSymbol)
                    {
                        Console.Write(_symbol.WallHorizontalSymbol);
                    }
                    // vertikale Linien links & rechts
                    else if (col == 0 || col == maxColl - offsetSymbol)
                    {
                        Console.Write(_symbol.WallVerticalSymbol);
                    }
                    else
                    {
                        Console.Write(" "); // innen leer
                    }
                }

                Console.WriteLine();
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

                    if (top && left) Console.Write("┌");
                    else if (top && right) Console.Write("┐");
                    else if (bottom && left) Console.Write("└");
                    else if (bottom && right) Console.Write("┘");
                    else if (top || bottom) Console.Write("─");
                    else if (left || right) Console.Write("│");
                    else Console.Write(" ");
                }
            }
        }
        public void DrawMonsterLabels(int x, int y)
        {
            Console.SetCursorPosition(x + 2, y + 1);
            Console.Write("HP :");

            Console.SetCursorPosition(x + 2, y + 2);
            Console.Write("HP :");

            Console.SetCursorPosition(x + 2, y + 3);
            Console.Write("LVL:");

            Console.SetCursorPosition(x + 2, y + 4);
            Console.Write("AP :");

            Console.SetCursorPosition(x + 20, y + 3);
            Console.Write("Speed:");

            Console.SetCursorPosition(x + 20, y + 4);
            Console.Write("DP   :");
        }
        public void PrintMonsterInfoBox(int x, int y)
        {
            DrawMonsterBoxFrame(x, y, 6, 34);
            DrawMonsterLabels(x, y);
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
    }
..........::*%#*++*#%*::..........
:::....::*#*++======++*#+:::..::::
#+*:..:=#*++++++++++++++*#=:..:++#
-%*%::*##*++*+*++++++*+**##*::#*%-
:%#*+##**++=**+#**#+**=++**##+*#%:
:##%%####%*+==++++++==+*##*##%%##:
:#*%@##**%=%##*#**#*##%=%**##@%**-
:-#*++=+*##%#@%*++*%@#%##*+=+*+#-:
.::=##+=+******+==*#*****+=+##+:..
..::*%*+*++#%+*+--=*+%#++*+*%*:...
....:@#+#*#*%%%+==+%%#*#*#+#@:....
....:%#=-#*++**%##%**++*#:+#%::...
....:%%#:+*+**++**++**+*+:#%%::...
....:@*%%==%==#**+*#==%==##*%::...
....:%*#%%%*+++++++*++*%%%*+%::...
.....*%#++*#%%%%%%%%%%#*++#%*:....
.....:-%@%#**+======+**#%@%-:.....
.......:.-%#*+=++++=+*#%::........
::::::::::::+@%%%%%%@+::::::::::::
@*++%@@@@@@@@@@@@@@@@@@@@@@@@%#%%@
}
