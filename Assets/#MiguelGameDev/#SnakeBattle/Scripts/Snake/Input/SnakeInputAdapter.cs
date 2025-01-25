using UnityEngine;
using UnityEngine.InputSystem;

namespace MiguelGameDev.SnakeBubble.Snake.Input
{
    public class SnakeInputAdapter : MonoBehaviour
    {
        public enum EAdapter
        {
            Classic,
            Modern
        }

        private PlayerInput _playerInput;
        private SnakeInput _input;
        
        public void Setup(PlayerInput playerInput)
        {
            _playerInput = playerInput;
        }
        
        public void Init(EAdapter inputAdapter)
        {
            switch (inputAdapter)
            {
                case EAdapter.Classic:
                    _input = new SnakeClassicInput(_playerInput);
                    break;
                
                case EAdapter.Modern:
                    _input = new SnakeModernInput(_playerInput, transform);
                    break;
            }
        }

        public int CheckDirection()
        {
            return _input.CheckDirection();
        }
    }
}