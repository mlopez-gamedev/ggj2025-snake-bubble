using System;
using DG.Tweening;
using MiguelGameDev.SnakeBubble.Players;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MiguelGameDev.SnakeBubble.Menu
{
    public class SelectPlayerScreen : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private PlayerSelection[] _playersSelection;
        [SerializeField] private PlayersManager _playersManager;
        
        private MenuMediator _menuMediator;

        [ShowInInspector, HideInEditorMode] private int _playersJoined = 0;
        [ShowInInspector, HideInEditorMode] private int _playersReady = 0;
        
        public void Setup(MenuMediator menuMediator)
        {
            _menuMediator = menuMediator;
            foreach (var playerSelection in _playersSelection)
            {
                playerSelection.Setup(this);
            }
        }

        public void Show()
        {
            _canvasGroup.gameObject.SetActive(true);
            _playersJoined = _playersManager.PlayerAmount;
        }

        public void PlayerJoin(Player player)
        {
            if (player.PlayerIndex >= _playersSelection.Length)
            {
                return;
            }
            _playersSelection[player.PlayerIndex].Join(player);
            ++_playersJoined;
        }
        
        public void PlayerReady()
        {
            ++_playersReady;
            if (/*_playersReady == _playersJoined && */_playersReady == _playersManager.MaxPlayers)
            {
                _menuMediator.StartGame();
            }
        }

        public Tween Hide(float duration)
        {
            return _canvasGroup.DOFade(0f, duration)
                .OnComplete(Disable);

            void Disable()
            {
                gameObject.SetActive(false);
            }
        }
    }
}