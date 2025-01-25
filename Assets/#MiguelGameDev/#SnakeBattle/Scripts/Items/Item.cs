using Cysharp.Threading.Tasks;
using MoreMountains.Feedbacks;
using UnityEngine;

namespace MiguelGameDev.SnakeBubble.Items
{
    public class Item : MonoBehaviour
    {
        [SerializeField] private ItemCollider _collider;
        [SerializeField] private SpriteRenderer _renderer;
        [Header("Feedback")]
        [SerializeField] MMF_Player _spawnEffect;
        [SerializeField] MMF_Player _eatenEffect;
        
        private ItemsManager _manager;
        private BubbleColor _color;
        
        public BubbleColor Color => _color;

        private void Awake()
        {
            _collider.Setup(this);
        }
        public void Spawn(ItemsManager manager, BubbleColor color)
        {
            _manager = manager;
            _color = color;
            _renderer.color = _color.Value;
            _spawnEffect.PlayFeedbacks();
        }

        public async UniTask Eaten()
        {
            await _eatenEffect.PlayFeedbacksTask();
            
            _manager.ItemEaten(this);
            Destroy(gameObject);
        }
    }
}