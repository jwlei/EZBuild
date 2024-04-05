namespace EZBuild
{
    using BepInEx;
    using HarmonyLib;
    using UnityEngine;

    [BepInPlugin(MID, modName, pluginVersion)]
    [BepInProcess("valheim.exe")]
    [BepInProcess("valheim_server.exe")]
    public partial class EZBuild : BaseUnityPlugin
    {
        /*
		 * Initialization
		 */

        private const string MID = "jwlei.EZBuild";
        private const string modName = "EZ Build and pipette";
        private const string pluginVersion = "2.1.0";

        private void Awake()
        {
            var harmony = new Harmony(MID);
            LoadConfig();

            if (!EnableMod.Value)
            {
                Logger.LogInfo(modName + " has been disabled in the mod config");
                OnDestroy();
            }
            harmony.PatchAll();
        }

        private void OnDestroy()
        {
            var harmony = new Harmony(MID);
            harmony.UnpatchSelf();
        }
    }
}