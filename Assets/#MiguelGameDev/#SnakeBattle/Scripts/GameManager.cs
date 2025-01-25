using System;
using MiguelGameDev.SnakeBubble.Items;
using MiguelGameDev.SnakeBubble.Players;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MiguelGameDev.SnakeBubble
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private PlayersManager _playersManager;
        [SerializeField] private ItemsManager _itemsManager;
        
        public void StartGame()
        {
            _itemsManager.Init();
            _playersManager.StartGame();
        }
    }
}