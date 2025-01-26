using MiguelGameDev.SnakeBubble.Snake;
using UnityEngine;

namespace MiguelGameDev.SnakeBubble.Projectiles
{
    [RequireComponent(typeof(Collider2D))]
    public class ProjectileCollider : MonoBehaviour
    {
        private int _playerLayer;
        private Projectile _projectile;
        
        public void Setup(Projectile projectile)
        {
            _projectile = projectile;
            _playerLayer = LayerMask.NameToLayer("Player");
        }
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.layer == _playerLayer)
            {
                CollideWithPlayer(other.gameObject.GetComponent<SnakeCollider>());
            }
            _projectile.Explode().Preserve();
        }

        private void CollideWithPlayer(SnakeCollider snakeCollider)
        {
            if (snakeCollider.PlayerIndex != _projectile.PlayerIndex)
            {
                snakeCollider.Hit();
            }
        }
    }
}