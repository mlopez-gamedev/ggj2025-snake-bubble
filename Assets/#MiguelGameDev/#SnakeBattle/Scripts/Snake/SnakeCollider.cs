using UnityEngine;

namespace MiguelGameDev.SnakeBubble.Snake
{
    public class SnakeCollider : MonoBehaviour
    {
        private SnakeSegment _segment;
        
        public int PlayerIndex => _segment.PlayerIndex;
        public int SegmentIndex => _segment.SegmentIndex;
        
        public void Setup(SnakeSegment segment)
        {
            _segment = segment;
        }
    }
}