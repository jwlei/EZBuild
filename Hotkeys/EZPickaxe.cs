namespace EZBuild
{
	using UnityEngine;
	using System;
	using System.Linq;
	using System.Collections.Generic;

	public partial class PatchHotkey
	{
		public static string[] pickaxeCompareTexts = new string[]
			{"Rock", "Tin deposit", "Muddy scrap pile", "Copper deposit", "Obsidian deposit", "Silver vein", "Guck Sack"};
		private static bool EZPickaxe(Player player)
		{
			GameObject hoverObject = player.GetHoverObject();
			Hoverable hoverable = (hoverObject ? hoverObject.GetComponentInParent<Hoverable>() : null);
			if (hoverable == null)
			{
				return true;
			}
			string hoverText = hoverable.GetHoverText();
			if (Array.Exists(pickaxeCompareTexts, element => element == hoverText))
			{
				// If the item is equipped, unequip it
				if (player.m_rightItem != null && player.m_rightItem.m_shared.m_name.Contains("$item_pickaxe"))
				{
					player.QueueUnequipItem(player.m_rightItem);

					// Stop autorun
					m_stopAutorun = true;
					return false;
				}

				// Find the best and lowest durability pickaxe and equip it
				Predicate<ItemDrop.ItemData> isPickaxe = delegate (ItemDrop.ItemData item) { return item.m_shared.m_name.Contains("$item_pickaxe"); };
				List<ItemDrop.ItemData> pickaxes = player.m_inventory.m_inventory.FindAll(isPickaxe);
				List<ItemDrop.ItemData> durablePickaxes = pickaxes.Where(pick => pick.m_durability != 0).ToList();
				if (durablePickaxes.Count > 0)
				{
					int maxTier = durablePickaxes.Max(pick => pick.m_shared.m_toolTier);
					List<ItemDrop.ItemData> topTierPickaxes = durablePickaxes.Where(pick => pick.m_shared.m_toolTier == maxTier).ToList();
					topTierPickaxes.Sort(new CompareDurability());
					if (topTierPickaxes.Count > 0)
					{
						player.QueueUnequipItem(player.m_rightItem);
						player.QueueEquipItem(topTierPickaxes[0]);

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