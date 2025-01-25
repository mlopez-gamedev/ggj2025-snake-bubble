using UnityEngine;

namespace MiguelGameDev.SnakeBubble.Items
{
    public class ItemCollider : MonoBehaviour
    {
        [SerializeField] Collider2D _collider;
        
        private Item _item;

        public BubbleColor Color => _item.Color;
        
        public void Setup(Item item)
        {
            _item = item;
        }

        public void Eaten()
        {
            _collider.enabled = false;
            _item.Eaten();
        }
    }
}