using UnityEngine;

namespace MiguelGameDev.SnakeBubble.Levels
{
    public class LevelsManager : MonoBehaviour
    {
        [SerializeField] private Level[] _levelsPrefab;

        private Level _currentLevel;
        public Level CurrentLevel => _currentLevel;
        public void Init(int playersAmount)
        {
            _currentLevel = Instantiate(_levelsPrefab[Random.Range(0, _levelsPrefab.Length)]);
            _currentLevel.Init(playersAmount);
        }
    }
}