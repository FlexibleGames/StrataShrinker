using Vintagestory.API.Common;
using HarmonyLib;
using System;
using Vintagestory.API.Server;
using Vintagestory.ServerMods;
using Vintagestory.Server;

namespace StrataShrinker
{
    public class StrataShrinkerMod : ModSystem
    {
        ICoreAPI api;
        string configName = "stratashrink_config.json";

        public StrataShrinkerConfig SSConfig
        {
            get
            {
                return (StrataShrinkerConfig)this.api.ObjectCache[configName];
            }
            set
            {
                this.api.ObjectCache.Add(configName, value);
            }
        }
        public override void Start(ICoreAPI api)
        {
            base.Start(api);
            this.api = api;
        }
        public override void StartServerSide(ICoreServerAPI api)
        {
            base.StartServerSide(api);
            StrataShrinkerConfig strataShrinkerConfig = null;
            try
            {
                strataShrinkerConfig = api.LoadModConfig<StrataShrinkerConfig>(configName);
            }
            catch (Exception e)
            {
                base.Mod.Logger.Warning($"StrataShrinker: Error loading config, regenerating. {e.Message}");
                api.StoreModConfig<StrataShrinkerConfig>(new StrataShrinkerConfig(), configName);
                strataShrinkerConfig = api.LoadModConfig<StrataShrinkerConfig>(configName);
            }
            if (strataShrinkerConfig == null)
            {
                base.Mod.Logger.Warning("StrataShrinker: Regenerating default config as it was missing or broken...");
                api.StoreModConfig<StrataShrinkerConfig>(new StrataShrinkerConfig(), configName);
                strataShrinkerConfig = api.LoadModConfig<StrataShrinkerConfig>(configName);
            }
            this.SSConfig = strataShrinkerConfig;

            EditTerraGen();
        }

        public void EditTerraGen()
        {
            try
            {
                base.Mod.Logger.VerboseDebug($"StrataShrinker: Overriding StrataScale with value {SSConfig.StrataScale}");
                Traverse.Create(typeof(TerraGenConfig)).Field("rockStrataScale").SetValue(SSConfig.StrataScale);

                base.Mod.Logger.VerboseDebug($"StrataShrinker: Overriding OctaveScale with value {SSConfig.OctaveScale}");
                Traverse.Create(typeof(TerraGenConfig)).Field("rockStrataOctaveScale").SetValue(SSConfig.OctaveScale);

                base.Mod.Logger.VerboseDebug($"StrataShrinker: Overriding RegionSizeInChunks with value {SSConfig.RegionSizeInChunks}");
                Traverse.Create(typeof(MagicNum)).Field("ChunkRegionSizeInChunks").SetValue(SSConfig.RegionSizeInChunks);
            }
            catch (Exception e)
            {
                base.Mod.Logger.Warning($"StrataShrinker: Error during Traverse into TerraGenConfig object: {e.Message}");
            }
        }
    }
}
