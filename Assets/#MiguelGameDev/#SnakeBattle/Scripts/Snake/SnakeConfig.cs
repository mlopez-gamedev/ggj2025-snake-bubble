using MiguelGameDev.SnakeBubble.Items;
using UnityEngine;

namespace MiguelGameDev.SnakeBubble.Snake
{
    [CreateAssetMenu(fileName = "SnakeConfig", menuName = "Snake Bubble/Config")]
    public class SnakeConfig : ScriptableObject
    {
        [SerializeField] private float _defaultSpeed;
        [SerializeField] private int _startSegments;
        [SerializeField] private BubbleColor[] _colors;
        
        public float DefaultSpeed => _defaultSpeed;
        public int StartSegments => _startSegments;
        
        public BubbleColor GetColorByIndex(int colorIndex)
        {
            return _colors[colorIndex];
        }
    }
}