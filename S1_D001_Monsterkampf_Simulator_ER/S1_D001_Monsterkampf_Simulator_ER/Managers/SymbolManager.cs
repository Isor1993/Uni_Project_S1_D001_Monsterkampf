/*****************************************************************************
* Project : Monsterkampf-Simulator (K1, S1, S4)
* File    : SymbolManager.cs
* Date    : 03.12.2025
* Author  : Eric Rosenberg
*
* Description :
*   Provides all Unicode symbols used throughout the UI (borders, corners,
*   HP bars, pointer symbol, box frames, wall-drawing characters).
*   Centralizes all UI character rendering so the visuals can be changed or
*   upgraded easily without modifying individual UI components.
*
* Responsibilities :
*   - Provide symbols for info boxes, monster boxes, and message boxes
*   - Provide HP bar symbols (filled / unfilled)
*   - Provide corner and line drawing characters for all UI borders
*   - Provide pointer symbol for selection menus
*   - Expose symbols via read-only properties for consistent usage
*
* History :
*   03.12.2025 ER Created
******************************************************************************/

namespace S1_D001_Monsterkampf_Simulator_ER.Managers
{
    internal class SymbolManager
    {
        private char _pointerSymbol = '\u25BA';//                    ' ► '

        private char _infoboxCornerTopLeftSymbol = '\u250c';//       ' ┌ '

        private char _infoBoxCornerTopRightSymbol = '\u2510'; //     ' ┐ '

        private char _infoBoxCornerBottomLeftSymbol = '\u2514'; //   ' └ '

        private char _infoBoxCornerBottomRightSymbol = '\u2518'; //  ' ┘ '

        private char _infoBoxHorizontalLineSymbol = '\u2500'; //     ' ─ '

        private char _infoBoxVerticalLineSymbol = '\u2502'; //       ' │ '

        private char _filledHpBar = '\u2588'; //                     ' █ '

        private char _unfilledHpBar = '\u2591'; //                   ' ░ '

        private char _wallTDownSymbol = '\u2566'; //                 ' ╦ '

        private char _wallTUpSymbol = '\u2569'; //                   ' ╩ '

        private char _wallTRightSymbol = '\u2560'; //                ' ╠ '

        private char _wallTLeftSymbol = '\u2563'; //                 ' ╣ '

        private char _wallCrossSymbol = '\u256C'; //                 ' ╬ '

        private char _wallHorizontalSymbol = '\u2550';//             ' ═ '

        private char _wallVerticalSymbol = '\u2551';//               ' ║'

        private char _wallCornerTopLeftSymbol = '\u2554';//          ' ╔ '

        private char _wallCornerTopRightSymbol = '\u2557';//         ' ╗ '

        private char _wallCornerBottomLeftSymbol = '\u255A';//       ' ╚ '

        private char _wallCornerBottomRightSymbol = '\u255D';//      ' ╝ '

        // Properties with get and set for possible upgrades later

        /// <summary>
        /// Pointer Symbol: ►
        /// </summary>
        public char PointerSymbol => _pointerSymbol;

        /// <summary>
        /// Hp Bar for current HP: █
        /// </summary>
        public char filledHpBar => _filledHpBar;

        /// <summary>
        /// Hp Bar for HP range to max HP: ░
        /// </summary>
        public char unfilledBar => _unfilledHpBar;

        /// <summary>
        /// Infobox Top-left corner: ┌
        /// </summary>
        public char InfoBoxCornerTopLeftSymbol => _infoboxCornerTopLeftSymbol;

        /// <summary>
        /// Infobox Top-right corner: ┐
        /// </summary>
        public char InfoBoxCornerTopRightSymbol => _infoBoxCornerTopRightSymbol;

        /// <summary>
        /// Infobox Bottom-left corner: └
        /// </summary>
        public char InfoBoxCornerBottomLeftSymbol => _infoBoxCornerBottomLeftSymbol;

        /// <summary>
        /// Infobox Bottom-right corner: ┘
        /// </summary>
        public char InfoBoxCornerBottomRightSymbol => _infoBoxCornerBottomRightSymbol;

        /// <summary>
        /// Infobox horizontal line symbol: ─
        /// </summary>
        public char InfoBoxHorizontalLineSymbol => _infoBoxHorizontalLineSymbol;

        /// <summary>
        /// Infobox vertical line symbol: │
        /// </summary>
        public char InfoBoxVerticalLineSymbol => _infoBoxVerticalLineSymbol;

        /// <summary>
        /// Wall intersection (top-down): ╦
        /// </summary>
        public char WallTDownSymbol => _wallTDownSymbol;

        /// <summary>
        /// Wall intersection (bottom-up): ╩
        /// </summary>
        public char WallTUpSymbol => _wallTUpSymbol;

        /// <summary>
        /// Wall intersection (left-right): ╠
        /// </summary>
        public char WallTRightSymbol => _wallTRightSymbol;

        /// <summary>
        /// Wall intersection (right-left): ╣
        /// </summary>
        public char WallTLeftSymbol => _wallTLeftSymbol;

        /// <summary>
        /// Wall cross intersection: ╬
        /// </summary>
        public char WallCrossSymbol => _wallCrossSymbol;

        /// <summary>
        /// Horizontal wall: ═
        /// </summary>
        public char WallHorizontalSymbol => _wallHorizontalSymbol;

        /// <summary>
        /// Vertical wall: ║
        /// </summary>
        public char WallVerticalSymbol => _wallVerticalSymbol;

        /// <summary>
        /// Top-left corner: ╔
        /// </summary>
        public char WallCornerTopLeftSymbol { get => _wallCornerTopLeftSymbol; private set => _wallCornerTopLeftSymbol = value; }

        /// <summary>
        /// Top-right corner: ╗
        /// </summary>
        public char WallCornerTopRightSymbol { get => _wallCornerTopRightSymbol; private set => _wallCornerTopRightSymbol = value; }

        /// <summary>
        /// Bottom-left corner: ╚
        /// </summary>
        public char WallCornerBottomLeftSymbol { get => _wallCornerBottomLeftSymbol; private set => _wallCornerBottomLeftSymbol = value; }

        /// <summary>
        /// Bottom-right corner: ╝
        /// </summary>
        public char WallCornerBottomRightSymbol { get => _wallCornerBottomRightSymbol; private set => _wallCornerBottomRightSymbol = value; }
    }
}