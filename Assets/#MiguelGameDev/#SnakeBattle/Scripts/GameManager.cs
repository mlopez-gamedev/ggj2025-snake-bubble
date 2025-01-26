using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using MiguelGameDev.SnakeBubble.Items;
using MiguelGameDev.SnakeBubble.Levels;
using MiguelGameDev.SnakeBubble.Menu;
using MiguelGameDev.SnakeBubble.Players;
using MoreMountains.Feedbacks;
using TMPro;
using UnityEngine;

namespace MiguelGameDev.SnakeBubble
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private PlayersManager _playersManager;
        [SerializeField] private MenuMediator _menuMediator;
        [SerializeField] private LevelsManager _levelsManager;
        [SerializeField] private MMF_Player _startGameFeedback;
        [SerializeField] private MMF_Player _endGameFeedback;

        private List<Player> _alivePlayers;
        
        public async UniTask StartGame()
        {
            _levelsManager.Init(_playersManager.PlayerAmount);
            _playersManager.Setup(_levelsManager.CurrentLevel);
            await _startGameFeedback.PlayFeedbacksTask();
            var players = _playersManager.StartGame();
            _alivePlayers = new List<Player>(players);
            foreach (var player in players)
            {
                player.OnDie += OnDiePlayer;
            }
        }
        
        private void OnDiePlayer(Player player)
        {
            _alivePlayers.Remove(player);

            if (_alivePlayers.Count == 1)
            {
                EndGame(_alivePlayers[0]).Preserve();
            }
        }

        private async UniTask EndGame(Player winner)
        {
            var tasks = new UniTask[]
            {
                _endGameFeedback.PlayFeedbacksTask().AsUniTask(),
                winner.Win()
            };
            await UniTask.WhenAll(tasks);
            _menuMediator.ShowWinner(winner.PlayerIndex);
        }
    }
}