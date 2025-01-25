using DG.Tweening;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace MiguelGameDev.SnakeBubble.Menu
{
    public class TitleScreen : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private Button _startButton;
        [SerializeField] private Button _exitButton;

        [SerializeField] private GameManager _game;

        private MenuMediator _menuMediator;
        public void Setup(MenuMediator menuMediator)
        {
            _menuMediator = menuMediator;
        }
        
        private void Awake()
        {
            _startButton.gameObject.SetActive(true);
            _exitButton.gameObject.SetActive(true);
            _startButton.onClick.AddListener(OnStartButtonClicked);
            _exitButton.onClick.AddListener(OnExitButtonClicked);
        }

        private void Start()
        {
            _startButton.Select();
        }

        private void OnStartButtonClicked()
        {
            _startButton.gameObject.SetActive(false);
            _exitButton.gameObject.SetActive(false);
            _canvasGroup.interactable = false;
            
            //TODO: AUDIO
            _canvasGroup.DOFade(0, 0.1f).OnComplete(ShowSelectPlayer);

            void ShowSelectPlayer()
            {
                _menuMediator.ShowSelectPlayer();
            }
        }
        
        private void OnExitButtonClicked()
        {
            #if UNITY_EDITOR
            EditorApplication.isPlaying = false;
            return;
            #endif
            
            Application.Quit();
        }
    }
}
