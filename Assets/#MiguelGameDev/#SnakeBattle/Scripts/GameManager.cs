using System;
using MiguelGameDev.SnakeBubble.Items;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MiguelGameDev.SnakeBubble
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private PlayerInputManager _playerInputManager;
        [SerializeField] private ItemsManager _itemsManager;
        
        public void StartGame()
        {
            _playerInputManager.JoinPlayer(0, -1, "Game/ClassicControls");
            _itemsManager.Init();
        }
    }
}