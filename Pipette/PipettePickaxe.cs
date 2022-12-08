namespace EZBuild
{
	using UnityEngine;
	using System;
	using System.Linq;
	using System.Collections.Generic;
    using System.Reflection;

    public partial class PatchHotkey {


		// String array for valid targets for the pickaxe
		public static string[] pickaxeCompareTexts = new string[] {"Rock", "Tin deposit", "Muddy scrap pile", "Copper deposit", "Obsidian deposit", "Silver vein", "Guck Sack", "Ancient Sword" , "Petrified Bone"};


		private static bool Pipette_Pickaxe(Player player) {

			GameObject hoverObject = player.GetHoverObject();
			Hoverable hoverable = (hoverObject ? hoverObject.GetComponentInParent<Hoverable>() : null);

			if (hoverable == null) {
				return true;
			}
			string hoverText = hoverable.GetHoverText();

			if (Array.Exists(pickaxeCompareTexts, element => element == hoverText))
			{
				// If the item is equipped, unequip it
				if (player.GetRightItem() != null && player.GetRightItem().m_shared.m_name.Contains("$item_pickaxe"))
				{
					MethodInfo unequip = typeof(Player).GetMethod("QueueUnequipAction", BindingFlags.NonPublic | BindingFlags.Instance);
					unequip.Invoke(player, new object[] { player.GetRightItem() });

					// Stop autorun
					m_stopAutorun = true;
					return false;
				}

				// Find the best and lowest durability pickaxe and equip it
				Predicate<ItemDrop.ItemData> isPickaxe = delegate (ItemDrop.ItemData item) { 
					return item.m_shared.m_name.Contains("$item_pickaxe"); 
				};

				List<ItemDrop.ItemData> currentInventory = player.GetInventory().GetAllItems();
				List<ItemDrop.ItemData> pickaxes = currentInventory.FindAll(isPickaxe);
				List<ItemDrop.ItemData> durablePickaxes = pickaxes.Where(pick => pick.m_durability != 0).ToList();
				if (durablePickaxes.Count > 0)
				{
					int maxTier = durablePickaxes.Max(pick => pick.m_shared.m_toolTier);
					List<ItemDrop.ItemData> topTierPickaxes = durablePickaxes.Where(pick => pick.m_shared.m_toolTier == maxTier).ToList();
					topTierPickaxes.Sort(new CompareDurability());
					if (topTierPickaxes.Count > 0)
					{
						player.UnequipItem(player.GetRightItem());
						MethodInfo methodinfo = typeof(Player).GetMethod("QueueEquipAction", BindingFlags.NonPublic | BindingFlags.Instance);
						methodinfo.Invoke(player, new object[] { topTierPickaxes[0] });

						// Stop autorun
						m_stopAutorun = true;
						return false;
					}
				}
			}
			return true;
		}
	}
}