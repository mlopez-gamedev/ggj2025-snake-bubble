using UnityEngine;

namespace MiguelGameDev.SnakeBubble.Items
{
    [CreateAssetMenu(menuName = "Snake Bubble/Bubble Color")]
    public class BubbleColor : ScriptableObject
    {
        [SerializeField] Color _value;
        public Color Value => _value;
    }
}