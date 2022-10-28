namespace EZBuild {
	using BepInEx;
	using HarmonyLib;
	using UnityEngine;

	[BepInPlugin(MID, modName, pluginVersion)]
	[BepInProcess("valheim.exe")]
	[BepInProcess("valheim_server.exe")]
	public partial class EZBuild : BaseUnityPlugin {
		private const string MID = "EZBuild";
		private const string modName = "EZBuild";
		private const string pluginVersion = "1.1.5";

		void Awake() {
			var harmony = new Harmony(MID);
			this.LoadConfig();

			if (!EZBuild.enabledMod.Value) {
				Logger.LogInfo(modName + " has been disabled in the mod config");
				return;
			}
			harmony.PatchAll();
		}	

		void OnDestroy() {
			var harmony = new Harmony(MID);
			harmony.UnpatchSelf();
		}
	}
}