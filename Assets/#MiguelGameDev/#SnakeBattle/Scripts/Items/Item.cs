using MoreMountains.Feedbacks;
using UnityEngine;

namespace MiguelGameDev.SnakeBubble.Items
{
    public class Item : MonoBehaviour
    {
        [SerializeField] private ItemCollider _collider;
        [SerializeField] MMF_Player _spawnEffect;
        [SerializeField] MMF_Player _eatenEffect;
        
        private ItemsManager _manager;

        private void Awake()
        {
            _collider.Setup(this);
        }
        public void Spawn(ItemsManager manager)
        {
            _manager = manager;
            _spawnEffect.PlayFeedbacks();
        }

        public async void Eaten()
        {
            await _eatenEffect.PlayFeedbacksTask();
            
            _manager.ItemEaten(this);
            Destroy(gameObject);
        }
    }
}