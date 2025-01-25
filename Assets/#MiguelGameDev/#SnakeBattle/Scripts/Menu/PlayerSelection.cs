using System;
using MiguelGameDev.SnakeBubble.Players;
using MiguelGameDev.SnakeBubble.Snake;
using MiguelGameDev.SnakeBubble.Snake.Input;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MiguelGameDev.SnakeBubble.Menu
{
    public class PlayerSelection : MonoBehaviour
    {
        [SerializeField] private SnakeConfig _config;
        [SerializeField] private Image _joinedGroup;
        [SerializeField] private Image _joinedGroupStroke;
        
        [SerializeField] private Image _readyGroup;
        [SerializeField] private Image _readyGroupStroke;

        [SerializeField] private Button _readyWithClassicControlButton;
        [SerializeField] private Button _readyWithModernControlButton;
        
        private SelectPlayerScreen _selectPlayerScreen;
        private Player _joinedPlayer;

        private void Awake()
        {
            _readyWithClassicControlButton.onClick.AddListener(OnReadyWithClassicControlButtonClicked);
            _readyWithModernControlButton.onClick.AddListener(OnReadyWithModernControlButtonClicked);
        }

        private void Start()
        {
            if (_joinedPlayer == null)
            {
                return;
            }
            _joinedPlayer.EventSystem.SetSelectedGameObject(_readyWithClassicControlButton.gameObject);
        }

        public void Setup(SelectPlayerScreen selectPlayerScreen)
        {
            _selectPlayerScreen = selectPlayerScreen;
        }
        
        public void Join(Player player)
        {
            _joinedPlayer = player;
            var playerColor = _config.GetColorByIndex(player.PlayerIndex);
            _joinedGroup.color = playerColor.BackgroundColor;
            _joinedGroupStroke.color = playerColor.Value;
            
            _readyGroup.color = playerColor.BackgroundColor;
            _readyGroupStroke.color = playerColor.Value;
            
            _joinedGroup.gameObject.SetActive(true);

            if (gameObject.activeInHierarchy)
            {
                _joinedPlayer.EventSystem.SetSelectedGameObject(_readyWithClassicControlButton.gameObject);
                EventSystem.current.enabled = false;
            }
        }   

        private void OnReadyWithClassicControlButtonClicked()
        {
            PlayerReadyWith(SnakeInputAdapter.EAdapter.Classic);
        }
        
        private void OnReadyWithModernControlButtonClicked()
        {
            PlayerReadyWith(SnakeInputAdapter.EAdapter.Modern);
        }

        private void PlayerReadyWith(SnakeInputAdapter.EAdapter adapter)
        {
            _readyWithClassicControlButton.interactable = false;
            _readyWithModernControlButton.interactable = false;
            _joinedPlayer.SetInput(adapter);
            
            _readyGroup.gameObject.SetActive(true);
            
            _selectPlayerScreen.PlayerReady();
        }
    }
}