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
				player.Repair(rightItem, player.m_buildPieces.GetSelectedPiece());
			}
		}
	}
}
