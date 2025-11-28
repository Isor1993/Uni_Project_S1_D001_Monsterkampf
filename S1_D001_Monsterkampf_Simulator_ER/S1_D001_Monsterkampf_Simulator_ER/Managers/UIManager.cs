/*****************************************************************************
* Project : Monsterkampf-Simulator (K1, S1, S4)
* File    : UIManager.cs
* Date    : 03.12.2025
* Author  : Eric Rosenberg
*
* Description :
*   Handles all console-based battle UI. Draws layout frames, monster info
*   boxes, skill selection box and message box, and updates them during combat.
*
* Responsibilities :
*   - Render static UI frames (outline, info boxes, skill/message boxes)
*   - Show monster stats, skills, and selection cursors
*   - Display contextual messages (attacks, damage, choices, results)
*   - Support stat allocation and monster selection workflows
*
* History :
*   03.12.2025 ER Created
******************************************************************************/

using S1_D001_Monsterkampf_Simulator_ER.Balancing;
using S1_D001_Monsterkampf_Simulator_ER.Monsters;
using S1_D001_Monsterkampf_Simulator_ER.Skills;
using S1_D001_Monsterkampf_Simulator_ER.Systems.StatusEffects;

namespace S1_D001_Monsterkampf_Simulator_ER.Managers
{
    internal class UIManager
    {
        private readonly SymbolManager _symbol;
        private readonly DiagnosticsManager _diagnostics;
        private readonly MonsterBalancing _balancing;

        /// <summary>
        /// X position used to render the player’s monster box.
        /// </summary>
        public int PlayerPositionX => 20;

        /// <summary>
        /// Y position used to render the player’s monster box.
        /// </summary>
        public int PlayerPositionY => 3;

        /// <summary>
        /// X position used to render the enemy’s monster box.
        /// </summary>
        public int EnemyPositionX => 63;

        /// <summary>
        /// Y position used to render the enemy’s monster box.
        /// </summary>
        public int EnemyPositionY => 3;


        /// <summary>
        /// Generic offset used as padding inside framed areas.
        /// </summary>
        const int EmptyOffset = 1;

        /// <summary>
        /// Creates a new UI manager instance for drawing all battle UI.
        /// </summary>
        /// <param name="symbol">Symbol manager providing ASCII characters.</param>
        /// <param name="diagnostics">Diagnostics manager for debug logging.</param>
        /// <param name="balancing">Balancing values used for stat previews.</param>
        public UIManager(SymbolManager symbol, DiagnosticsManager diagnostics, MonsterBalancing balancing)
        {
            _symbol = symbol;
            _diagnostics = diagnostics;
            _balancing = balancing;

        }

        /// <summary>
        /// Draws the outer rectangular outline for the whole battle screen.
        /// </summary>
        public void PrintOutlineLayout()
        {
            // Position Outline
            int x = 29;
            int y = 100;
            // Size
            int maxRow = x;
            int maxColl = y;
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

        /// <summary>
        /// Draws the player monster info box frame and static labels.
        /// </summary>
        public void PrintMonsterInfoBoxPlayer()
        {
            const int BoxHeight = 6;
            const int BoxWidth = 34;
            const int BoxPositionX = 20;
            const int BoxPositionY = 3;
            DrawMonsterBoxFrame(BoxPositionX, BoxPositionY, BoxHeight, BoxWidth);
            DrawMonsterLabelStats(BoxPositionX, BoxPositionY);
        }

        /// <summary>
        /// Draws the enemy monster info box frame and static labels.
        /// </summary>
        public void PrintMonsterInfoBoxEnemy()
        {
            const int BoxHeight = 6;
            const int BoxWidth = 34;
            const int BoxPositionX = 63;
            const int BoxPositionY = 3;
            DrawMonsterBoxFrame(BoxPositionX, BoxPositionY, BoxHeight, BoxWidth);
            DrawMonsterLabelStats(BoxPositionX, BoxPositionY);
        }

        /// <summary>
        /// Draws the message box frame at the bottom-right side of the screen.
        /// </summary>
        public void PrintMessageBoxLayout()
        {
            // Position Outline
            int x = 20;
            int y = 23;

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

        /// <summary>
        ///  Draws the skill selection box frame at the bottom-left side of the screen.
        /// </summary>
        public void PrintSkillBoxLayout()
        {
            // Position Outline
            int x = 0;
            int y = 23;

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


        /// <summary>
        /// Updates the player monster info box with current stats and HP bar.
        /// </summary>
        /// <param name="monster">The player’s monster to display.</param>
        public void UpdateMonsterBoxPlayer(MonsterBase monster)
        {
            // Position Outline
            int x = 20;
            int y = 3;

            UpdateMonsterLabelStats(x, y, monster);
        }

        /// <summary>
        /// Updates the enemy monster info box with current stats and HP bar.
        /// </summary>
        /// <param name="monster">The enemy monster to display.</param>
        public void UpdateMonsterBoxEnemy(MonsterBase monster)
        {
            // Position Outline
            int x = 63;
            int y = 3;

            UpdateMonsterLabelStats(x, y, monster);
        }

        /// <summary>
        /// Clears the contents inside the skill box frame (without deleting the borders).
        /// </summary>
        public void ClearSkillBox()
        {
            {
                int x = 0;
                int y = 23;

                const int FrameOffset = 1;
                const int height = 6;
                const int width = 21;

                const int ClearWidth = width - FrameOffset * 2;
                const int ClearHeight = height - FrameOffset * 2;

                ClearArea(x + FrameOffset, y + FrameOffset, ClearWidth, ClearHeight);
            }
        }

        /// <summary>
        /// Updates the skill box with the player’s active skills and highlights
        /// the currently selected entry.
        /// </summary>
        /// <param name="skills">The skill package of the player monster.</param>
        /// <param name="pointerIndex">Index of the currently selected skill (0-based).</param>
        public void UpdateSkillBox(SkillPackage skills, int pointerIndex)
        {
            // Position Outline
            int x = 0;
            int y = 23;

            const int FrameOffset = 1;
            const int height = 6;
            const int width = 21;
            const int ClearWidth = width - FrameOffset * 2;
            const int ClearHeight = height - FrameOffset * 2;
            const int PointerOffset = 1;
            const int MaxShownSkills = 4;
            int lineY = y + FrameOffset;
            int pointerX = x + EmptyOffset;
            int pointerY = y + EmptyOffset + pointerIndex;
            int skillCount = 0;

            ClearArea(x + FrameOffset, y + FrameOffset, ClearWidth, ClearHeight);

            for (int i = 0; i < skills.ActiveSkills.Count; i++)
            {
                if (skillCount >= MaxShownSkills)
                {
                    _diagnostics.AddError($"{nameof(UIManager)}.{nameof(UpdateSkillBox)}: Too many skills ({skills.ActiveSkills.Count}). Only 4 can be displayed.");
                    break;
                }
                Console.SetCursorPosition(x + FrameOffset + PointerOffset, lineY);
                SkillBase skill = skills.ActiveSkills[i];

                if (skill.CurrentCooldown == 0)
                {
                    Console.Write(skill.Name);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write($"{skill.Name} CD:{skill.CurrentCooldown}");
                    Console.ResetColor();
                    _diagnostics.AddCheck($"{nameof(UIManager)}.{nameof(UpdateSkillBox)}: Skill {skill.Name} is still on cooldown.");
                }
                skillCount++;
                lineY++;
            }
            Console.SetCursorPosition(pointerX, pointerY);
            Console.Write(_symbol.PointerSymbol);
        }

        /// <summary>
        /// Updates the monster info box at the given position with current HP,
        /// level and other stats of the supplied monster.
        /// </summary>
        /// <param name="x">Top-left X position of the info box.</param>
        /// <param name="y">Top-left Y position of the info box.</param>
        /// <param name="monster">The monster whose stats are shown.</param>
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

                Console.Write($"HP : {(int)monster.Meta.CurrentHP}/{(int)monster.Meta.MaxHP} ");
            }
            else Console.Write($"HP  : >999999");
            yOffset++;

            Console.SetCursorPosition(x + LabelOffsetLeft, y + yOffset);
            if (monster.Level < MaxValue)
            {
                Console.Write($"LVL: {monster.Level:0}");
            }
            else Console.Write("LVL: > 9999");

            Console.SetCursorPosition(x + LableOffsetRight, y + yOffset);
            if (monster.Meta.Speed < MaxValue)
            {
                Console.Write($"Speed: {(int)monster.Meta.Speed:0}");
            }
            else Console.Write("Speed: > 9999");
            yOffset++;

            Console.SetCursorPosition(x + LabelOffsetLeft, y + yOffset);
            if (monster.Meta.AP < MaxValue)
            {
                Console.Write($"AP : {(int)monster.Meta.AP:0}");
            }
            else Console.Write("AP : > 9999");

            Console.SetCursorPosition(x + LableOffsetRight, y + yOffset);
            if (monster.Meta.DP < MaxValue)
            {
                Console.Write($"DP   : {(int)monster.Meta.DP:0}");
            }
            else Console.Write("DP   : > 9999");
        }

        /// <summary>
        /// Draws an HP bar consisting of filled and unfilled segments.
        /// </summary>
        /// <param name="currentHP">The current HP value.</param>
        /// <param name="maxHP">The maximum HP value.</param>
        /// <param name="maxBar">Number of segments the bar should have.</param>
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

        /// <summary>
        /// Clears the numeric/stat text region inside a monster info box.
        /// </summary>
        /// <param name="x">Top-left X position of the info box.</param>
        /// <param name="y">Top-left Y position of the info box.</param>
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

        /// <summary>
        /// Clears a rectangular area on the console by writing spaces.
        /// </summary>
        /// <param name="x">Top-left X position.</param>
        /// <param name="y">Top-left Y position.</param>
        /// <param name="width">Width of the area in characters.</param>
        /// <param name="height">Height of the area in characters.</param>
        private void ClearArea(int x, int y, int width, int height)
        {
            string blank = new string(' ', width);

            for (int row = 0; row < height; row++)
            {
                Console.SetCursorPosition(x, y + row);
                Console.Write(blank);
            }
        }

        /// <summary>
        /// Draws a rectangular frame for a monster info box using info-box symbols.
        /// </summary>
        /// <param name="x">Top-left X position of the frame.</param>
        /// <param name="y">Top-left Y position of the frame.</param>
        /// <param name="height">Height of the frame in rows.</param>
        /// <param name="width">Width of the frame in columns.</param>
        private void DrawMonsterBoxFrame(int x, int y, int height, int width)
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
        /// <summary>
        /// Writes static stat labels (HP, LVL, Speed, AP, DP) into a monster box.
        /// </summary>
        /// <param name="x">Top-left X position of the box.</param>
        /// <param name="y">Top-left Y position of the box.</param>
        private void DrawMonsterLabelStats(int x, int y)
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

        /// <summary>
        /// Updates the message box with information about an attack performed
        /// by the player (attacker) on the enemy (target).
        /// </summary>
        /// <param name="attacker">The monster performing the attack.</param>
        /// <param name="target">The monster receiving the attack.</param>
        /// <param name="usedSkill">The skill used during the attack.</param>
        /// <param name="finalDamage">Final damage dealt to the target.</param>
        /// <param name="triggeredEffect">
        /// Optional status effect that was triggered by the hit; null if none.
        /// </param>
        public void UpdateMessageBoxForAttack(MonsterBase attacker, MonsterBase target, SkillBase usedSkill, float finalDamage, StatusEffectBase? triggeredEffect)
        {
            // Position Outline
            int x = 20;
            int y = 23;

            const int FrameOffset = 1;
            const int InnerWidth = 78;
            const int InnerHeight = 4;

            int contentX = x + FrameOffset;
            int contentY = y + FrameOffset;
            int lineY = contentY;

            ClearArea(contentX, contentY, InnerWidth, InnerHeight);

            string line1 = $" {attacker.Race} attacks the {target.Race} using [{usedSkill.Name}].";
            string line2 = $" You dealt [{finalDamage:0}] damage.";
            string line3 = triggeredEffect != null ? $" The effect [{triggeredEffect.Name}] has been triggered." : "";
            string line4 = $" {_symbol.PointerSymbol} [Enter]";

            Console.SetCursorPosition(contentX, lineY);
            Console.Write(line1);
            lineY++;

            Console.SetCursorPosition(contentX, lineY);
            Console.Write(line2);
            lineY++;

            if (!string.IsNullOrEmpty(line3))
            {
                Console.SetCursorPosition(contentX, lineY);
                Console.Write(line3);
            }

            Console.SetCursorPosition(contentX + InnerWidth - line4.Length, contentY + InnerHeight - 1);
            Console.Write(line4);
        }

        /// <summary>
        /// Updates the message box with information about damage the player
        /// has taken from an enemy attack.
        /// </summary>
        /// <param name="attacker">The enemy monster dealing the damage.</param>
        /// <param name="defender">The player monster receiving the damage.</param>
        /// <param name="skill">The skill that caused the damage.</param>
        /// <param name="damage">Final damage value received.</param>
        /// <param name="triggeredEffect">
        /// Optional status effect that was applied to the defender; null if none.
        /// </param>
        public void UpdateMessageBoxForTakeDamage(MonsterBase attacker, MonsterBase defender, SkillBase skill, float damage, StatusEffectBase? triggeredEffect)
        {
            // Position Outline
            int x = 20;
            int y = 23;

            const int FrameOffset = 1;
            const int InnerWidth = 78;
            const int InnerHeight = 4;

            int contentX = x + FrameOffset;
            int contentY = y + FrameOffset;
            int lineY = contentY;


            ClearArea(contentX, contentY, InnerWidth, InnerHeight);


            string line1 = $" {attacker.Race} hits you using [{skill.Name}].";
            string line2 = $" You received [{damage:0}] damage.";

            string line3 = triggeredEffect != null ? $" The effect [{triggeredEffect.Name}] has been triggered on you." : string.Empty;

            string enterLine = $"{_symbol.PointerSymbol} [Enter]";

            Console.SetCursorPosition(contentX, lineY++);
            Console.Write(line1);

            Console.SetCursorPosition(contentX, lineY++);
            Console.Write(line2);

            if (!string.IsNullOrEmpty(line3))
            {
                Console.SetCursorPosition(contentX, lineY++);
                Console.Write(line3);
            }

            Console.SetCursorPosition(contentX + InnerWidth - enterLine.Length, contentY + InnerHeight - 1);
            Console.Write(enterLine);
        }

        /// <summary>
        /// Updates the message box to show detailed information about the
        /// currently selected skill in the skill box.
        /// </summary>
        /// <param name="skill">The skill which information should be displayed.</param>
        public void UpdateMessageBoxForChooseSkill(SkillBase skill)
        {
            // Position Outline
            int x = 20;
            int y = 23;

            const int FrameOffset = 1;
            const int InnerWidth = 78;
            const int InnerHeight = 4;

            int contentX = x + FrameOffset;
            int contentY = y + FrameOffset;
            int contentLineY = contentY;


            ClearArea(contentX, contentY, InnerWidth, InnerHeight);

            string line1 = $"Skill      : {skill.Name}   DamageType: {skill.DamageType}";
            string line2 = $"Power      : {(skill.Power * 100):0}%                   CD: {skill.Cooldown}";
            string line3 = $"Description: {skill.Description}";
            string line4 = $"{_symbol.PointerSymbol} [Enter]";

            Console.SetCursorPosition(contentX, contentLineY);
            Console.Write(line1);
            contentLineY++;

            Console.SetCursorPosition(contentX, contentLineY);
            Console.Write(line2);
            contentLineY++;
            Console.SetCursorPosition(contentX, contentLineY);
            Console.Write(line3);

            Console.SetCursorPosition(contentX + InnerWidth - line4.Length, contentY + InnerHeight - 1);
            Console.Write(line4);
        }

        /// <summary>
        /// Updates the skill box to show a list of monster races for selection
        /// and highlights the currently selected race.
        /// </summary>
        /// <param name="races">Array of available monster races.</param>
        /// <param name="pointerIndex">Index of the currently selected race.</param>
        public void UpdateMonsterSelectionBox(RaceType[] races, int pointerIndex)
        {
            // SkillBox Layout Position
            int x = 0;
            int y = 23;

            const int FrameOffset = 1;
            const int height = 6;
            const int width = 21;
            const int ClearWidth = width - FrameOffset * 2;
            const int ClearHeight = height - FrameOffset * 2;
            const int PointerOffset = 1;
            const int MaxShown = 4;

            int lineY = y + FrameOffset;
            int pointerX = x + EmptyOffset;
            int pointerY = y + EmptyOffset + pointerIndex;

            ClearArea(x + FrameOffset, y + FrameOffset, ClearWidth, ClearHeight);

            for (int i = 0; i < races.Length && i < MaxShown; i++)
            {
                Console.SetCursorPosition(x + FrameOffset + PointerOffset, lineY);
                Console.Write(races[i].ToString());
                lineY++;
            }

            Console.SetCursorPosition(pointerX, pointerY);
            Console.Write(_symbol.PointerSymbol);
        }

        /// <summary>
        /// Draws all required UI elements for the monster selection screen
        /// and displays the list of races.
        /// </summary>
        /// <param name="races">Array of available monster races.</param>
        /// <param name="pointerIndex">Index of the currently selected race.</param>
        public void ShowMonsterSelectionMenu(RaceType[] races, int pointerIndex)
        {
            PrintOutlineLayout();
            PrintSkillBoxLayout();
            PrintMessageBoxLayout();
            UpdateMonsterSelectionBox(races, pointerIndex);
        }

        /// <summary>
        /// Updates the message box with information about the currently
        /// highlighted monster choice (stats and description).
        /// </summary>
        /// <param name="race">The race of the highlighted monster.</param>
        /// <param name="meta">Meta stats of the highlighted monster.</param>
        /// <param name="description">Flavor text describing the monster.</param>
        public void UpdateMessageBoxForMonsterChoice(RaceType race, MonsterMeta meta, string description)
        {
            // Position der MessageBox
            int x = 20;
            int y = 23;

            const int FrameOffset = 1;
            const int InnerWidth = 78;
            const int InnerHeight = 4;

            int contentX = x + FrameOffset;
            int contentY = y + FrameOffset;
            int lineY = contentY;


            ClearArea(contentX, contentY, InnerWidth, InnerHeight);


            string line1 = $" Monster : {race}";
            string line2 = $" Stats   : HP {meta.MaxHP:0} | AP {meta.AP:0.0} | DP {meta.DP:0.0} | SPD {meta.Speed}";
            string line3 = $" Info    : {description}";
            string enterLine = $"{_symbol.PointerSymbol} [Enter]";

            Console.SetCursorPosition(contentX, lineY++);
            Console.Write(line1);

            Console.SetCursorPosition(contentX, lineY++);
            Console.Write(line2);

            Console.SetCursorPosition(contentX, lineY++);
            Console.Write(line3);

            Console.SetCursorPosition(contentX + InnerWidth - enterLine.Length, contentY + InnerHeight - 1);
            Console.Write(enterLine);
        }

        /// <summary>
        /// Updates the skill/stat choice box to show the four possible
        /// stat upgrade options (HP, AP, DP, Speed) and highlights the selection.
        /// </summary>
        /// <param name="pointerIndex">Index of the currently selected stat.</param>
        public void UpdateStatChoiceBox(int pointerIndex)
        {
            // Position wie SkillBox
            int x = 0;
            int y = 23;

            const int FrameOffset = 1;
            const int height = 6;
            const int width = 21;
            const int ClearWidth = width - FrameOffset * 2;
            const int ClearHeight = height - FrameOffset * 2;

            int lineY = y + FrameOffset;
            int pointerX = x + EmptyOffset;
            int pointerY = y + EmptyOffset + pointerIndex;

            ClearArea(x + FrameOffset, y + FrameOffset, ClearWidth, ClearHeight);

            string[] stats = { "Increase HP", "Increase AP", "Increase DP", "Increase Speed" };

            for (int i = 0; i < stats.Length; i++)
            {
                Console.SetCursorPosition(x + FrameOffset + EmptyOffset, lineY);
                Console.Write(stats[i]);
                lineY++;
            }

            Console.SetCursorPosition(pointerX, pointerY);
            Console.Write(_symbol.PointerSymbol);
        }

        /// <summary>
        /// Updates the message box to show a preview of the selected stat
        /// increase, including old and new values and a short description.
        /// </summary>
        /// <param name="stat">The stat type that may be increased.</param>
        /// <param name="player">The player monster whose stats are previewed.</param>
        public void UpdateMessageBoxForStatChoice(StatType stat, MonsterBase player)
        {
            // Position Outline (wie alle Message-Boxen)
            int x = 20;
            int y = 23;

            const int FrameOffset = 1;
            const int InnerWidth = 78;
            const int InnerHeight = 4;

            int contentX = x + FrameOffset;
            int contentY = y + FrameOffset;
            int lineY = contentY;

            ClearArea(contentX, contentY, InnerWidth, InnerHeight);

            string description = stat switch
            {
                StatType.MaxHP => "Increases maximum HP, improving survivability.",
                StatType.AP => "Increases Attack Power. Your attacks deal more damage.",
                StatType.DP => "Increases Defense Power. Reduces incoming damage.",
                StatType.Speed => "Increases Speed. Determines turn order and lets you attack first.",
                _ => "Unknown Stat."
            };

            float currentValue = stat switch
            {
                StatType.MaxHP => player.Meta.MaxHP,
                StatType.AP => player.Meta.AP,
                StatType.DP => player.Meta.DP,
                StatType.Speed => player.Meta.Speed,
                _ => 0
            };
            float newValue = stat switch
            {
                StatType.MaxHP => player.Meta.MaxHP + _balancing.StatIncrease_HP,
                StatType.AP => player.Meta.AP + _balancing.StatIncrease_AP,
                StatType.DP => player.Meta.DP + _balancing.StatIncrease_DP,
                StatType.Speed => player.Meta.Speed + _balancing.StatIncrease_Speed,
                _ => 0
            };

            string line1 = $" Stat      : {stat}";
            string line2 = $" Current   : {currentValue:0.0} -> {newValue}";
            string line3 = $" Effect    : {description}";
            string enterLine = $"{_symbol.PointerSymbol} [Enter]";

            Console.SetCursorPosition(contentX, lineY++);
            Console.Write(line1);

            Console.SetCursorPosition(contentX, lineY++);
            Console.Write(line2);

            Console.SetCursorPosition(contentX, lineY++);
            Console.Write(line3);

            Console.SetCursorPosition(contentX + InnerWidth - enterLine.Length,
                                      contentY + InnerHeight - 1);
            Console.Write(enterLine);
        }

        /// <summary>
        /// Updates the message box with condensed information about the last fight
        /// (win/lose state, fight number, total rounds).
        /// </summary>
        /// <param name="playerWon">True if the player won the fight.</param>
        /// <param name="fightNumber">The current fight number.</param>
        /// <param name="totalRounds">How many rounds the fight lasted.</param>
        public void UpdateMessageBoxRoundInfo(bool playerWon, int fightNumber, int totalRounds)
        {
            // Position Outline (wie alle Message-Boxen)
            int x = 20;
            int y = 23;

            const int FrameOffset = 1;
            const int InnerWidth = 78;
            const int InnerHeight = 4;

            int contentX = x + FrameOffset;
            int contentY = y + FrameOffset;
            int lineY = contentY;


            ClearArea(contentX, contentY, InnerWidth, InnerHeight);


            string line1 = playerWon
                ? $" You won the fight."
                : $" You lost the fight.";

            string line2 = $" Fight Nr: {fightNumber}   |   Rounds: {totalRounds}";

            string line3 = playerWon
                ? " Next enemy is ready to fight."
                : "";

            string enterLine = $"{_symbol.PointerSymbol} [Enter]";

            Console.SetCursorPosition(contentX, lineY++);
            Console.Write(line1);

            Console.SetCursorPosition(contentX, lineY++);
            Console.Write(line2);

            Console.SetCursorPosition(contentX, lineY++);
            Console.Write(line3);

            Console.SetCursorPosition(contentX + InnerWidth - enterLine.Length,
                                      contentY + InnerHeight - 1);
            Console.Write(enterLine);
        }
    }
}