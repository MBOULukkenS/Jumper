using System.Reflection;
using UnityEngine;

namespace Entities.Base
{
    public abstract class CollisionDetectorBase : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            GetType()
                .GetMethod($"On{other.gameObject.tag}Enter", BindingFlags.NonPublic | BindingFlags.Instance)
                ?.Invoke(this, new object[] {other});
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            GetType()
                .GetMethod($"On{other.gameObject.tag}Exit", BindingFlags.NonPublic | BindingFlags.Instance)
                ?.Invoke(this, new object[] {other});
        }
    }
}