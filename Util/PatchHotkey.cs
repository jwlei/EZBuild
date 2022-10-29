namespace EZBuild {

	using BepInEx;
	using HarmonyLib;
	using UnityEngine;

	[HarmonyPatch]
	public partial class PatchHotkey : BaseUnityPlugin {
		/*
		 * A class for patching the hotkeys from the config file
		 */

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

			// Apply the same hotkey as the pipette to Axe
			if (Input.GetKeyDown(EZBuild.Pipette_Hotkey.Value.MainKey) && __instance.GetHoverObject() != null) {
				PatchHotkey.Pipette_Axe(__instance);
			}

			// Apply the same hotkey as the pipette to Pickaxe
			if (Input.GetKeyDown(EZBuild.Pipette_Hotkey.Value.MainKey) && __instance.GetHoverObject() != null) {
				PatchHotkey.Pipette_Pickaxe(__instance);
			}
			return true;
		}


		[HarmonyPostfix]
		[HarmonyPatch(typeof(Player), "Update")]
		private static void Postfix_Update(Player __instance) {

			// Check correct player
			if (Player.m_localPlayer != __instance || !__instance.TakeInput()) {
				return;
			}

			// Pipette
			if (Input.GetKeyDown(EZBuild.Pipette_Hotkey.Value.MainKey))
			{
				PatchHotkey.Pipette_Building(__instance);
				return;
			}

			// Repair
			if (Input.GetKeyDown(EZBuild.Repair_Hotkey.Value.MainKey)) {
				PatchHotkey.RepairHighlighted(__instance);
				return;
			}

			// Hammer
			if (Input.GetKeyDown(EZBuild.Hammer_Hotkey.Value.MainKey))
			{
				PatchHotkey.Equip_Hammer(__instance);
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