using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using MiguelGameDev.SnakeBubble.Items;
using MiguelGameDev.SnakeBubble.Snake.Input;
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
        [SerializeField] private SnakeHeadCollider _headCollider;
        [Header("Dependencies")]
        [SerializeField] private SnakeConfig _config;
        [SerializeField] private SnakeInputAdapter _input;
        [SerializeField] private SnakeBody bodyPrefab;
        [SerializeField] private MMF_Player _winnerFeedback;
        [SerializeField] private MMF_Player _hitFeedback;
        //[Header("Feeling")]
        //[SerializeField] private MMF_Player _crashFeedback;

        private PlayerInput _playerInput;
        
        [ShowInInspector, HideInEditorMode] private float _speed;
        [ShowInInspector, HideInEditorMode] private readonly List<SnakeBody> _segments = new List<SnakeBody>();
        [ShowInInspector, HideInEditorMode] private readonly List<Waypoint> _waypoints = new List<Waypoint>();

        [ShowInInspector, HideInEditorMode] private Vector3 _desiredTurnPosition;
        [ShowInInspector, HideInEditorMode] private int _desiredTurn = 0;

        private bool _stopped;
        private float _growTranslationOffset = 0;

        private Action _onDieCallback;

        public override int WaypointIndex => 0;

        public void Setup(PlayerInput playerInput, Transform spawnTransform, Action onDieCallback)
        {
            _playerInput = playerInput;
            _onDieCallback = onDieCallback;
            base.Setup(this, _playerInput.playerIndex, 0, _config.GetColorByIndex(_playerInput.playerIndex));
            
            transform.position = spawnTransform.position;
            transform.rotation = spawnTransform.rotation;
            
            _headCollider.Setup(this);
            _input.Setup(_playerInput);
            
            for (int i = 1; i <= _config.StartSegments; i++)
            {
                var segment = Instantiate(
                    bodyPrefab,
                    transform.position - transform.up * i,
                    transform.rotation);

                segment.Setup(this, PlayerIndex, i, Color);
                _segments.Add(segment);
            }
        }
        
        public void Init(SnakeInputAdapter.EAdapter inputAdapter)
        {
            _input.Init(inputAdapter);

            // Test
            //if (_playerInput.playerIndex == 0)
            //{
            SetSpeed();
            //}

            // _waypoints.Add(new Waypoint(transform.position, transform.rotation));
            foreach (var segment in _segments)
            {
                segment.Init(0, 0);
            }
        }
        
        private void Update()
        {
            if (_stopped)
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
        
        private void Grow()
        {
            var lastSegment = LastSegment();
            var segment = Instantiate(
                bodyPrefab,
                lastSegment.transform.position,
                lastSegment.transform.rotation);

            segment.Setup(this, PlayerIndex, _segments.Count + 1, Color);
            _segments.Add(segment);
            segment.Init(lastSegment.WaypointIndex, 1f + _growTranslationOffset);
            SetSpeed();
            _growTranslationOffset += 1f;
        }

        public void Degrow()
        {
            if (_segments.Count == 0)
            {
                Crash().Preserve();
                return;
            }
            var lastSegment = LastBody();
            lastSegment.Explode().Preserve();
            _segments.Remove(lastSegment);
            SetSpeed();
        }

        public void Cut(int segmentIndex)
        {
            var realSegmentIndex = segmentIndex - 1;
            for (int i = realSegmentIndex; i < _segments.Count; i++)
            {
                int delay = (i - realSegmentIndex) * 40;
                _segments[i].Explode(delay).Preserve();
            }
            
            _segments.RemoveRange(realSegmentIndex, _segments.Count - realSegmentIndex);
            SetSpeed();
        }
        
        private SnakeBody LastBody()
        {
            return _segments[^1];
        }
        
        private SnakeSegment LastSegment()
        {
            if (_segments.Count == 0)
            {
                return this;
            }

            return LastBody();
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
            Crash().Preserve();
        }
        
        public void CollideWithItem(ItemCollider itemCollider)
        {
            itemCollider.Eaten();
            if (itemCollider.Color == Color)
            {
                Grow();    
            }
            else
            {
                Degrow();
            }
        }
        
        public void CollideWithOther(SnakeCollider snakeCollider)
        {
            if (snakeCollider.Hit())
            {
                Degrow();
                _hitFeedback.PlayFeedbacks();
            }
            
        }
        
        public void CollideWithOwnBody(SnakeCollider snakeCollider)
        {
            snakeCollider.Hit();
        }

        private async UniTask Crash()
        {
            Stop();
            _onDieCallback?.Invoke();
            
            var tasks = new UniTask[_segments.Count + 1];
            tasks[0] = Explode();
            
            for (int i = 1; i <= _segments.Count; i++)
            {
                tasks[i] = _segments[i - 1].Explode(i * 40);
            }
            
            await UniTask.WhenAll(tasks);
        }

        private void Stop()
        {
            _stopped = true;
            _speed = 0;
        }

        public async UniTask Win()
        {
            Stop();
            await _winnerFeedback.PlayFeedbacksTask();
        }

        private void SetSpeed()
        {
            if (_stopped)
            {
                return;
            }
            var desiredSpeed = _config.DefaultSpeed - _config.SpeedReductionPerSegment * _segments.Count;
            _speed = Mathf.Max(_config.MinSpeed, desiredSpeed);
        }
    }
}