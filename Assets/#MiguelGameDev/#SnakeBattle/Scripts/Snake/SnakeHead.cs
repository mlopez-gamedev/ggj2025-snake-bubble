using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using MoreMountains.Feedbacks;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace MiguelGameDev.SnakeBubble.Snake
{
    public class SnakeHead : SnakeSegment
    {
        [Header("Components")]
        [SerializeField] private PlayerInput _playerInput;
        [SerializeField] private SnakeHeadCollider _headCollider;
        [Header("Dependencies")]
        [SerializeField] private SnakeConfig _config;
        [SerializeField] private SnakeInputController _input;
        [SerializeField] private SnakeBody bodyPrefab;
        //[Header("Feeling")]
        //[SerializeField] private MMF_Player _crashFeedback;

        [ShowInInspector, HideInEditorMode] private float _speed;
        [ShowInInspector, HideInEditorMode] private readonly List<SnakeBody> _segments = new List<SnakeBody>();
        [ShowInInspector, HideInEditorMode] private readonly List<Waypoint> _waypoints = new List<Waypoint>();

        [ShowInInspector, HideInEditorMode] private Vector3 _desiredTurnPosition;
        [ShowInInspector, HideInEditorMode] private int _desiredTurn = 0;

        private float _growTranslationOffset = 0;
        
        private async void Start()
        {
            Setup();
            await Task.Delay(1000);
            Init();
        }

        private void Setup()
        {
            base.Setup(_playerInput.playerIndex, 0);
            
            _headCollider.Setup(this);
            
            for (int i = 1; i <= _config.StartSegments; i++)
            {
                var segment = Instantiate(
                    bodyPrefab,
                    transform.position - transform.up * i,
                    transform.rotation);

                segment.Setup(this, PlayerIndex, i);
                _segments.Add(segment);
            }
        }
        
        private void Init()
        {
            _speed = _config.DefaultSpeed;
            // _waypoints.Add(new Waypoint(transform.position, transform.rotation));
            foreach (var segment in _segments)
            {
                segment.Init(0, 0);
            }
        }
        
        private void Update()
        {
            if (_speed == 0)
            {
                return;
            }
            UpdateInput();
            UpdatePosition();
        }

        private void UpdateInput()
        {
            var changeDirection = _input.CheckDirection();
            if (changeDirection == 0)
            {
                return;
            }

            _desiredTurn = changeDirection;
            _desiredTurnPosition = GetDesiredTurnPosition();
        }

        private Vector3 GetDesiredTurnPosition()
        {
            Vector3 position = transform.position;
            if (Mathf.Abs(transform.up.x) > 0.1f)
            {
                position.x = transform.up.x > 0 
                    ? Mathf.Ceil(position.x) 
                    : Mathf.Floor(position.x);
            }
            if (Mathf.Abs(transform.up.y) > 0.1f)
            {
                position.y = transform.up.y > 0 
                    ? Mathf.Ceil(position.y) 
                    : Mathf.Floor(position.y);
            }

            return position;
        }

        private void ChangeDirection()
        {
            _waypoints.Add(new Waypoint(transform.position, transform.rotation));
            transform.Rotate(Vector3.forward, -90f * _desiredTurn);
            _desiredTurn = 0;
        }

        private void UpdatePosition()
        {
            var translation = Time.deltaTime * _speed;

            _growTranslationOffset = Mathf.Max(0, _growTranslationOffset - translation);
            
            TryTurnAndTranslate();

            foreach (var segment in _segments)
            {
                segment.UpdatePosition(translation);
            }

            void TryTurnAndTranslate()
            {
                if (_desiredTurn == 0)
                {
                    transform.Translate(Vector3.up * translation);
                    return;
                }
                
                var distance = Vector3.Distance(transform.position, _desiredTurnPosition);
                if (distance >= translation)
                {
                    transform.Translate(Vector3.up * translation);
                    return;
                }
                
                transform.position = _desiredTurnPosition;
                ChangeDirection();
                transform.Translate(Vector3.up * (translation - distance));
            }
        }
        
        public void Grow()
        {
            var lastSegment = LastSegment();
            var segment = Instantiate(
                bodyPrefab,
                lastSegment.transform.position,
                lastSegment.transform.rotation);

            segment.Setup(this, PlayerIndex, _segments.Count + 1);
            _segments.Add(segment);
            segment.Init(lastSegment.WaypointIndex, 1f + _growTranslationOffset);

            _growTranslationOffset += 1f;
        }
        
        private SnakeBody LastSegment()
        {
            return _segments[^1];
        }
        
        public Waypoint GetWaypoint(int index)
        {
            if (index == _waypoints.Count)
            {
                return new Waypoint(transform.position, transform.rotation);
            }
            
            Assert.IsTrue(index < _waypoints.Count, $"Invalid waypoint {index} of {_waypoints.Count}");
            return _waypoints[index];
        }

        public void CollideWithWall()
        {
            Crash();
        }
        
        public void CollideWithOther(SnakeCollider snakeCollider)
        {
            Crash();
        }
        
        public void CollideWithOwnBody(SnakeCollider snakeCollider)
        {
            Crash();
        }

        private async void Crash()
        {
            _speed = 0;
            await UniTask.Delay(1000);
            SceneManager.LoadScene(0);
        }
    }
}