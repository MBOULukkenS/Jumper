using System.Collections.Generic;
using System.Linq;
using Camera;
using Data.Config;
using Doozy.Engine;
using Jumper.Entities.Player;
using Jumper.Managers.Base;
using SmartData.SmartEvent;
using SmartData.SmartInt.Data;
using UnityEngine;
using Utils.SmartData;

namespace Jumper.Managers
{
    public class GameManager : ManagerBase<GameConfig>
    {
        public IntVar PlayerScore;
        
        public EventListener StartGameListener;
        public EventListener GameOverListener;
        
        public EventListener PlayerPlatformEdgeReached;
        public EventListener PlayerPlatformExit;

        [SerializeField]
        private TimeManager _timeManager;

        public TimeManager TimeManager => _timeManager;

        [SerializeField]
        private PlatformsManager _platformsManager;

        public PlatformsManager PlatformsManager => _platformsManager;

        public GameObject PlayerObject { get; private set; }
        protected override bool DontDestroyOnLoad => true;

        public void ResetGame()
        {
            if (!TimeManager.RunningAtFullSpeed)
                ExitSlowMotionMode();
            
            PlatformsManager.ResetPlatforms();
            ResetPlayer();
            
            GameEventMessage.SendEvent("MainMenu_Show");
        }

        public void StartGame()
        {
            PlayerObject.GetComponent<Player>().Active = true;
        }

        public void PlatformEdgeReached()
        {
            EnterSlowMotionMode(true);
            PlatformsManager.ShowNextPlatformOptions();
        }

        public void ToggleSlowMotionMode()
        {
            if (TimeManager.RunningAtFullSpeed)
                EnterSlowMotionMode();
            else
                ExitSlowMotionMode();
        }

        public void EnterSlowMotionMode(bool usePlayerScore = false)
        {
            float slowmotionSpeed = _timeManager.Config.SlowMotionSpeed;
            if (usePlayerScore)
            {
                float newSpeed = slowmotionSpeed + (_timeManager.Config.SpeedDecrease * PlayerScore.value);
                slowmotionSpeed = newSpeed > _timeManager.Config.DefaultSpeed 
                    ? _timeManager.Config.DefaultSpeed 
                    : newSpeed;
            }

            TimeManager.ChangeTimescale(slowmotionSpeed, 0.25f);
        }

        public void ExitSlowMotionMode()
        {
            TimeManager.ChangeTimescale(_timeManager.Config.DefaultSpeed, 0.25f);
        }

        protected override void Initialize()
        {
            ResetGame();
            this.InitializeListeners();

            base.Initialize();
        }

        private void ResetPlayer()
        {
            SpawnPlayer();
            PlayerObject.GetComponent<Player>().Active = false;

            PlayerScore.value = 0;
            PlayerObject.transform.position = PlatformsManager.ActivePlatforms
                                                              .First()
                                                              .JumpTarget.transform.position;
        }

        private void SpawnPlayer()
        {
            if (PlayerObject != null)
                return;

            PlayerObject = Instantiate(GameConfig.Instance.PlayerPrefab);
            if (UnityEngine.Camera.main != null)
            {
                UnityEngine.Camera main;
                (main = UnityEngine.Camera.main).GetComponent<Camera2DFollow>().Target = PlayerObject;
                main.transform.position = new Vector3(PlayerObject.transform.position.x,
                                                      PlayerObject.transform.position.y,
                                                      main.transform.position.z);
            }
        }
    }
}