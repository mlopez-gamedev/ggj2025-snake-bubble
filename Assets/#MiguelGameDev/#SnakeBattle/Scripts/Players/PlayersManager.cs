using System.Collections.Generic;
using MiguelGameDev.SnakeBubble.Items;
using MiguelGameDev.SnakeBubble.Snake;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace MiguelGameDev.SnakeBubble.Players
{
    public class PlayersManager : MonoBehaviour
    {
        [SerializeField] private PlayerInputManager _playerInputManager;
        [SerializeField] private PlayerDependencies[] _playerDependencies;
        [SerializeField] private SnakeHead _playerPrefab;

        private List<Player> _players;
        
        private void Awake()
        {
            _players = new List<Player>();
            _playerInputManager.onPlayerJoined += OnPlayerJoined;
        }

        private void Start()
        {
            _playerInputManager.JoinPlayer(0, -1, "Menu");
            _playerInputManager.DisableJoining();
        }

        private void EnableJoining()
        {
            _playerInputManager.EnableJoining();
        }

        private void OnPlayerJoined(PlayerInput playerInput)
        {
            var player = playerInput.GetComponent<Player>();
            player.Setup(_playerPrefab, _playerDependencies);
            _players.Add(player);
        }

        public void StartGame()
        {
            _playerInputManager.DisableJoining();
            foreach (var player in _players)
            {
                player.StartGame();
            }
        }
    }
}