using System;
using Jumper.Entities.Platforms;
using SmartData.SmartVector3.Data;
using UnityEngine;

namespace Jumper.Input
{
    public class JumpOption : MonoBehaviour
    {
        public Platform Platform;
        public Transform JumpTarget;
        public Vector3Var OptionSelectedEvent;

        private void OnMouseDown()
        {
            OptionSelectedEvent.value = JumpTarget.transform.position;
            Platform.HideJumpOptions();
        }
    }
}