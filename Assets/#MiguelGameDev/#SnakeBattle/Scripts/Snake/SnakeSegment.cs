using Cysharp.Threading.Tasks;
using MiguelGameDev.SnakeBubble.Items;
using MoreMountains.Feedbacks;
using UnityEngine;

namespace MiguelGameDev.SnakeBubble.Snake
{
    public class SnakeSegment : MonoBehaviour
    {
        [SerializeField] private SnakeCollider _collider;
        [SerializeField] private SpriteRenderer _renderer;
        [Header("Feedback")]
        [SerializeField] private MMF_Player _explodeFeedback;
        
        private int _playerIndex;
        private int _segmentIndex;
                
        private BubbleColor _color;
        
        public int PlayerIndex => _playerIndex;
        public int SegmentIndex => _segmentIndex;

        public BubbleColor Color
        {
            get => _color;
            protected set
            {
                _color = value;
                _renderer.color = _color.Value;
            } 
        }
        
        protected void Setup(int playerIndex, int segmentIndex, BubbleColor color)
        {
            _playerIndex = playerIndex;
            _segmentIndex = segmentIndex;
            Color = color;
            
            _collider.Setup(this);
        }
        
        public async UniTask Explode(int delay = 0)
        {
            if (delay > 0)
            {
                await UniTask.Delay(delay);
            }
            await _explodeFeedback.PlayFeedbacksTask();
            Destroy(gameObject);
        }
    }
}