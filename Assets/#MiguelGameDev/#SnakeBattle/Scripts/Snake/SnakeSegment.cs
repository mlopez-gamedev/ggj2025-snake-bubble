using Cysharp.Threading.Tasks;
using Lofelt.NiceVibrations;
using MiguelGameDev.SnakeBubble.Items;
using MoreMountains.Feedbacks;
using MoreMountains.FeedbacksForThirdParty;
using UnityEngine;

namespace MiguelGameDev.SnakeBubble.Snake
{
    public abstract class SnakeSegment : MonoBehaviour
    {
        [SerializeField] private SnakeCollider _collider;
        [SerializeField] private SpriteRenderer _renderer;
        [Header("Feedback")]
        [SerializeField] private ParticleSystem _bubbleParticles;
        [SerializeField] private MMF_Player _explodeFeedback;
        
        protected SnakeHead _head;
        private int _playerIndex;
        private int _segmentIndex;
                
        private BubbleColor _color;
        
        public int PlayerIndex => _playerIndex;
        public int SegmentIndex => _segmentIndex;
        public abstract int WaypointIndex { get; }

        public BubbleColor Color
        {
            get => _color;
            protected set
            {
                _color = value;
                _bubbleParticles.startColor = _color.Value;
                _renderer.color = _color.Value;
            } 
        }
        
        public void Setup(SnakeHead head, int playerIndex, int segmentIndex, BubbleColor color)
        {
            _head = head;
            _playerIndex = playerIndex;
            _segmentIndex = segmentIndex;
            Color = color;

            SetHapticsGamepad();
            
            _collider.Setup(_head, this);
        }

        private void SetHapticsGamepad()
        {
            var haptics = _explodeFeedback.GetFeedbackOfType<MMF_NVContinuous>();
            if (haptics == null)
            {
                return;
            }
            haptics.HapticSettings.GamepadID = _playerIndex;
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