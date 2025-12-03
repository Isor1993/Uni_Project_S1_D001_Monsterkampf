/*****************************************************************************
* Project : Monsterkampf-Simulator (K1, S1, S4)
* File    : InputManager.cs
* Date    : 03.12.2025
* Author  : Eric Rosenberg
*
* Description :
*   Handles all player input processing for navigation and confirmation
*   inside selection menus such as stat increase selection. Converts
*   player commands into pointer movements and confirms choices.
*
* Responsibilities :
*   - Read player commands via IPlayerInput
*   - Process pointer navigation logic for stat selection
*   - Update UI based on current pointer index
*   - Wait for ENTER/Confirm events when required
*
* History :
*   03.12.2025 ER Created
******************************************************************************/

using S1_D001_Monsterkampf_Simulator_ER.Monsters;

namespace S1_D001_Monsterkampf_Simulator_ER.Managers
{
    internal class InputManager
    {
        private readonly IPlayerInput _playerInput;

        /// <summary>
        /// Creates a new InputManager instance used to process and interpret
        /// player input coming from the assigned IPlayerInput implementation.
        /// </summary>
        /// <param name="playerInput">
        /// The player's input source (keyboard, test input, mock input).
        /// </param>
        public InputManager(IPlayerInput playerInput)
        {
            _playerInput = playerInput;
        }

        /// <summary>
        /// Handles the interactive stat increase selection menu.
        /// Allows the player to navigate up and down using input commands
        /// and confirms the selected StatType with ENTER.
        /// </summary>
        /// <param name="ui">
        /// UIManager used to update pointer visuals and the stat info box.
        /// </param>
        /// <param name="player">
        /// The player's monster whose stats are previewed in the UI.
        /// </param>
        /// <returns>
        /// Returns the chosen StatType after the player confirms the decision.
        /// </returns>
        public StatType ReadStatIncreaseChoice(UIManager ui, MonsterBase player)
        {
            int pointer = 0;
            bool confirmed = false;

            ui.UpdateStatChoiceBox(pointer);
            ui.UpdateMessageBoxForStatChoice((StatType)pointer, player);

            while (!confirmed)
            {
                PlayerCommand cmd = _playerInput.ReadCommand();

                switch (cmd)
                {
                    case PlayerCommand.MoveUp: pointer = Math.Max(0, pointer - 1); break;
                    case PlayerCommand.MoveDown: pointer = Math.Min(3, pointer + 1); break;
                    case PlayerCommand.Confirm: confirmed = true; break;
                }

                ui.UpdateStatChoiceBox(pointer);
                ui.UpdateMessageBoxForStatChoice((StatType)pointer, player);
            }

            return (StatType)pointer;
        }

        /// <summary>
        /// Waits until the player presses ENTER/Confirm. Used to pause message boxes
        /// or continue the flow only when explicitly acknowledged by the player.
        /// </summary>
        /// <param name="input">
        /// Input source used to read confirm commands.
        /// </param>
        public void WaitForEnter(IPlayerInput input)
        {
            while (true)
            {
                PlayerCommand cmd = input.ReadCommand();

                if (cmd == PlayerCommand.Confirm)
                    break;
            }
        }
    }
}