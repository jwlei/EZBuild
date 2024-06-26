﻿namespace EZBuild
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using System.Linq;
    using static CharacterAnimEvent;
    using UnityEngine.UI;

    public partial class PatchHotkey
    {
        // String array for valid targets for the Axe
        public static string[] axeCompareTexts = { "Log", "Stump", "Beech", "Birch", "Oak", "Ancient tree", "Fir", "Pine", "Guck sack" };

        private static bool Pipette_Axe(Player player)
        {
            GameObject hoverObject = player.GetHoverObject();
            Hoverable hoverable = (hoverObject ? hoverObject.GetComponentInParent<Hoverable>() : null);

            if (hoverable == null)
            {
                return true;
            }

            string hoverText = hoverable.GetHoverText();

            if (Array.Exists(axeCompareTexts, element => element == hoverText))
            {
                if (player.m_rightItem != null && (player.m_rightItem.m_shared.m_name.Contains("$item_axe") || player.m_rightItem.m_shared.m_name.Contains("$item_battleaxe")))
                {
                    player.QueueUnequipAction(player.m_rightItem);
                    return false;
                }

                Predicate<ItemDrop.ItemData> isAxe = delegate (ItemDrop.ItemData item)
                {
                    return item.m_shared.m_name.Contains("$item_axe");
                };

                Predicate<ItemDrop.ItemData> isBattleaxe = delegate (ItemDrop.ItemData item)
                {
                    return item.m_shared.m_name.Contains("$item_battleaxe");
                };

                List<ItemDrop.ItemData> axes = player.m_inventory.m_inventory.FindAll(isAxe);
                List<ItemDrop.ItemData> durableAxes = axes.Where(axe => axe.m_durability != 0).ToList();

                List<ItemDrop.ItemData> battleaxes = player.m_inventory.m_inventory.FindAll(isBattleaxe);
                List<ItemDrop.ItemData> durableBattleaxe = battleaxes.Where(battleaxe => battleaxe.m_durability != 0).ToList();

                // Find 2H axe first if true, 1H first if false
                if (EZBuild.Prefer_2H_Axe.Value)
                {
                    // 2H PRIO --------------------------------------------------
                    if (durableBattleaxe.Count > 0)
                    {
                        int maxTier = durableBattleaxe.Max(Battleaxe => Battleaxe.m_shared.m_toolTier);
                        List<ItemDrop.ItemData> topTierBattleaxes = durableBattleaxe.Where(Battleaxe => Battleaxe.m_shared.m_toolTier == maxTier).ToList();
                        topTierBattleaxes.Sort(new CompareDurability());

                        if (topTierBattleaxes.Count > 0)
                        {
                            player.QueueUnequipAction(player.m_rightItem);
                            player.QueueEquipAction(topTierBattleaxes[0]);

                            return false;
                        }
                    }
                    else if (durableAxes.Count > 0)
                    {
                        int maxTier = durableAxes.Max(axe => axe.m_shared.m_toolTier);
                        List<ItemDrop.ItemData> topTierAxes = durableAxes.Where(axe => axe.m_shared.m_toolTier == maxTier).ToList();
                        topTierAxes.Sort(new CompareDurability());

                        if (topTierAxes.Count > 0)
                        {
                            player.QueueUnequipAction(player.m_rightItem);
                            player.QueueEquipAction(topTierAxes[0]);

                            return false;
                        }
                    }
                }

                if (!EZBuild.Prefer_2H_Axe.Value)
                {
                    // 1H PRIO --------------------------------------------------
                    if (durableAxes.Count > 0)
                    {
                        int maxTier = durableAxes.Max(axe => axe.m_shared.m_toolTier);
                        List<ItemDrop.ItemData> topTierAxes = durableAxes.Where(axe => axe.m_shared.m_toolTier == maxTier).ToList();
                        topTierAxes.Sort(new CompareDurability());

                        if (topTierAxes.Count > 0)
                        {
                            player.QueueUnequipAction(player.m_rightItem);
                            player.QueueEquipAction(topTierAxes[0]);

                            return false;
                        }
                    }
                    else if (durableBattleaxe.Count > 0)
                    {
                        int maxTier = durableBattleaxe.Max(Battleaxe => Battleaxe.m_shared.m_toolTier);
                        List<ItemDrop.ItemData> topTierBattleaxes = durableBattleaxe.Where(Battleaxe => Battleaxe.m_shared.m_toolTier == maxTier).ToList();
                        topTierBattleaxes.Sort(new CompareDurability());

                        if (topTierBattleaxes.Count > 0)
                        {
                            player.QueueUnequipAction(player.m_rightItem);
                            player.QueueEquipAction(topTierBattleaxes[0]);

                            return false;
                        }
                    }
                }
            }
            return true;
        }
    }
}