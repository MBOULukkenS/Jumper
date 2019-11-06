using System;
using SmartData.SmartBool.Data;
using SmartData.SmartEvent;
using SmartData.SmartFloat.Data;
using UnityEngine;

namespace Jumper.Entities.Player
{
    public class Player : MonoBehaviour
    {
        public EventDispatcher PlayerDied;
        
        public PlayerMovement Movement;
        public PlayerCollisionDetector CollisionDetector;

        public Animator Animator;

        [SerializeField]
        private BoolVar _active;
        
        [SerializeField]
        private float _gameOverWaitTime = 5;

        private float GameOverWaitTime => _gameOverWaitTime / 1f;
        
        private float _gameOverTimer = -1;

        private Rigidbody2D _rigidbody2D;
        private static readonly int XVelocity = Animator.StringToHash("XVelocity");
        private static readonly int YVelocity = Animator.StringToHash("YVelocity");

        internal Rigidbody2D Rigidbody2D 
        {
            get
            {
                if (_rigidbody2D == null)
                    _rigidbody2D = GetComponent<Rigidbody2D>();
                return _rigidbody2D;
            }
        }
        public bool Active
        {
            get => _active.value;
            set
            {
                _active.value = value;
                if (value)
                    Rigidbody2D.WakeUp();
                else
                    Rigidbody2D.Sleep();
            }
        }

        public void Die()
        {
            PlayerDied?.Dispatch();
        }

        private void Update()
        {
            Vector2 velocity = Rigidbody2D.velocity;
            
            Animator.SetFloat(XVelocity, velocity.x);
            Animator.SetFloat(YVelocity, velocity.y);
            
            if (velocity.x > 0.25f || !Active)
            {
                _gameOverTimer = -1;
                return;
            }

            if (_gameOverTimer < 0)
                _gameOverTimer = Time.unscaledTime;

            if (_gameOverTimer > 0 && Time.unscaledTime > _gameOverTimer + GameOverWaitTime)
            {
                _gameOverTimer = -1;
                Die();
            }
        }
    }
}