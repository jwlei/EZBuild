namespace EZBuild {

	using BepInEx;
	using HarmonyLib;
	using System;
	using UnityEngine;

	[HarmonyPatch]
	public partial class PlayerHotkeyPatch : BaseUnityPlugin {

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
				PlayerHotkeyPatch.EZAxe(__instance);
			}
			if (Input.GetKeyDown(EZBuild.EZPickaxeHotkey.Value.MainKey) && __instance.GetHoverObject() != null)
			{
				PlayerHotkeyPatch.EZPickaxe(__instance);
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
				PlayerHotkeyPatch.EZRepair(__instance);
				return;
			}

			if (Input.GetKeyDown(EZBuild.EZPipetteHotkey.Value.MainKey)) {
				PlayerHotkeyPatch.QuickSelectBuild(__instance);
				return;
			}
			if (Input.GetKeyDown(EZBuild.EZHammerHotkey.Value.MainKey))
			{
				PlayerHotkeyPatch.EZHammer(__instance);
				return;
			}
		}

		[HarmonyPostfix]
		[HarmonyPatch(typeof(PlayerController), "FixedUpdate")]
		private static void Postfix_FixedUpdate() {
			if (PlayerHotkeyPatch.m_stopAutorun) {
				Player.m_localPlayer.m_autoRun = false;
				PlayerHotkeyPatch.m_stopAutorun = false;
			}
		}
	}
}