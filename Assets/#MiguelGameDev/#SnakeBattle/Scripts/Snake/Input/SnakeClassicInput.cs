using System;
using UnityEngine.InputSystem;

namespace MiguelGameDev.SnakeBubble.Snake.Input
{
    public class SnakeClassicInput : SnakeInput
    {
        private PlayerInput _playerInput;

        private InputAction _turnLeftAction;
        private InputAction _turnRightAction;
        private bool _moveActionPerformed;
        
        public SnakeClassicInput(PlayerInput playerInput)
        {
            _playerInput = playerInput;
            _playerInput.SwitchCurrentActionMap("Game/Classic");
            _turnLeftAction = _playerInput.actions.FindAction("TurnLeft");
            _turnRightAction = _playerInput.actions.FindAction("TurnRight");
        }

        public int CheckDirection()
        {
            if (!_turnLeftAction.IsPressed() && !_turnRightAction.IsPressed())
            {
                _moveActionPerformed = false;
                return 0;
            }

            var move = 0;
            if (_turnLeftAction.IsPressed())
            {
                move -= 1;
            }
            
            if (_turnRightAction.IsPressed())
            {
                move += 1;
            }

            switch (move)
            {
                case -1 when _moveActionPerformed:
                    return 0;
                case -1:
                    _moveActionPerformed = true;
                    return -1;
                case 1 when _moveActionPerformed:
                    return 0;
                case 1:
                    _moveActionPerformed = true;
                    return 1;
                default:
                    _moveActionPerformed = false;
                    return 0;
            }
        }
    }
}