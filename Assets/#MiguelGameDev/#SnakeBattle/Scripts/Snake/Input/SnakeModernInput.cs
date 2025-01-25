using UnityEngine;
using UnityEngine.InputSystem;

namespace MiguelGameDev.SnakeBubble.Snake.Input
{
    public class SnakeModernInput : SnakeInput
    {
        private PlayerInput _playerInput;
        private Transform _playerTransform;
        
        private InputAction _moveAction;
        private bool _moveActionPerformed;

        public SnakeModernInput(PlayerInput playerInput, Transform playerTransform)
        {
            _playerInput = playerInput;
            _playerTransform = playerTransform;
            
            _playerInput.SwitchCurrentActionMap("Game/Modern");
            _moveAction = _playerInput.actions.FindAction("Move");
        }
        
        public int CheckDirection()
        {
            if (!_moveAction.IsPressed())
            {
                _moveActionPerformed = false;
                return 0;
            }

            var moveAction = _moveAction.ReadValue<Vector2>();
            var move = 0;
            if (_playerTransform.up == Vector3.up)
            {
                move = GetMove(moveAction.x, false);
            }
            else if (_playerTransform.up == Vector3.down)
            {
                move = GetMove(moveAction.x, true);
            }
            else if (_playerTransform.up == Vector3.left)
            {
                move = GetMove(moveAction.y, false);
            }
            else if (_playerTransform.up == Vector3.right)
            {
                move = GetMove(moveAction.y, true);
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

        private int GetMove(float value, bool inverse)
        {
            if (value > 0.5f)
            {
                return inverse ? - 1 : 1;
            }
            if (value < -0.5f)
            {
                return inverse ? 1 : -1;
            }

            return 0;
        }
    }
}