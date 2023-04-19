using System;

namespace StrataShrinker
{
    public class StrataShrinkerConfig
    {
        public string StrataScaleComment = "Changes TerraGenConfig.rockStrataScale, Vanilla value = 16";
        public int StrataScale = 16;
        public string OctaveScaleComment = "Changes TerraGenConfig.rockStrataOctaveScale, Vanilla value = 32";
        public int OctaveScale = 32;
        public string RegionSizeComment = "Region Size in Chunks... Vanilla value = 16";
        public int RegionSizeInChunks = 16;

        public StrataShrinkerConfig()
        {
            StrataScaleComment = "Changes TerraGenConfig.rockStrataScale, Vanilla value = 16";
            StrataScale = 16;
            OctaveScaleComment = "Changes TerraGenConfig.rockStrataOctaveScale, Vanilla value = 32";
            OctaveScale = 32;
            RegionSizeComment = "Region Size in Chunks... Vanilla value = 16";
            RegionSizeInChunks = 16;
        }
    }
}
