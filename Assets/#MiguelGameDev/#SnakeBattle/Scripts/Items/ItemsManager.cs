using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace MiguelGameDev.SnakeBubble.Items
{
    public class ItemsManager : MonoBehaviour
    {
        [SerializeField] private Item _itemPrefab;
        [SerializeField] private Transform[] _startItemsPositions;

        private Camera _camera;
        private List<Item> _items;

        private void Awake()
        {
            _camera = Camera.main;
        }

        public void Init()
        {
            _items = new List<Item>();
            foreach (var startItemPosition in _startItemsPositions)
            {
                SpawnAt(startItemPosition.position);   
            }
        }
        
        private void Spawn()
        {
            for (int i = 0; i < 100; i++)
            {
                if (TryGetPosition(out Vector2 spawnPosition))
                {
                    SpawnAt(spawnPosition);
                    return;
                }
            }

            bool TryGetPosition(out Vector2 position)
            {
                position = new Vector2(
                    Random.Range(-17, 18),
                    Random.Range(-10, 11));

                var spawnCollider = Physics2D.OverlapCircle(position, 0.99f);
                
                Debug.Log($"Position: {position} - Collider: {spawnCollider}");
                
                return spawnCollider == null;
            }
        }

        private void SpawnAt(Vector2 position)
        {
            var item = Instantiate(_itemPrefab, position, Quaternion.identity);
            
            item.Spawn(this);
            _items.Add(item);
        }

        public void ItemEaten(Item item)
        {
            _items.Remove(item);
            Spawn();
        }
    }
}