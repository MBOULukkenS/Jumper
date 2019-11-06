using System;
using Jumper.Entities.Platforms;

namespace Data.Config
{
    [Serializable]
    public class PlatformDefinition
    {
        public Platform PlatformPiece;
        public Platform[] NextPieceBlacklist;
    }
}