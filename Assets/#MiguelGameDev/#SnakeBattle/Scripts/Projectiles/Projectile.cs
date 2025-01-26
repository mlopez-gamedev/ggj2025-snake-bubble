using Cysharp.Threading.Tasks;
using MiguelGameDev.SnakeBubble.Items;
using MoreMountains.Feedbacks;
using UnityEngine;

namespace MiguelGameDev.SnakeBubble.Projectiles
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private float _speed = 16f;
        [SerializeField] private SpriteRenderer _renderer;
        [SerializeField] private ProjectileCollider _collider;
        [SerializeField] private MMF_Player _spawnFeedback;
        [SerializeField] private MMF_Player _explodeFeedback;

        private int _playerIndex;
        private BubbleColor _color;
        private bool _isDead;
        
        public int PlayerIndex => _playerIndex;
        
        public void Spawn(Transform shooterTransform, int playerIndex, BubbleColor color)
        {
            _playerIndex = playerIndex;
            _color = color;

            _renderer.color = _color.Value;
            
            transform.position = shooterTransform.position;
            transform.rotation = shooterTransform.rotation;
            
            _collider.Setup(this);
            
            _spawnFeedback.PlayFeedbacks();
        }
        
        private void Update()
        {
            if (_isDead)
            {
                return;
            }
            transform.Translate(Vector3.up * _speed * Time.deltaTime);
        }


        public async UniTask Explode()
        {
            _isDead = true;
            await _explodeFeedback.PlayFeedbacksTask();
            Destroy(this.gameObject);
        }
    }
}