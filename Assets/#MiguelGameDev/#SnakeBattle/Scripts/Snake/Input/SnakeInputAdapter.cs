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
        
        [SerializeField] private EAdapter adapter;

        private PlayerInput _playerInput;
        private SnakeInput _input;
        
        private void Awake()
        {
            _playerInput = GetComponent<PlayerInput>();
            
            switch (adapter)
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