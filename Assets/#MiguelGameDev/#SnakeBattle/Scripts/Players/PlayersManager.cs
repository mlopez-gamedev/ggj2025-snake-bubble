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
        [SerializeField] private Transform[] _playerPositions;

        private List<SnakeHead> _players;
        
        private void Start()
        {
            _players = new List<SnakeHead>();
            _playerInputManager.onPlayerJoined += OnPlayerJoined;
        }

        private void OnPlayerJoined(PlayerInput playerInput)
        {
            var player = playerInput.GetComponent<SnakeHead>();
            player.SetupAndInit(_playerPositions);
            
            _players.Add(player);
        }
    }
}