using UnityEngine;

namespace Jumper.Entities.Platforms
{
    public class Platform : MonoBehaviour
    {
        public float WidthOffset = 5;
        public GameObject JumpOptionsContainer;
        public GameObject JumpTarget;

        public void ShowJumpOptions()
        {
            JumpOptionsContainer.SetActive(true);
        }

        public void HideJumpOptions()
        {
            JumpOptionsContainer.SetActive(false);
        }
    }
}