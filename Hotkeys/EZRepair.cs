namespace EZBuild
{
    public partial class PlayerHotkeyPatch
    {

		private static void EZRepair(Player player) {

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
