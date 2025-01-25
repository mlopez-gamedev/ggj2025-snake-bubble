using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace MiguelGameDev.SnakeBubble.Items
{
    public class ItemsManager : MonoBehaviour
    {
        [SerializeField] private Item _itemPrefab;
        [SerializeField] private ItemSpawner[] _startItemsPositions;

        private int _playersAmount;
        private Camera _camera;
        private List<Item> _items;

        private void Awake()
        {
            _camera = Camera.main;
        }

        public void Init(int playersAmount)
        {
            _playersAmount = playersAmount;
            _items = new List<Item>(_playersAmount);
            for (int i = 0; i < _playersAmount; ++i)
            {
                SpawnAt(_startItemsPositions[i].Position, _startItemsPositions[i].Color);   
            }

            StartCoroutine(SpawnCoroutine());
        }

        private IEnumerator SpawnCoroutine()
        {
            yield return new WaitForSeconds(10f);
            for (int i = 0; i < _playersAmount; ++i)
            {
                Spawn(_startItemsPositions[i].Color);   
            }
        }
        
        private void Spawn(BubbleColor color)
        {
            for (int i = 0; i < 100; i++)
            {
                if (TryGetPosition(out Vector2 spawnPosition))
                {
                    SpawnAt(spawnPosition, color);
                    return;
                }
            }

            bool TryGetPosition(out Vector2 position)
            {
                position = new Vector2(
                    Random.Range(-17, 18),
                    Random.Range(-10, 11));

                var spawnCollider = Physics2D.OverlapCircle(position, 0.99f);
                
                return spawnCollider == null;
            }
        }

        private void SpawnAt(Vector2 position, BubbleColor color)
        {
            var item = Instantiate(_itemPrefab, position, Quaternion.identity);
            
            item.Spawn(this, color);
            _items.Add(item);
        }

        public void ItemEaten(Item item)
        {
            _items.Remove(item);
            Spawn(item.Color);
        }
    }
}