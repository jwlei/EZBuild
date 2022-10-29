namespace EZBuild
{

	using System;
	using System.Collections.Generic;

	public partial class PatchHotkey {

		private static void Equip_Hammer(Player player) {
			/*
			 * Class responsible for equipping the hammer on hotkey press
			 */

			var rightItem = player.m_rightItem;

			if (rightItem != null && rightItem.m_shared.m_name == "$item_hammer") {

				if (!player.InAttack()) {
					player.QueueUnequipItem(rightItem);
					return;
				}
			}

			if (Hud.IsPieceSelectionVisible()) {
				return;
			}

			// Get the hammer with highest durability
			Predicate<ItemDrop.ItemData> isHammer = delegate (ItemDrop.ItemData item) { return item.m_shared.m_name == "$item_hammer"; };
			List<ItemDrop.ItemData> hammers = player.m_inventory.m_inventory.FindAll(isHammer);
			hammers.Sort(new CompareDurability());

			if (hammers.Count > 0) {
				player.QueueEquipItem(hammers[0]);
			}
		}
	}
}