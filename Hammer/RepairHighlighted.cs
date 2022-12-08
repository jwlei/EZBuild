namespace EZBuild
{
    public partial class PatchHotkey
    {

		private static void RepairHighlighted(Player player) {

			/*
			 * Repair the highlighted piece
			 */

			if (Hud.IsPieceSelectionVisible()) {
				return;
			}
			ItemDrop.ItemData rightItem = player.GetRightItem();
			if (rightItem != null && player.HaveStamina(rightItem.m_shared.m_attack.m_attackStamina)) {

				RepairStructure(player.m_nViewOverride);
			}
		}

		public static void RepairStructure(ZNetView obj)
		{
			obj.ClaimOwnership();
			var wearNTear = obj.GetComponent<WearNTear>();
	
			obj.InvokeRPC(ZNetView.Everybody, "WNTHealthChanged", new object[] { obj.GetZDO().GetFloat("health", wearNTear.m_health) });

		}



	}
}
