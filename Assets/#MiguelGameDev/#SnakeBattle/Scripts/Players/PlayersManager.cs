using System.Collections.Generic;
using MiguelGameDev.SnakeBubble.Menu;
using MiguelGameDev.SnakeBubble.Snake;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MiguelGameDev.SnakeBubble.Players
{
    public class PlayersManager : MonoBehaviour
    {
        [SerializeField] private PlayerInputManager _playerInputManager;
        [SerializeField] private PlayerDependencies[] _playerDependencies;
        [SerializeField] private SelectPlayerScreen _selectPlayerScreen;
        [SerializeField] private SnakeHead _playerPrefab;

        private List<Player> _players;
        
        public int PlayerAmount => _players.Count;
        
        private void Awake()
        {
            _players = new List<Player>();
            _playerInputManager.onPlayerJoined += OnPlayerJoined;
            _playerInputManager.onPlayerLeft += OnPlayerLeft;
            _playerInputManager.EnableJoining();
        }

        private void Start()
        {
            //_playerInputManager.JoinPlayer(0, -1, "UI");
            _playerInputManager.EnableJoining();
        }

        public void EnableJoining()
        {
            _playerInputManager.EnableJoining();
        }

        private void OnPlayerJoined(PlayerInput playerInput)
        {
            var player = playerInput.GetComponent<Player>();
            player.Setup(_playerPrefab, _playerDependencies);
            _players.Add(player);
            _selectPlayerScreen.PlayerJoin(player);
        }
        
        private void OnPlayerLeft(PlayerInput playerInput)
        {
            Debug.Log($"{playerInput} left");
        }

        public Player[] StartGame()
        {
            _playerInputManager.DisableJoining();
            foreach (var player in _players)
            {
                player.StartGame();
            }
            
            return _players.ToArray();
        }
        
        private void OnDestroy()
        {
            _playerInputManager.onPlayerJoined -= OnPlayerJoined;
            _playerInputManager.onPlayerLeft -= OnPlayerLeft;
        }
    }
}