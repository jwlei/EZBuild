namespace EZBuild {

	using System;
	using System.Reflection;
	using UnityEngine;

	public partial class PatchHotkey {

		private static void Pipette_Building(Player player) {

			bool objectFound = false;


			if (!player.InPlaceMode() && Hud.IsPieceSelectionVisible()) {
				return;
			}

			Piece currentHoverPiece = player.GetHoveringPiece();
			if (currentHoverPiece == null) {
				return;
			}

		
			// ? -- Not working
			PropertyInfo prop = typeof(Player).GetProperty("m_buildPieces", BindingFlags.NonPublic | BindingFlags.Instance);
			MethodInfo getter = prop.GetGetMethod(nonPublic: true);
			var availablePieces = getter.Invoke(player, null);

			PieceTable currentPieceTable = (PieceTable) availablePieces;
			Piece.PieceCategory category = currentHoverPiece.m_category;

			int availablePiecesInCategoryLength = currentPieceTable.GetAvailablePiecesInCategory(category);
			
			for (int y = 0; y < availablePiecesInCategoryLength && !objectFound; ++y) {

				Piece elem = currentPieceTable.GetPiece((int)category, new Vector2Int(y % 13, y / 13));

				if (elem.m_icon.name == currentHoverPiece.m_icon.name) {
					

					Debug.Log(elem.m_icon);

					currentPieceTable.SetCategory((int)elem.m_category);
					player.SetSelectedPiece(new Vector2Int(y % 13, y / 13));

					//player.m_placeRotation = (int)( Math.Round(currentHoverPiece.transform.localRotation.eulerAngles.y / 22.5f) );

					MethodInfo setupPlacementGhostRef = typeof(Player).GetMethod("SetupPlacementGhost", BindingFlags.NonPublic | BindingFlags.Instance);
					setupPlacementGhostRef.Invoke(player, new object[] { });

					Hud.instance.m_buildSelection = null;

					objectFound = true;
				}
			}
		}
	}
}
