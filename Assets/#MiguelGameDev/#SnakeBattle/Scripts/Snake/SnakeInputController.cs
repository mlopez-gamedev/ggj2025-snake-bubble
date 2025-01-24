using UnityEngine;
using UnityEngine.InputSystem;

namespace MiguelGameDev.SnakeBubble.Snake
{
    public class SnakeInputController : MonoBehaviour
    {
        [SerializeField] private PlayerInput _playerInput;

        private InputAction _turnLeftAction;
        private InputAction _turnRightAction;
        private bool _moveActionPerformed;
        
        private void Start()
        {
            _turnLeftAction = _playerInput.actions.FindAction("TurnLeft");
            _turnRightAction = _playerInput.actions.FindAction("TurnRight");
        }

        internal int CheckDirection()
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
        
        internal bool CancelDirection()
        {
            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                return true;
            }

            return false;
        }
    }
}