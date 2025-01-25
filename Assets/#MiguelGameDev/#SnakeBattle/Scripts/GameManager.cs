using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using MiguelGameDev.SnakeBubble.Items;
using MiguelGameDev.SnakeBubble.Levels;
using MiguelGameDev.SnakeBubble.Menu;
using MiguelGameDev.SnakeBubble.Players;
using TMPro;
using UnityEngine;

namespace MiguelGameDev.SnakeBubble
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private PlayersManager _playersManager;
        [SerializeField] private MenuMediator _menuMediator;
        [SerializeField] private LevelsManager _levelsManager;

        private List<Player> _alivePlayers;
        
        public void StartGame()
        {
            _levelsManager.Init(_playersManager.PlayerAmount);
            var players = _playersManager.StartGame(_levelsManager.CurrentLevel);
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
            winner.EndGame();
            await UniTask.Delay(1000);
            _menuMediator.ShowWinner(winner.PlayerIndex);
        }
    }
}