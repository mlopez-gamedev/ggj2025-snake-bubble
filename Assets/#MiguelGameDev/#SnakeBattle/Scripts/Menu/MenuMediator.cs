using DG.Tweening;
using UnityEngine;

namespace MiguelGameDev.SnakeBubble.Menu
{
    public class MenuMediator : MonoBehaviour
    {
        [SerializeField] CanvasGroup _background;
        [SerializeField] TitleScreen _titleScreen;
        [SerializeField] SelectPlayerScreen _selectPlayerScreen;
        [SerializeField] EndGameScreen _endGameScreen;
        
        [SerializeField] GameManager _gameManager;
        
        private void Awake()
        {
            Cursor.visible = false;

            _titleScreen.Setup(this);
            _selectPlayerScreen.Setup(this);
            
            _titleScreen.gameObject.SetActive(true);
            _selectPlayerScreen.gameObject.SetActive(false);
            _endGameScreen.gameObject.SetActive(false);
        }

        public void ShowSelectPlayer()
        {
         
            _selectPlayerScreen.Show();
        }

        public void ShowWinner(int winner)
        {
            _endGameScreen.ShowWinner(winner);
        }
        
        public void StartGame()
        {
            DOTween.Sequence()
                .Join(_selectPlayerScreen.Hide(1f))
                .Join(_background.DOFade(0, 1f))
                .OnComplete(InitGame);

            void InitGame()
            {
                _gameManager.StartGame().Preserve();
            }
        }
    }
}