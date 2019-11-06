using System;
using Data.Base;

namespace Data.PlayerData
{
    [Serializable]
    public class PlayerSaveData : BinarySavable<PlayerSaveData>
    {
        public ulong HighScore { get; set; }
    }
}