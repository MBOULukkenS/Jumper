using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Data.Config
{
    [CreateAssetMenu(menuName = "Config/Platforms Config", fileName = nameof(PlatformsConfig))]
    public class PlatformsConfig : ScriptableObjectConfigSingleton<PlatformsConfig>
    {
        [SerializeField]
        private PlatformDefinition[] _platforms;
        public IEnumerable<PlatformDefinition> Platforms => _platforms;

        [SerializeField]
        private int[] _platformSpawnHeights;
        public IEnumerable<int> PlatformSpawnHeights => _platformSpawnHeights;

        [SerializeField]
        private float _platformWidthSpacing = 2.5f;
        public float PlatformWidthSpacing => _platformWidthSpacing;

        [SerializeField]
        private uint _platformSpawnCount = 3;
        public uint PlatformSpawnCount => _platformSpawnCount;
        
        [SerializeField]
        private uint _maxPlatformHeightDifference = 2;
        public uint MaxPlatformHeightDifference => _maxPlatformHeightDifference;
    }
}