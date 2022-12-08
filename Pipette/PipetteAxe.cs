namespace EZBuild {

    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using System.Linq;
    using System.Reflection;

    public partial class PatchHotkey {

        // String array for valid targets for the Axe
        public static string[] axeCompareTexts = {"Log", "Stump", "Beech", "Birch", "Oak", "Ancient tree", "Fir", "Pine", "Guck sack", "Yggdrasil Shoot" };


        private static bool Pipette_Axe(Player player) {

            GameObject hoverObject = player.GetHoverObject();
            Hoverable hoverable = (hoverObject ? hoverObject.GetComponentInParent<Hoverable>() : null);
            
            if (hoverable == null) {
                return true;
            }

            string hoverText = hoverable.GetHoverText();

            if (Array.Exists(axeCompareTexts, element => element == hoverText)) {

                if (player.GetRightItem() != null && (player.GetRightItem().m_shared.m_name.Contains("$item_axe") || player.GetRightItem().m_shared.m_name.Contains("$item_battleaxe"))) {
                        player.UnequipItem(player.GetRightItem());
                        return false;
                }
 

                Predicate<ItemDrop.ItemData> isAxe = delegate (ItemDrop.ItemData item) {
                    return item.m_shared.m_name.Contains("$item_axe");
                };

                Predicate<ItemDrop.ItemData> isBattleaxe = delegate (ItemDrop.ItemData item) {
                    return item.m_shared.m_name.Contains("$item_battleaxe");
                };

                List<ItemDrop.ItemData> current_inventory = player.GetInventory().GetAllItems();
                

                List<ItemDrop.ItemData> axes = current_inventory.FindAll(isAxe);
                List<ItemDrop.ItemData> durableAxes = axes.Where(axe => axe.m_durability != 0).ToList();

                List<ItemDrop.ItemData> battleaxes = current_inventory.FindAll(isBattleaxe);
                List<ItemDrop.ItemData> durableBattleaxe = battleaxes.Where(battleaxe => battleaxe.m_durability != 0).ToList();


                // Find 2H axe first if true, 1H first if false
                if (EZBuild.Prefer_2H_Axe.Value) {
                    // 2H PRIO --------------------------------------------------
                    if (durableBattleaxe.Count > 0) {

                        int maxTier = durableBattleaxe.Max(Battleaxe => Battleaxe.m_shared.m_toolTier);
                        List<ItemDrop.ItemData> topTierBattleaxes = durableBattleaxe.Where(Battleaxe => Battleaxe.m_shared.m_toolTier == maxTier).ToList();
                        topTierBattleaxes.Sort(new CompareDurability());

                        if (topTierBattleaxes.Count > 0) {
                            MethodInfo unequip = typeof(Player).GetMethod("QueueUnequipAction", BindingFlags.NonPublic | BindingFlags.Instance);
                            unequip.Invoke(player, new object[] { player.GetRightItem() });


                            MethodInfo equip = typeof(Player).GetMethod("QueueEquipAction", BindingFlags.NonPublic | BindingFlags.Instance);
                            equip.Invoke(player, new object[] { topTierBattleaxes[0] });

                            return false;
                        }

                    } else if (durableAxes.Count > 0) {

                        int maxTier = durableAxes.Max(axe => axe.m_shared.m_toolTier);
                        List<ItemDrop.ItemData> topTierAxes = durableAxes.Where(axe => axe.m_shared.m_toolTier == maxTier).ToList();
                        topTierAxes.Sort(new CompareDurability());

                        if (topTierAxes.Count > 0) {
                            MethodInfo unequip = typeof(Player).GetMethod("QueueUnequipAction", BindingFlags.NonPublic | BindingFlags.Instance);
                            unequip.Invoke(player, new object[] { player.GetRightItem() });


                            MethodInfo equip = typeof(Player).GetMethod("QueueEquipAction", BindingFlags.NonPublic | BindingFlags.Instance);
                            equip.Invoke(player, new object[] { topTierAxes[0] });

                            return false;
                        }
                    }
                }
                

                
                if (!EZBuild.Prefer_2H_Axe.Value) {
                    // 1H PRIO --------------------------------------------------
                    if (durableAxes.Count > 0) {

                        int maxTier = durableAxes.Max(axe => axe.m_shared.m_toolTier);
                        List<ItemDrop.ItemData> topTierAxes = durableAxes.Where(axe => axe.m_shared.m_toolTier == maxTier).ToList();
                        topTierAxes.Sort(new CompareDurability());

                        if (topTierAxes.Count > 0) {
                            MethodInfo unequip = typeof(Player).GetMethod("QueueUnequipAction", BindingFlags.NonPublic | BindingFlags.Instance);
                            unequip.Invoke(player, new object[] { player.GetRightItem() });


                            MethodInfo equip = typeof(Player).GetMethod("QueueEquipAction", BindingFlags.NonPublic | BindingFlags.Instance);
                            equip.Invoke(player, new object[] { topTierAxes[0] });

                            return false;
                        }

                    } else if (durableBattleaxe.Count > 0) {

                        int maxTier = durableBattleaxe.Max(Battleaxe => Battleaxe.m_shared.m_toolTier);
                        List<ItemDrop.ItemData> topTierBattleaxes = durableBattleaxe.Where(Battleaxe => Battleaxe.m_shared.m_toolTier == maxTier).ToList();
                        topTierBattleaxes.Sort(new CompareDurability());

                        if (topTierBattleaxes.Count > 0) {
                            MethodInfo unequip = typeof(Player).GetMethod("QueueUnequipAction", BindingFlags.NonPublic | BindingFlags.Instance);
                            unequip.Invoke(player, new object[] { player.GetRightItem() });


                            MethodInfo equip = typeof(Player).GetMethod("QueueEquipAction", BindingFlags.NonPublic | BindingFlags.Instance);
                            equip.Invoke(player, new object[] { topTierBattleaxes[0] });

                            return false;
                        }
                    }
                }
            } return true;
        }
    }
}