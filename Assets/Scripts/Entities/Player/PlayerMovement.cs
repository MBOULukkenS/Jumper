using System;
using SmartData.SmartBool.Data;
using SmartData.SmartEvent;
using SmartData.SmartVector3.Data;
using UnityEngine;
using Utils;

namespace Jumper.Entities.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovement : MonoBehaviour
    {
        public BoolVar Grounded;

        [SerializeField]
        private float _runSpeed = 5;

        [SerializeField]
        private float _maxSpeed = 4;

        [SerializeField]
        private float _jumpPower = 5;

        [SerializeField]
        private Vector3Var _jumpOptionSelected;

        private Player _player;

        public float RunSpeed => _runSpeed;

        public void Jump() => Jump(_jumpOptionSelected.value);
        public void Jump(Vector2 target)
        {
            Vector2 playerPos = transform.position;
            Vector2 diff = (target - playerPos);
            diff = new Vector2(Mathf.Abs(diff.x), (diff.y < 0 ? 0.25f : diff.y));

            float upForce = _jumpPower + (_player.Rigidbody2D.mass * _player.Rigidbody2D.gravityScale) * diff.y;
            float forwardForce = Mathf.Abs(_player.Rigidbody2D.velocity.x - _maxSpeed);
            
            Vector2 jumpVector = new Vector2(forwardForce, upForce);
            _player.Rigidbody2D.AddForce(jumpVector, ForceMode2D.Impulse);
        }

        private void Awake()
        {
            _player = GetComponent<Player>();
            _jumpOptionSelected.BindListener(Jump);
        }

        private void FixedUpdate()
        {
            if (_player.Active && _player.Rigidbody2D.velocity.x < _maxSpeed && Grounded.value)
                _player.Rigidbody2D.AddForce(Vector2.right * RunSpeed);
        }
    }
}