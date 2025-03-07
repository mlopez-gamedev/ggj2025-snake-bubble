using MiguelGameDev.SnakeBubble.Items;
using MoreMountains.Feedbacks;
using UnityEngine;

namespace MiguelGameDev.SnakeBubble.Snake
{
    public class SnakeBody : SnakeSegment
    {
        private int _waypointIndex;
        private float _headTranslationLeft;
        public override int WaypointIndex => _waypointIndex;
        
        [SerializeField] private MMF_Player _spawnFeedback;
        
        internal void Init(int waypointIndex, float waitForHeadTranslation)
        {
            _waypointIndex = waypointIndex;
            _headTranslationLeft = waitForHeadTranslation;
            
            _spawnFeedback.PlayFeedbacks();
        }

        public void UpdatePosition(float translation)
        {
            translation = WaitForHeadTranslation(translation);
            if (translation <= 0)
            {
                return;
            }

            var waypoint = _head.GetWaypoint(_waypointIndex);
            var distance = Vector3.Distance(transform.position, waypoint.Position);
            if (distance < translation)
            {
                transform.position = waypoint.Position;
                NextWaypoint();
                transform.Translate(Vector3.up * (translation - distance));
                return;
            }
            
            transform.Translate(Vector3.up * translation);
        }

        private float WaitForHeadTranslation(float translation)
        {
            if (_headTranslationLeft <= 0)
            {
                return translation;
            }
            
            _headTranslationLeft -= translation;
            return -_headTranslationLeft;
        }

        private void NextWaypoint()
        {
            _waypointIndex++;
            var waypoint = _head.GetWaypoint(_waypointIndex);
            transform.rotation = waypoint.Rotation;
        }
    }
}