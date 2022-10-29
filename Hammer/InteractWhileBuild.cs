using HarmonyLib;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using UnityEngine;

namespace EZBuild {

	public partial class PatchHotkey {

		private static MethodInfo InPlaceModeRef = AccessTools.Method(typeof(Character), "InPlaceMode", null, null);


		[HarmonyPostfix]
		[HarmonyPatch(typeof(Hud), "Awake")]
		private static void MoveHealthRoot(Hud __instance) {
			Vector3 currentPosition = __instance.m_pieceHealthRoot.localPosition;
			__instance.m_pieceHealthRoot.localPosition = new Vector3(currentPosition.x, currentPosition.y + 20f, currentPosition.z);
		}


		[HarmonyTranspiler]
		[HarmonyPatch(typeof(Player), "UpdateHover")]
		private static IEnumerable<CodeInstruction> Patch(IEnumerable<CodeInstruction> instructions) {
			if (!EZBuild.InteractWithToolEquipped.Value) {
				return instructions;
			}
			List<CodeInstruction> list = Enumerable.ToList<CodeInstruction>(instructions);
			for (int i = 0; i < list.Count; i++) {
				if (list[i].Calls(PatchHotkey.InPlaceModeRef)) {
					list[i - 1].opcode = OpCodes.Nop;
					list[i] = new CodeInstruction(OpCodes.Ldc_I4_0, null);
				}
			}
			return Enumerable.AsEnumerable<CodeInstruction>(list);
		}
	}
}
