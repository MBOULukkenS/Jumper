using Entities.Base;
using Jumper.Entities.Platforms;
using SmartData.SmartBool.Data;
using SmartData.SmartEvent;
using SmartData.SmartInt.Data;
using SmartData.SmartPlatform.Data;
using UnityEngine;

namespace Jumper.Entities.Player
{
    public class PlayerCollisionDetector : CollisionDetectorBase
    {
        public BoolVar Grounded;
        public IntVar PlayerScore;
        
        public EventDispatcher PlatformEdgeReached;
        public EventDispatcher PlatformEntered;
        public EventDispatcher PlatformExited;
        
        
        public PlatformVar CurrentPlatform;

        private void Start()
        {
            PlatformEdgeReached.unityEventOnReceive = true;
        }

        private void OnPlatformEnter(Collider2D ground)
        {
            Grounded.value = true;
            CurrentPlatform.value = ground.transform.root.GetComponent<Platform>();
            
            PlatformEntered?.Dispatch();
        }

        private void OnPlatformExit(Collider2D ground)
        {
            Grounded.value = false;
            CurrentPlatform.value = null;
            
            PlatformExited?.Dispatch();
        }

        private void OnPlatformEdgeEnter(Collider2D edge)
        {
            PlayerScore.value++;
            PlatformEdgeReached?.Dispatch();
        }

        private void OnPlatformEdgeExit(Collider2D edge)
        {
            //PlatformEdgeReached?.Dispatch();
        }
    }
}