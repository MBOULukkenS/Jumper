using UnityEngine;

namespace Camera
{
    public class Camera2DFollow : MonoBehaviour
    {
        public float InterpVelocity;
        public float MinDistance;
        public float FollowDistance;
        public float FollowSpeed = 1f;
        public Vector3 Offset;
        
        [SerializeField]
        private GameObject _target;
        
        public GameObject Target
        {
            get => _target;
            set
            {
                _target = value;
                
                _targetPos = _target.transform.position;
                transform.position = new Vector3(_targetPos.x, _targetPos.y, transform.position.z);
            }
        }

        private Vector3 _targetPos;

        private void Start()
        {
            _targetPos = transform.position;
        }

        private void LateUpdate()
        {
            if (Target == null)
                return;

            Vector3 position = transform.position;
            Vector3 posNoZ = position;
            Vector3 position1 = Target.transform.position;

            posNoZ.z = position1.z;

            Vector3 targetDirection = position1 - posNoZ;

            InterpVelocity = targetDirection.magnitude * 5f;

            _targetPos = position + Time.unscaledDeltaTime * InterpVelocity * targetDirection.normalized;

            position = Vector3.Lerp(position, _targetPos + Offset, 0.25f * FollowSpeed);
            transform.position = position;
        }
    }
}