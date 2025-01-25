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
        
        protected SnakeHead _head;
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
        
        public void Setup(SnakeHead head, int playerIndex, int segmentIndex, BubbleColor color)
        {
            _head = head;
            _playerIndex = playerIndex;
            _segmentIndex = segmentIndex;
            Color = color;
            
            _collider.Setup(_head, this);
        }
        
        public async UniTask Explode(int delay = 0)
        {
            _collider.Stop();
            if (delay > 0)
            {
                await UniTask.Delay(delay);
            }
            await _explodeFeedback.PlayFeedbacksTask();
            Destroy(gameObject);
        }
    }
}