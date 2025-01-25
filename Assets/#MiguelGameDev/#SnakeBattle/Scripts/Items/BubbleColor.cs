using UnityEngine;

namespace MiguelGameDev.SnakeBubble.Items
{
    [CreateAssetMenu(menuName = "Snake Bubble/Bubble Color")]
    public class BubbleColor : ScriptableObject
    {
        [SerializeField] Color _value = Color.white;
        
        [SerializeField] Color _backgroundColor = Color.white;
        public Color Value => _value;
        public Color BackgroundColor => _backgroundColor;
    }
}