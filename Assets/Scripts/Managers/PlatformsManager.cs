using System.IO;
using System.Linq;
using Data.Config;
using JetBrains.Annotations;
using Jumper.Entities.Platforms;
using Jumper.Managers.Base;
using SmartData.SmartPlatform.Data;
using UnityEngine;
using Utils;

namespace Jumper.Managers
{
    public sealed class PlatformsManager : ManagerBase<PlatformsConfig>
    {
        public PlatformVar PlayerCurrentPlatform;

        [SerializeField]
        private GameManager _gameManager;
        
        public readonly GOList<Platform> ActivePlatforms = new GOList<Platform>();
        public ulong PlatformIndex { get; private set; } = 0;

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
                                    || Mathf.Abs(sh - ActivePlatforms.Last().transform.position.y) <= Config.MaxPlatformHeightDifference)
                       .RandomElement());
            
            Platform newPlatform = Instantiate(platformDefinition.PlatformPiece.gameObject,
                                               newPlatformPos,
                                               Quaternion.identity).GetComponent<Platform>();
            if (newPlatform == null)
                throw new InvalidDataException($"Failed to spawn platform '{platformDefinition.PlatformPiece}'");

            PlatformIndex++;
            ActivePlatforms.Add(newPlatform);

            return newPlatform;
        }

        internal void ResetPlatforms()
        {
            PlatformIndex = 0;
            ActivePlatforms.Clear();

            SpawnPlatforms();
        }

        protected override void Initialize()
        {
            PlayerCurrentPlatform.BindListener(PlayerCurrentPlatformChanged);
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
                && ActivePlatforms.IndexOf(PlayerCurrentPlatform.value) + 3 > ActivePlatforms.Count)
                SpawnPlatforms();
            if (PlayerCurrentPlatform.value != null)
                _playerPreviousPlatform = PlayerCurrentPlatform.value;
        }

        [CanBeNull]
        private Platform GetNextPlatform(Platform previous)
        {
            if (previous == null)
                return null;
            
            int nextIndex = ActivePlatforms.IndexOf(previous) + 1;
            if (nextIndex > (ActivePlatforms.Count - 1) || nextIndex < 0)
                return null;

            return ActivePlatforms.ElementAt(nextIndex);
        }
    }
}