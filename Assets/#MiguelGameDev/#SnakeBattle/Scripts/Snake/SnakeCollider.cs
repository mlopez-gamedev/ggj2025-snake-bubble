using System.Collections;
using UnityEngine;

namespace MiguelGameDev.SnakeBubble.Snake
{
    public class SnakeCollider : MonoBehaviour
    {
        private SnakeHead _head;
        private SnakeSegment _segment;

        private bool _canCollide;
        
        public int PlayerIndex => _segment.PlayerIndex;
        public int SegmentIndex => _segment.SegmentIndex;
        
        public void Setup(SnakeHead head, SnakeSegment segment)
        {
            _head = head;
            _segment = segment;
            _canCollide = true;
        }

        public void Stop()
        {
            _canCollide = false;
        }

        public bool Hit()
        {
            if (!_canCollide)
            {
                return false;
            }
            
            if (!gameObject.activeInHierarchy)
            {
                return false;
            }
            
            bool returnDamage = false;
            if (SegmentIndex > 0)
            {
                _head.Cut(SegmentIndex);
                returnDamage = true;
            }
            else
            {
                _head.Degrow();
            }
            
            StartCoroutine(CollideTemp());   
            return returnDamage;
        }

        private IEnumerator CollideTemp()
        {
            _canCollide = false;
            yield return new WaitForSeconds(0.5f);
            _canCollide = true;
        }
    }
}