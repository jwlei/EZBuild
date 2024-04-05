using System;
using UnityEngine;

namespace EZBuild
{
    public partial class PatchHotkey
    {
        private static void ClearSelectedPiece(Player player)
        {
            if (player.InPlaceMode())
            {
                player.SetSelectedPiece(new Vector2Int(0, 0));
            }
        }
    }
}