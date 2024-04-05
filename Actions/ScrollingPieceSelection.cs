using UnityEngine;

namespace EZBuild
{
    public partial class PatchHotkey
    {
        private static void ScrollPieceSelection(Player player)
        {
            // Get the current piece
            Vector2Int currentIndex = player.m_buildPieces.GetSelectedIndex();

            // Change the piece index one up or down depending on the scroll direction
            int newX = 0;
            if (Input.mouseScrollDelta.y > 0.0)
            {
                newX = currentIndex.x + 1;
                player.m_placeRotation--;
            }
            else
            {
                newX = currentIndex.x - 1;
                player.m_placeRotation++;
            }
            int newY = currentIndex.y;

            // Make sure the new X does not overflow/underflow
            if (newX < 0)
            {
                newX = 9;
                newY = currentIndex.y - 1;
            }
            else if (newX > 9)
            {
                newX = 0;
                newY = currentIndex.y + 1;
            }

            // Make sure the new Y does not overflow/underflow
            int amount_available_pieces = player.m_buildPieces.GetAvailablePiecesInSelectedCategory();
            if (newY < 0)
            {
                player.m_buildPieces.PrevCategory();
                amount_available_pieces = player.m_buildPieces.GetAvailablePiecesInSelectedCategory();
                newY = amount_available_pieces / 10;
                newX = amount_available_pieces % 10 - 1;
            }
            else if (newY > (amount_available_pieces / 10) || (newY == (amount_available_pieces / 10) && newX >= (amount_available_pieces % 10)))
            {
                player.m_buildPieces.NextCategory();
                newY = 0;
                newX = 0;
            }

            // Set the newly selected piece
            currentIndex = new Vector2Int(newX, newY);
            // Debug.Log(currentIndex);
            player.SetSelectedPiece(currentIndex);

            // Update the placement ghost
            player.SetupPlacementGhost();

            // Update the requirements UI
            Hud.instance.m_hoveredPiece = null;
        }
    }
}