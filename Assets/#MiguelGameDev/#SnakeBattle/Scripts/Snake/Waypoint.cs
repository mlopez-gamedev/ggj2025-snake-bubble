using Sirenix.OdinInspector;
using UnityEngine;

namespace MiguelGameDev.SnakeBubble.Snake
{
    public struct Waypoint
    {
        [ShowInInspector] public Vector3 Position { get; }
        [ShowInInspector] public Quaternion Rotation { get; }

        public Waypoint(Vector3 position, Quaternion rotation)
        {
            Position = position;
            Rotation = rotation;
        }
    }
}