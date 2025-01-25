using System;
using UnityEngine;
using MiguelGameDev.SnakeBubble.Items;

namespace MiguelGameDev.SnakeBubble.Snake
{
    public class SnakeHeadCollider : MonoBehaviour
    {
        private int _playerLayer;
        private int _wallLayer;
        private int _itemLayer;

        private SnakeHead _head;
        
        private int PlayerIndex => _head.PlayerIndex;
        private int SegmentIndex => _head.SegmentIndex;
        
        private void Awake()
        {
            _playerLayer = LayerMask.NameToLayer("Player");
            _wallLayer = LayerMask.NameToLayer("Wall");
            _itemLayer = LayerMask.NameToLayer("Item");
        }
        
        public void Setup(SnakeHead head)
        {
            _head = head;
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.layer == _wallLayer)
            {
                CollideWithWall();
                return;
            }
            
            if (other.gameObject.layer == _playerLayer)
            {
                CollideWithPlayer(other.gameObject.GetComponent<SnakeCollider>());
                return;
            }
            
            if (other.gameObject.layer == _itemLayer)
            {
                CollideWithItem(other.gameObject.GetComponent<ItemCollider>());
                return;
            }
        }

        private void CollideWithPlayer(SnakeCollider collider)
        {
            if (collider.PlayerIndex != PlayerIndex)
            {
                _head.CollideWithOther(collider);
                return;
            }
            
            if (IsCollidingWithNeighbour())
            {
                return;
            }
            
            _head.CollideWithOwnBody(collider);
            
            bool IsCollidingWithNeighbour()
            {
                return Math.Abs(collider.SegmentIndex - SegmentIndex) == 1;
            }
        }

        private void CollideWithItem(ItemCollider item)
        {
            _head.CollideWithItem(item);
        }

        private void CollideWithWall()
        {
            _head.CollideWithWall();
        }
    }
}