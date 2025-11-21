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

namespace S1_D001_Monsterkampf_Simulator_ER.Managers
{
    /// <summary>
    /// Provides all visual Unicode symbols used in the console-based Escape Room.
    /// </summary>
    internal class SymbolManager
    {

        // Private fields for symbols
        // Some Symbols are needed for later updates
        private char _pointerSymbol = '\u25BA';//                      ' ► '

        private char _infoboxCornerTopLeftSymbol = '\u250c';//       ' ┌ '

        private char _infoBoxCornerTopRightSymbol = '\u2510'; //     ' ┐ '

        private char _infoBoxCornerBottomLeftSymbol = '\u2514'; //   ' └ '

        private char _infoBoxCornerBottomRightSymbol = '\u2518'; //  ' ┘ '

        private char _infoBoxHorizontalLineSymbol = '\u2500'; //     ' ─ '

        private char _infoBoxVerticalLineSymbol = '\u2502'; //       ' │ '

        private char _filledHpBar = '\u2588'; //       ' █ '

        private char _unfilledHpBar = '\u2591'; //       ' ░ '

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

        private char _playerSymbol = '\u2659';//                     ' ♙ '

        private char _trapSymbol = '\u2297';// ;                     ' ⊗ '

        private char _fogSymbol = '\u2592';// ;                      ' ▒ '

        private char _deathSymbol = '\u2620';  //                    ' ☠ '

        private char _keyFragmentSymbol = '\u26BF';  //              ' ⚿ '

        private char _questSymbol = '\u003f';//                      ' ? ' 

        private char _emptySymbol = ' ';//                           '   '

        private char _closedDoorVerticalSymbol = '\u25AE';//         ' ▮ '

        private char _openDoorVerticalSymbol = '\u25AF';//           ' ▯ '

        private char _closedDoorHorizontalSymbol = '\u25AC';//          ' ▬ '

        private char _openDoorHorizontalSymbol = '\u25AD';//            ' ▭ '

        private char _timeWatchSymbol = '\u23f1';//                  ' ⏱ '

        private char _hearthSymbol = '\u2764';//                      ' ❤ '

        // Properties with get and set for possible upgrades later

        /// <summary>
        /// Pointer Symbol: ►
        /// </summary>
        public char PointerSymbol { get => _pointerSymbol; }

        /// <summary>
        /// Hp Bar for current HP: █
        /// </summary>
        public char filledHpBar { get => _filledHpBar; }

        /// <summary>
        /// Hp Bar for HP range to max HP: ░
        /// </summary>
        public char unfilledBar {  get => _unfilledHpBar; }

        /// <summary>
        /// Infobox Top-left corner: ┌
        /// </summary>
        public char InfoBoxCornerTopLeftSymbol { get => _infoboxCornerTopLeftSymbol;}

        /// <summary>
        /// Infobox Top-right corner: ┐
        /// </summary>
        public char InfoBoxCornerTopRightSymbol { get => _infoBoxCornerTopRightSymbol; }

        /// <summary>
        /// Infobox Bottom-left corner: └
        /// </summary>
        public char InfoBoxCornerBottomLeftSymbol { get => _infoBoxCornerBottomLeftSymbol; }

        /// <summary>
        /// Infobox Bottom-right corner: ┘
        /// </summary>
        public char InfoBoxCornerBottomRightSymbol { get => _infoBoxCornerBottomRightSymbol; }

        /// <summary>
        /// Infobox horizontal line symbol: ─
        /// </summary>
        public char InfoBoxHorizontalLineSymbol { get => _infoBoxHorizontalLineSymbol; }

        /// <summary>
        /// Infobox vertical line symbol: │
        /// </summary>
        public char InfoBoxVerticalLineSymbol { get => _infoBoxVerticalLineSymbol; }

        /// <summary>
        /// Wall intersection (top-down): ╦
        /// </summary>
        public char WallTDownSymbol { get => _wallTDownSymbol; private set => _wallTDownSymbol = value; }

        /// <summary>
        /// Wall intersection (bottom-up): ╩
        /// </summary>
        public char WallTUpSymbol { get => _wallTUpSymbol; private set => _wallTUpSymbol = value; }

        /// <summary>
        /// Wall intersection (left-right): ╠
        /// </summary>
        public char WallTRightSymbol { get => _wallTRightSymbol; private set => _wallTRightSymbol = value; }

        /// <summary>
        /// Wall intersection (right-left): ╣
        /// </summary>
        public char WallTLeftSymbol { get => _wallTLeftSymbol; private set => _wallTLeftSymbol = value; }

        /// <summary>
        /// Wall cross intersection: ╬
        /// </summary>
        public char WallCrossSymbol { get => _wallCrossSymbol; private set => _wallCrossSymbol = value; }

        /// <summary>
        /// Horizontal wall: ═
        /// </summary>
        public char WallHorizontalSymbol { get => _wallHorizontalSymbol; private set => _wallHorizontalSymbol = value; }

        /// <summary>
        /// Vertical wall: ║
        /// </summary>
        public char WallVerticalSymbol { get => _wallVerticalSymbol; private set => _wallVerticalSymbol = value; }

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

        /// <summary>
        /// Player: ♙
        /// </summary>
        public char PlayerSymbol { get => _playerSymbol; private set => _playerSymbol = value; }

        /// <summary>
        /// Trap: ⊗
        /// </summary>
        public char TrapSymbol { get => _trapSymbol; private set => _trapSymbol = value; }

        /// <summary>
        /// Fog of war: ▒
        /// </summary>
        public char FogSymbol { get => _fogSymbol; private set => _fogSymbol = value; }

        /// <summary>
        /// Death: ☠
        /// </summary>
        public char DeathSymbol { get => _deathSymbol; private set => _deathSymbol = value; }

        /// <summary>
        /// Key fragment: ⚿
        /// </summary>
        public char KeyFragmentSymbol { get => _keyFragmentSymbol; private set => _keyFragmentSymbol = value; }

        /// <summary>
        /// Quest mark: ?
        /// </summary>
        public char QuestSymbol { get => _questSymbol; private set => _questSymbol = value; }

        /// <summary>
        /// Empty space.
        /// </summary>
        public char EmptySymbol { get => _emptySymbol; private set => _emptySymbol = value; }

        /// <summary>
        /// Closed vertical door: ▮
        /// </summary>
        public char ClosedDoorVerticalSymbol { get => _closedDoorVerticalSymbol; private set => _closedDoorVerticalSymbol = value; }

        /// <summary>
        /// Open vertical door: ▯
        /// </summary>
        public char OpenDoorVerticalSymbol { get => _openDoorVerticalSymbol; private set => _openDoorVerticalSymbol = value; }

        /// <summary>
        /// Closed horizontal door: ▬
        /// </summary>
        public char ClosedDoorHorizontalSymbol { get => _closedDoorHorizontalSymbol; private set => _closedDoorHorizontalSymbol = value; }

        /// <summary>
        /// Open horizontal door: ▭
        /// </summary>
        public char OpenDoorHorizontalSymbol { get => _openDoorHorizontalSymbol; private set => _openDoorHorizontalSymbol = value; }

        /// <summary>
        /// Time watch: ⏱
        /// </summary>
        public char TimeWatchSymbol { get => _timeWatchSymbol; private set => _timeWatchSymbol = value; }

        /// <summary>
        /// Heart: ❤
        /// </summary>
        public char HearthSymbol { get => _hearthSymbol; private set => _hearthSymbol = value; }
    }
}