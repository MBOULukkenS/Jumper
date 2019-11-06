using System.Collections.Generic;
using System.Linq;
using Camera;
using Data.Config;
using Doozy.Engine;
using Jumper.Entities.Player;
using Jumper.Managers.Base;
using SmartData.SmartEvent;
using SmartData.SmartInt.Data;
using SmartData.SmartVector3.Data;
using UnityEngine;
using Utils.SmartData;

namespace Jumper.Managers
{
    public class GameManager : ManagerBase<GameConfig>
    {
        public IntVar PlayerScore;
        public Vector3Var PlayerSpawnPos;
        
        public EventListener StartGameListener;
        public EventListener GameOverListener;
        
        public EventListener PlayerPlatformEdgeReachedEvent;
        public EventListener PlayerPlatformExitEvent;

        public EventDispatcher EnterSlowmotion;
        public EventDispatcher ExitSlowmotion;

        public EventDispatcher ShowJumpOptions;
        public EventDispatcher HideJumpOptions;

        public EventDispatcher ResetGameEvent;

        public GameObject PlayerObject { get; private set; }
        protected override bool DontDestroyOnLoad => true;

        public void ResetGame()
        {
            ExitSlowmotion?.Dispatch();
            ResetGameEvent?.Dispatch();
            
            ResetPlayer();
            GameEventMessage.SendEvent("MainMenu_Show");
        }

        public void StartGame()
        {
            PlayerObject.GetComponent<Player>().Active = true;
        }

        public void PlayerPlatformEdgeReached()
        {
            EnterSlowmotion?.Dispatch();
            ShowJumpOptions?.Dispatch();
        }

        public void PlayerPlatformExit()
        {
            ExitSlowmotion?.Dispatch();
            HideJumpOptions?.Dispatch();
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
            PlayerObject.transform.position = PlayerSpawnPos.value;
        }

        private void SpawnPlayer()
        {
            if (PlayerObject != null)
                return;

            PlayerObject = Instantiate(GameConfig.Instance.PlayerPrefab, PlayerSpawnPos.value, Quaternion.identity);
            if (UnityEngine.Camera.main != null)
                UnityEngine.Camera.main.GetComponent<Camera2DFollow>().Target = PlayerObject;
        }
    }
}