using System;
using MiguelGameDev.SnakeBubble.Snake;
using MiguelGameDev.SnakeBubble.Snake.Input;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

namespace MiguelGameDev.SnakeBubble.Players
{
    [RequireComponent(typeof(PlayerInput))]
    public class Player : MonoBehaviour
    {
        [SerializeField] private SnakeInputAdapter.EAdapter _inputAdapter;

        private PlayerDependencies _playerDependencies;
        private PlayerInput _playerInput;

        private SnakeHead _playerPrefab;
        private SnakeHead _playerSnake;
        
        public int PlayerIndex => _playerInput.playerIndex;

        public event Action<Player> OnDie;
        
        public MultiplayerEventSystem EventSystem => _playerDependencies.EventSystem;
        public void Setup(SnakeHead playerPrefab, PlayerDependencies[] playerDependencies)
        {
            _playerPrefab = playerPrefab;
            _playerInput = GetComponent<PlayerInput>();
            _playerDependencies = playerDependencies[_playerInput.playerIndex];

            _playerInput.uiInputModule = _playerDependencies.EventSystem.GetComponent<InputSystemUIInputModule>();
        }

        private void NotifyDie()
        {
            Debug.Log("Player Die");
            OnDie?.Invoke(this);
        }

        public void SetInput(SnakeInputAdapter.EAdapter inputAdapter)
        {
            _inputAdapter = inputAdapter;
        }

        public void StartGame(Transform spawnTransform)
        {
            _playerSnake = Instantiate(_playerPrefab);
            _playerSnake.Setup(_playerInput, spawnTransform, NotifyDie);
            _playerSnake.Init(_inputAdapter);
        }

        public void EndGame()
        {
            _playerSnake.Stop();
        }
    }
}