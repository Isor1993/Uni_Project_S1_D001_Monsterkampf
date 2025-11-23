using S1_D001_Monsterkampf_Simulator_ER.Monsters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S1_D001_Monsterkampf_Simulator_ER.Managers
{
    internal class InputManager
    {
        private readonly IPlayerInput _playerInput;

        public InputManager(IPlayerInput playerInput)
        {
            _playerInput = playerInput;
        }

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
        public void WaitForEnter(IPlayerInput input)
        {
            while (true)
            {
                PlayerCommand cmd = input.ReadCommand();

                if (cmd == PlayerCommand.Confirm)
                    break;   // nur Enter beendet die MessageBox
            }
        }
    }
}