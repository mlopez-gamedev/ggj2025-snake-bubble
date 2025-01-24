using UnityEngine;

namespace MiguelGameDev.SnakeBubble.Snake
{
    [CreateAssetMenu(fileName = "SnakeConfig", menuName = "Snake/Config")]
    public class SnakeConfig : ScriptableObject
    {
        [SerializeField] private float _defaultSpeed;
        [SerializeField] private int _startSegments;
        
        public float DefaultSpeed => _defaultSpeed;
        public int StartSegments => _startSegments;
    }
}