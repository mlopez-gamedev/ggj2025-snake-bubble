using MiguelGameDev.SnakeBubble.Snake;
using MiguelGameDev.SnakeBubble.Snake.Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MiguelGameDev.SnakeBubble.Players
{
    [RequireComponent(typeof(PlayerInput))]
    public class Player : MonoBehaviour
    {
        [SerializeField] private SnakeInputAdapter.EAdapter _inputAdapter;

        private PlayerDependencies _playerDependencies;
        private PlayerInput _playerInput;
        
        private SnakeHead _playerSnake;
        public void Setup(SnakeHead playerPrefab, PlayerDependencies[] playerDependencies)
        {
            _playerInput = GetComponent<PlayerInput>();
            
            _playerDependencies = playerDependencies[_playerInput.playerIndex];
            
            _playerSnake = Instantiate(playerPrefab);
            _playerSnake.Setup(_playerInput, _playerDependencies.SpawmTransform);
        }

        public void StartGame()
        {
            _playerSnake.Init(_inputAdapter);
        }
    }
}