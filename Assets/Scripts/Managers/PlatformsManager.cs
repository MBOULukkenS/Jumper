using System.Collections.Generic;
using System.IO;
using System.Linq;
using Data.Config;
using JetBrains.Annotations;
using Jumper.Entities.Platforms;
using Jumper.Managers.Base;
using SmartData.SmartEvent;
using SmartData.SmartPlatform.Data;
using SmartData.SmartVector3.Data;
using UnityEngine;
using Utils;
using Utils.SmartData;

namespace Jumper.Managers
{
    public sealed class PlatformsManager : ManagerBase<PlatformsConfig>
    {
        public PlatformVar PlayerCurrentPlatform;
        public Vector3Var PlayerSpawnPos;

        public EventListener ShowJumpOptions;
        public EventListener HideJumpOptions;

        public EventListener ResetGameEvent;

        public readonly SortedGOList<int, Platform> ActivePlatforms = new SortedGOList<int, Platform>();
        public int PlatformIndex { get; private set; } = 0;

        internal Platform NextPlatform => GetNextPlatform(PlayerCurrentPlatform.value);

        private readonly System.Random _random = new System.Random();
        private Platform _playerPreviousPlatform;

        public void ShowNextPlatformOptions()
        {
            if (NextPlatform != null) 
                NextPlatform.ShowJumpOptions();
        }

        public void HideNextPlatformOptions()
        {
            if (NextPlatform != null) 
                NextPlatform.HideJumpOptions();
            else if (_playerPreviousPlatform != null)
                _playerPreviousPlatform.HideJumpOptions();
        }
        
        public void SpawnPlatforms()
        {
            for (int i = 0; i < _config.PlatformSpawnCount; i++)
                SpawnNextPlatform();
        }

        public void RemoveOldPlatforms()
        {
            foreach (KeyValuePair<int, Platform> kvp in ActivePlatforms
                                                        .Where(kvp => kvp.Key < ActivePlatforms.GetKey(PlayerCurrentPlatform.value) - 3)
                                                        .ToList())
                ActivePlatforms.Remove(kvp.Key);
        }

        public Platform SpawnNextPlatform()
        {
            return SpawnNextPlatform(GetRandomPlatformDefinition());
        }

        public Platform SpawnNextPlatform(PlatformDefinition platformDefinition)
        {
            Vector2 newPlatformPos = new Vector2(
                (platformDefinition.PlatformPiece.WidthOffset + _config.PlatformWidthSpacing)
                * (int) PlatformIndex,
                _config.PlatformSpawnHeights
                       .Where(sh => ActivePlatforms.Count == 0 
                                    || Mathf.Abs(sh - ActivePlatforms.Last().Value.transform.position.y) <= Config.MaxPlatformHeightDifference)
                       .RandomElement());
            
            Platform newPlatform = Instantiate(platformDefinition.PlatformPiece.gameObject,
                                               newPlatformPos,
                                               Quaternion.identity).GetComponent<Platform>();
            if (newPlatform == null)
                throw new InvalidDataException($"Failed to spawn platform '{platformDefinition.PlatformPiece}'");

            ActivePlatforms.Add(PlatformIndex, newPlatform);
            PlatformIndex++;

            return newPlatform;
        }

        public void ResetPlatforms()
        {
            PlatformIndex = 0;
            ActivePlatforms.Clear();

            SpawnPlatforms();
            PlayerSpawnPos.value = ActivePlatforms.First().Value.JumpTarget.transform.position;
        }

        protected override void Initialize()
        {
            PlayerCurrentPlatform.BindListener(PlayerCurrentPlatformChanged);
            
            this.InitializeListeners();
            base.Initialize();
        }

        private PlatformDefinition GetRandomPlatformDefinition()
        {
            return _config.Platforms
                          .RandomElement();
        }

        private void PlayerCurrentPlatformChanged()
        {
            if (PlayerCurrentPlatform.value != null
                && ActivePlatforms.GetKey(PlayerCurrentPlatform.value) + Config.PlatformSpawnCount > ActivePlatforms.Last().Key)
            {
                SpawnPlatforms();
                RemoveOldPlatforms();
            }

            if (PlayerCurrentPlatform.value != null)
                _playerPreviousPlatform = PlayerCurrentPlatform.value;
        }

        [CanBeNull]
        private Platform GetNextPlatform(Platform previous)
        {
            if (previous == null)
                return ActivePlatforms.GetValue(0);
            
            int nextIndex = ActivePlatforms.GetKey(previous) + 1;

            bool success = ActivePlatforms.TryGetValue(nextIndex, out Platform next);
            if (success) 
                return next;
            
            Debug.LogWarning($"Failed to get platform at index '{nextIndex}'");
            return null;
        }
    }
}