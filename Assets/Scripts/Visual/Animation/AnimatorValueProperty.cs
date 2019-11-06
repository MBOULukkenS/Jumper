using UnityEngine;

namespace Jumper.Visual.Animation
{
    public abstract class AnimatorValueProperty<T> : MonoBehaviour
    {
        public string PropertyName;
        
        public T Value
        {
            set => SetValue(value);
        }

        protected abstract void SetValue(T value);

        private Animator _animator;
        protected Animator Animator
        {
            get
            {
                if (_animator == null)
                    _animator = GetComponent<Animator>();
                return _animator;
            }
        }
    }
}