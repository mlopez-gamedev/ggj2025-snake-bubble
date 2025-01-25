using MiguelGameDev.SnakeBubble.Items;
using UnityEngine;
using UnityEngine.Serialization;

namespace MiguelGameDev.SnakeBubble.Levels
{
    public class Level : MonoBehaviour
    {
        [SerializeField] private ItemsManager _itemsManager;
        [SerializeField] private Transform[] spawnTransform;
        
        public Transform[] SpawnTransform => spawnTransform;

        public void Init(int playersAmount)
        {
            _itemsManager.Init(playersAmount);
        }
    }
}