using System;
using UnityEngine;

namespace Jumper.Visual.Parallax
{
    [ExecuteInEditMode]
    public class ParallaxCamera : MonoBehaviour 
    {
        public delegate void ParallaxCameraDelegate(float deltaMovement);
        public ParallaxCameraDelegate OnCameraTranslate;
        private float _oldPosition;
        void Start()
        {
            _oldPosition = transform.position.x;
        }
        void Update()
        {
            if (Math.Abs(transform.position.x - _oldPosition) < 0.001f) 
                return;
            
            if (OnCameraTranslate != null)
            {
                float delta = _oldPosition - transform.position.x;
                OnCameraTranslate(delta);
            }
            _oldPosition = transform.position.x;
        }
    }
}
