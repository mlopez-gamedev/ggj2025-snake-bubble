using System;
using MiguelGameDev.SnakeBubble.Menu;
using UnityEngine;
using UnityEngine.InputSystem.UI;

namespace MiguelGameDev.SnakeBubble.Players
{
    [Serializable]
    public class PlayerDependencies
    {
        [SerializeField] private Transform _spawmTransform;
        [SerializeField] private MultiplayerEventSystem _eventSystem;
        
        public Transform SpawmTransform => _spawmTransform;
        public MultiplayerEventSystem EventSystem => _eventSystem;
    }
}