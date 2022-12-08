namespace EZBuild
{

	using System;
	using System.Collections.Generic;
    using System.Reflection;

    public partial class PatchHotkey {

		private static void Equip_Hammer(Player player) {
			/*
			 * Class responsible for equipping the hammer on hotkey press
			 */

			var rightItem = player.GetRightItem();

			if (rightItem != null && rightItem.m_shared.m_name == "$item_hammer") {

				if (!player.InAttack()) {
					MethodInfo unequip = typeof(Player).GetMethod("QueueUnequipAction", BindingFlags.NonPublic | BindingFlags.Instance);
					unequip.Invoke(player, new object[] { rightItem });
					return;
				}
			}

			if (Hud.IsPieceSelectionVisible()) {
				return;
			}


			// Get the hammer with highest durability
			Predicate<ItemDrop.ItemData> isHammer = delegate (ItemDrop.ItemData item) { 
				return item.m_shared.m_name == "$item_hammer"; 
			};

			List<ItemDrop.ItemData> current_inventory = player.GetInventory().GetAllItems();
			List<ItemDrop.ItemData> hammers = current_inventory.FindAll(isHammer);
			hammers.Sort(new CompareDurability());

			if (hammers.Count > 0) {
				

				MethodInfo equip = typeof(Player).GetMethod("QueueEquipAction", BindingFlags.NonPublic | BindingFlags.Instance);
				equip.Invoke(player, new object[] { hammers[0] });

				


				//player.IsEquipActionQueued(hammers[0]);
			}
		}
	}
}