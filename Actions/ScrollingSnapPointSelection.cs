using System;
using UnityEngine;

namespace EZBuild
{
    public partial class PatchHotkey
    {
        private static void ScrollSnapPointSelection(Player player)
        {
            //GameObject currentRotation = player.m_placementGhost;
            if (player.m_placementGhost)
            {
                if (Input.mouseScrollDelta.y > 0.0)
                {
                    player.m_manualSnapPoint++;
                    player.m_placeRotation--;
                }
                else
                {
                    player.m_manualSnapPoint--;
                    player.m_placeRotation++;
                }
            }

            //currentRotation.SetActive(true);
            player.SetupPlacementGhost();
        }
    }
}