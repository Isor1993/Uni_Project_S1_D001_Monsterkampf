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

using S1_D001_Monsterkampf_Simulator_ER.Balancing;
using S1_D001_Monsterkampf_Simulator_ER.Monsters;
using S1_D001_Monsterkampf_Simulator_ER.Skills;
using S1_D001_Monsterkampf_Simulator_ER.Systems.StatusEffects;
using System.ComponentModel;
using System.Numerics;

namespace S1_D001_Monsterkampf_Simulator_ER.Managers
{
    internal class UIManager
    {
        private readonly SymbolManager _symbol;
        private readonly DiagnosticsManager _diagnostics;
        private readonly MonsterBalancing _balancing;


        public int PlayerPositionX => 20;
        public int PlayerPositionY => 3;
        public int EnemyPositionX => 63;
        public int EnemyPositionY => 3;

        const int EmptyOffset = 1;


        public UIManager(SymbolManager symbol, DiagnosticsManager diagnostics, MonsterBalancing balancing)
        {
            _symbol = symbol;
            _diagnostics = diagnostics;
            _balancing = balancing;

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
        public void UpdateMonsterBoxPlayer(MonsterBase monster)
        {
            // Position Outline
            int x = 20;
            int y = 3;

            UpdateMonsterLabelStats(x, y, monster);
        }
        public void UpdateMonsterBoxEnemy(MonsterBase monster)
        {
            // Position Outline
            int x = 63;
            int y = 3;

            UpdateMonsterLabelStats(x, y, monster);
        }
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
        private void ClearArea(int x, int y, int width, int height)
        {
            string blank = new string(' ', width);

            for (int row = 0; row < height; row++)
            {
                Console.SetCursorPosition(x, y + row);
                Console.Write(blank);
            }
        }
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

        public void UpdateMessageBoxForAttack(MonsterBase attacker, MonsterBase target, SkillBase usedSkill, float finalDamage, StatusEffectBase? triggeredEffect)
        {
            // Position Outline
            int x = 20;
            int y = 23;
            // === Innenbereich bestimmen ===
            const int FrameOffset = 1;
            const int InnerWidth = 78;
            const int InnerHeight = 4;

            int contentX = x + FrameOffset;
            int contentY = y + FrameOffset;
            int lineY = contentY;

            // === Clear old content ===
            ClearArea(contentX, contentY, InnerWidth, InnerHeight);

            // === Text vorbereiten ===
            string line1 = $" {attacker.Race} attacks the {target.Race} using [{usedSkill.Name}].";
            string line2 = $" You dealt [{finalDamage:0}] damage.";
            string line3 = triggeredEffect != null ? $" The effect [{triggeredEffect.Name}] has been triggered." : "";
            string line4 = $" {_symbol.PointerSymbol} [Enter]";

            // === Zeichnen ===
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

            // Enter unten rechts
            Console.SetCursorPosition(contentX + InnerWidth - line4.Length, contentY + InnerHeight - 1);
            Console.Write(line4);

        }



        public void UpdateMessageBoxForTakeDamage(MonsterBase attacker, MonsterBase defender, SkillBase skill, float damage, StatusEffectBase? triggeredEffect)
        {
            // Position Outline
            int x = 20;
            int y = 23;
            // Innenbereich der MessageBox bestimmen
            const int FrameOffset = 1;
            const int InnerWidth = 78;
            const int InnerHeight = 4;

            int contentX = x + FrameOffset;
            int contentY = y + FrameOffset;
            int lineY = contentY;

            // Box leeren
            ClearArea(contentX, contentY, InnerWidth, InnerHeight);

            // Textzeilen
            string line1 = $" {attacker.Race} hits you using [{skill.Name}].";
            string line2 = $" You received [{damage:0}] damage.";

            string line3 = triggeredEffect != null ? $" The effect [{triggeredEffect.Name}] has been triggered on you." : string.Empty;

            string enterLine = $"{_symbol.PointerSymbol} [Enter]";

            // Schreiben
            Console.SetCursorPosition(contentX, lineY++);
            Console.Write(line1);

            Console.SetCursorPosition(contentX, lineY++);
            Console.Write(line2);

            if (!string.IsNullOrEmpty(line3))
            {
                Console.SetCursorPosition(contentX, lineY++);
                Console.Write(line3);
            }

            // Enter unten rechts
            Console.SetCursorPosition(contentX + InnerWidth - enterLine.Length, contentY + InnerHeight - 1);
            Console.Write(enterLine);
        }

        public void UpdateMessageBoxForChooseSkill(SkillBase skill)
        {
            // Position Outline
            int x = 20;
            int y = 23;
            // Innenbereich der MessageBox bestimmen
            const int FrameOffset = 1;
            const int InnerWidth = 78;   // 80 Gesamtbreite - 2 Rahmen
            const int InnerHeight = 4;   // 6 Gesamt - 2 Rahmen

            int contentX = x + FrameOffset;
            int contentY = y + FrameOffset;
            int contentLineY = contentY;

            // alten Inhalt löschen
            ClearArea(contentX, contentY, InnerWidth, InnerHeight);

            // Schritt 2: hier kommen gleich Description / Cooldown / Power hin
            string line1 = $"Skill      : {skill.Name}   DamageType: {skill.DamageType}";
            string line2 = $"Power      : {(skill.Power * 100):0}%                   CD: {skill.Cooldown}";
            string line3 = $"Description: {skill.Description}";
            string line4 = $"{_symbol.PointerSymbol} [Enter]";

            // Schreiben
            Console.SetCursorPosition(contentX, contentLineY);
            Console.Write(line1);
            contentLineY++;

            Console.SetCursorPosition(contentX, contentLineY);
            Console.Write(line2);
            contentLineY++;
            Console.SetCursorPosition(contentX, contentLineY);
            Console.Write(line3);

            // rechts unten:
            Console.SetCursorPosition(contentX + InnerWidth - line4.Length, contentY + InnerHeight - 1);
            Console.Write(line4);
        }
        /// <summary>
        /// Shows 4 selectable monsters in the skill-box area using the same UI layout.
        /// Pointer moves vertically exactly like skill selection.
        /// </summary>
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
            const int MaxShown = 4; // wie Skills

            int lineY = y + FrameOffset;
            int pointerX = x + EmptyOffset;
            int pointerY = y + EmptyOffset + pointerIndex;

            // Alten Text löschen
            ClearArea(x + FrameOffset, y + FrameOffset, ClearWidth, ClearHeight);

            // 4 Monster-Namen anzeigen
            for (int i = 0; i < races.Length && i < MaxShown; i++)
            {
                Console.SetCursorPosition(x + FrameOffset + PointerOffset, lineY);
                Console.Write(races[i].ToString());
                lineY++;
            }

            // Pointer zeichnen
            Console.SetCursorPosition(pointerX, pointerY);
            Console.Write(_symbol.PointerSymbol);
        }

        /// <summary>
        /// Shows the monster selection menu (simple console menu).
        /// </summary>
        public void ShowMonsterSelectionMenu(RaceType[] races, int pointerIndex)
        {
            PrintOutlineLayout();
            PrintSkillBoxLayout();
            PrintMessageBoxLayout();
            //TODO alle monstersprites printen 
            UpdateMonsterSelectionBox(races, pointerIndex);

        }
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

            // Clear old content
            ClearArea(contentX, contentY, InnerWidth, InnerHeight);

            // Text vorbereiten
            string line1 = $" Monster : {race}";
            string line2 = $" Stats   : HP {meta.MaxHP:0} | AP {meta.AP:0.0} | DP {meta.DP:0.0} | SPD {meta.Speed}";
            string line3 = $" Info    : {description}";
            string enterLine = $"{_symbol.PointerSymbol} [Enter]";

            // Schreiben
            Console.SetCursorPosition(contentX, lineY++);
            Console.Write(line1);

            Console.SetCursorPosition(contentX, lineY++);
            Console.Write(line2);

            Console.SetCursorPosition(contentX, lineY++);
            Console.Write(line3);

            // Enter unten rechts
            Console.SetCursorPosition(contentX + InnerWidth - enterLine.Length, contentY + InnerHeight - 1);
            Console.Write(enterLine);

        }

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

            // 1. Innenbereich löschen
            ClearArea(x + FrameOffset, y + FrameOffset, ClearWidth, ClearHeight);

            // 2. Stat-Namen anzeigen
            string[] stats = { "Increase HP", "Increase AP", "Increase DP", "Increase Speed" };

            for (int i = 0; i < stats.Length; i++)
            {
                Console.SetCursorPosition(x + FrameOffset + EmptyOffset, lineY);
                Console.Write(stats[i]);
                lineY++;
            }

            // 3. Pointer setzen
            Console.SetCursorPosition(pointerX, pointerY);
            Console.Write(_symbol.PointerSymbol);
        }

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

            // Box leeren
            ClearArea(contentX, contentY, InnerWidth, InnerHeight);

            // Beschreibung pro Stat
            string description = stat switch
            {
                StatType.MaxHP => "Increases maximum HP, improving survivability.",
                StatType.AP => "Increases Attack Power. Your attacks deal more damage.",
                StatType.DP => "Increases Defense Power. Reduces incoming damage.",
                StatType.Speed => "Increases Speed. Determines turn order and lets you attack first.",
                _ => "Unknown Stat."
            };

            // Aktuellen Wert holen
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

            // Textzeilen
            string line1 = $" Stat      : {stat}";
            string line2 = $" Current   : {currentValue:0.0} -> {newValue}";
            string line3 = $" Effect    : {description}";
            string enterLine = $"{_symbol.PointerSymbol} [Enter]";

            // Schreiben
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

            // Box leeren
            ClearArea(contentX, contentY, InnerWidth, InnerHeight);

            // Textzeilen
            string line1 = playerWon
                ? $" You won the fight."
                : $" You lost the fight.";

            string line2 = $" Fight Nr: {fightNumber}   |   Rounds: {totalRounds}";

            string line3 = playerWon
                ? " Next enemy is ready to fight."
                : "";

            string enterLine = $"{_symbol.PointerSymbol} [Enter]";

            // Schreiben
            Console.SetCursorPosition(contentX, lineY++);
            Console.Write(line1);

            Console.SetCursorPosition(contentX, lineY++);
            Console.Write(line2);

            Console.SetCursorPosition(contentX, lineY++);
            Console.Write(line3);

            // Enter rechts unten
            Console.SetCursorPosition(contentX + InnerWidth - enterLine.Length,
                                      contentY + InnerHeight - 1);
            Console.Write(enterLine);
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

        public void PrintSlimeE()
        {
            const int OffsetY = 10;
            const int OffsetX = 7;
            int x = EnemyPositionX + OffsetX;
            int y = EnemyPositionY + OffsetY;
            for (int i = 0; i < SlimeSpriteE.Length; i++)
            {
                Console.SetCursorPosition(x, y + i);
                Console.Write(SlimeSpriteE[i]);
            }
        }

        public void PrintSlimeP()
        {
            const int OffsetY = 10;
            const int OffsetX = 7;
            int x = PlayerPositionX + OffsetX;
            int y = PlayerPositionY + OffsetY;
            for (int i = 0; i < SlimeSpriteP.Length; i++)
            {
                Console.SetCursorPosition(x, y + i);
                Console.Write(SlimeSpriteP[i]);
            }

        }


    }

}
