using MiguelGameDev.SnakeBubble.Items;
using UnityEngine;

namespace MiguelGameDev.SnakeBubble.Snake
{
    [CreateAssetMenu(fileName = "SnakeConfig", menuName = "Snake Bubble/Config")]
    public class SnakeConfig : ScriptableObject
    {
        [SerializeField] private float _defaultSpeed;
        [SerializeField] private float _speedReductionPerSegment;
        [SerializeField] private float _minSpeed;
        [SerializeField] private float _shotCooldown = 1f;
        [SerializeField] private int _startSegments;
        [SerializeField] private BubbleColor[] _colors;
        
        public float DefaultSpeed => _defaultSpeed;
        public float SpeedReductionPerSegment => _speedReductionPerSegment;
        public float MinSpeed => _minSpeed;
        public int StartSegments => _startSegments;
        public float ShotCooldown => _shotCooldown;
        
        public BubbleColor GetColorByIndex(int colorIndex)
        {
            return _colors[colorIndex];
        }
    }
}