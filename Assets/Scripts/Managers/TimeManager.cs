using System;
using Data.Config;
using DG.Tweening;
using Jumper.Managers.Base;
using SmartData.SmartBool.Data;
using SmartData.SmartEvent;
using SmartData.SmartInt.Data;
using UnityEngine;
using Utils.SmartData;

namespace Jumper.Managers
{
    public class TimeManager : ManagerBase<TimeConfig>
    {
        public IntVar PlayerScore;
        public BoolVar GamePaused;

        public EventListener EnterSlowmotion;
        public EventListener ExitSlowmotion;

        public bool RunningAtFullSpeed => Math.Abs(Time.timeScale - Config.DefaultSpeed) < 0.01f;

        public bool Paused
        {
            get => Time.timeScale < 0.01f;
            set => TogglePaused(value);
        }
        
        protected override bool DontDestroyOnLoad => true;

        private float _previousSpeed;

        public void ChangeTimescale(float newScale, float duration = 0.15f)
        {
            if (newScale < 0f)
                return;
            
            if (Math.Abs(Time.timeScale) < 0.001f)
                Time.timeScale = 0.1f;
            
            DOTween.To(value => Time.timeScale = value, Time.timeScale, newScale, duration)
                   .OnUpdate(() =>
                   {
                       if (Time.timeScale < 0.01f)
                           Time.timeScale = 0;
                   }).SetEase(Config.TimeScaleEase);
        }

        public void TogglePaused()
        {
            GamePaused.value = !GamePaused.value;
        }

        public void TogglePaused(bool pause)
        {
            if (pause)
                _previousSpeed = Time.timeScale;
            
            ChangeTimescale(pause ? 0f : _previousSpeed);
        }
        
        public void EnterSlowMotionMode(bool usePlayerScore = false)
        {
            float slowmotionSpeed = Config.SlowMotionSpeed;
            if (usePlayerScore && PlayerScore != null)
            {
                float newSpeed = slowmotionSpeed + (Config.SpeedDecrease * PlayerScore.value);
                slowmotionSpeed = newSpeed > Config.DefaultSpeed 
                    ? Config.DefaultSpeed 
                    : newSpeed;
            }

            ChangeTimescale(slowmotionSpeed, 0.25f);
        }

        public void ExitSlowMotionMode()
        {
            if (RunningAtFullSpeed)
                return;
            
            ChangeTimescale(Config.DefaultSpeed, 0.25f);
        }

        protected override void Initialize()
        {
            _previousSpeed = Config.DefaultSpeed;
            if (GamePaused != null)
                GamePaused.BindListener(UpdatePaused);
            
            this.InitializeListeners();
            base.Initialize();
        }

        private void UpdatePaused()
        {
            Paused = GamePaused.value;
        }
    }
}