using System;
using Data.Config;
using DG.Tweening;
using Jumper.Managers.Base;
using UnityEngine;

namespace Jumper.Managers
{
    public class TimeManager : ManagerBase<TimeConfig>
    {
        protected override bool DontDestroyOnLoad => true;

        public bool RunningAtFullSpeed => Math.Abs(Time.timeScale - Config.DefaultSpeed) < 0.001f;

        private float _previousSpeed;

        public void ChangeTimescale(float newScale, float duration = 0.15f)
        {
            if (Time.timeScale < 0.001f && newScale > 0.001f)
                Time.timeScale = 0.01f;
            
            DOTween.To(value => Time.timeScale = value, Time.timeScale, newScale, duration)
                   .OnUpdate(() =>
                   {
                       if (Time.timeScale < 0.001f)
                           Time.timeScale = 0;
                   }).SetEase(Config.TimeScaleEase);
        }

        public void TogglePaused(bool paused)
        {
            if (paused)
                _previousSpeed = Time.timeScale;
            
            ChangeTimescale(paused ? 0 : _previousSpeed);
        }

        protected override void Initialize()
        {
            _previousSpeed = Config.DefaultSpeed;
            base.Initialize();
        }
    }
}