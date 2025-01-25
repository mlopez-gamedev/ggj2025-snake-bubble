using Cysharp.Threading.Tasks;
using DG.Tweening;
using MiguelGameDev.SnakeBubble.Snake;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MiguelGameDev.SnakeBubble.Menu
{
    public class EndGameScreen : MonoBehaviour
    {
        [SerializeField] private SnakeConfig _config;
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private TMP_Text _playerWinnerText;
        [SerializeField] private TMP_Text _continueText;

        private bool _enableSubmit;

        private void Awake()
        {
            _continueText.gameObject.SetActive(false);
        }
        
        public void ShowWinner(int winner)
        {
            _playerWinnerText.color = _config.GetColorByIndex(winner).Value;
            _playerWinnerText.text = $"Player {winner} wins!!!";
            
            _canvasGroup.gameObject.SetActive(true);
            _canvasGroup.DOFade(1f, 0.5f).OnComplete(EnableSubmit);

            async void EnableSubmit()
            {
                await UniTask.Delay(5000);
                _enableSubmit = true;
                _continueText.gameObject.SetActive(true);
            }
        }

        private void Update()
        {
            if (_enableSubmit && Input.anyKeyDown)
            {
                _enableSubmit = false;
                SceneManager.LoadScene(0);
            }
        }
    }
}