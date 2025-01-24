using UnityEngine;

namespace MiguelGameDev.SnakeBubble.Snake
{
    public class SnakeSegment : MonoBehaviour
    {
        [SerializeField] private SnakeCollider _collider;
        
        private int _playerIndex;
        private int _segmentIndex;
        
        public int PlayerIndex => _playerIndex;
        public int SegmentIndex => _segmentIndex;
        
        protected void Setup(int playerIndex, int segmentIndex)
        {
            _playerIndex = playerIndex;
            _segmentIndex = segmentIndex;

            _collider.Setup(this);
        }
    }
}