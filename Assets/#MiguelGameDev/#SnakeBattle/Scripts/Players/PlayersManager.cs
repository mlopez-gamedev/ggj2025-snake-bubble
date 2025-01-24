using UnityEngine;
using UnityEngine.InputSystem;

namespace MiguelGameDev.SnakeBubble.Players
{
    public class PlayersManager : MonoBehaviour
    {
        [SerializeField] private PlayerInputManager _playerInputManager;
        [SerializeField] private Transform[] _playerPositions;

        private void Start()
        {
            _playerInputManager.onPlayerJoined += OnPlayerJoined;
        }

        private void OnPlayerJoined(PlayerInput playerInput)
        {
            var spawnTransform = _playerPositions[playerInput.playerIndex];
            playerInput.transform.position = spawnTransform.position;
            playerInput.transform.rotation = spawnTransform.rotation;
        }
    }
}