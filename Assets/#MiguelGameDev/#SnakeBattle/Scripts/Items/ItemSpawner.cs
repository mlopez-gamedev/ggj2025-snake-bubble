using UnityEngine;

namespace MiguelGameDev.SnakeBubble.Items
{
    public class ItemSpawner : MonoBehaviour
    {
        [SerializeField] private BubbleColor _color;
        public Vector3 Position => transform.position;
        public BubbleColor Color => _color;
    }
}