namespace EZBuild {

	using BepInEx;
	using HarmonyLib;
	using UnityEngine;

	[HarmonyPatch]
	public partial class PatchHotkey : BaseUnityPlugin {

		private static bool m_stopAutorun = false;

		[HarmonyPrefix]
		[HarmonyPatch(typeof(Player), "Update")]
		private static bool Prefix_Update(Player __instance) {
			if (Player.m_localPlayer != __instance) {
				return true;
			}
			if (!__instance.TakeInput()) {
				return true;
			}
			if (Input.GetKeyDown(EZBuild.EZAxeHotkey.Value.MainKey) && __instance.GetHoverObject() != null)
			{
				PatchHotkey.ItemAxe(__instance);
			}
			if (Input.GetKeyDown(EZBuild.EZPickaxeHotkey.Value.MainKey) && __instance.GetHoverObject() != null)
			{
				PatchHotkey.EZPickaxe(__instance);
			}
			return true;
		}


		[HarmonyPostfix]
		[HarmonyPatch(typeof(Player), "Update")]
		private static void Postfix_Update(Player __instance) {
			if (Player.m_localPlayer != __instance || !__instance.TakeInput()) {
				return;
			}

			if (Input.GetKeyDown(EZBuild.EZRepairHotkey.Value.MainKey)) {
				PatchHotkey.RepairHighlighted(__instance);
				return;
			}

			if (Input.GetKeyDown(EZBuild.EZPipetteHotkey.Value.MainKey)) {
				PatchHotkey.PipetteBuilding(__instance);
				return;
			}
			if (Input.GetKeyDown(EZBuild.EZHammerHotkey.Value.MainKey))
			{
				PatchHotkey.EZHammer(__instance);
				return;
			}
		}

		[HarmonyPostfix]
		[HarmonyPatch(typeof(PlayerController), "FixedUpdate")]
		private static void Postfix_FixedUpdate() {
			if (PatchHotkey.m_stopAutorun) {
				Player.m_localPlayer.m_autoRun = false;
				PatchHotkey.m_stopAutorun = false;
			}
		}
	}
}