using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlacedTilesManager {

	private static List<HexBoardTile> _tilesPlaced  = new List<HexBoardTile>();
	private static List<HexBoardTile> _freeNeighborTiles = new List<HexBoardTile>();
	

	/// <summary>
	/// Add boardTiles to a list, which have been occupied by a hexPlayerTile
	/// </summary>
	/// <param name="hexBoardTile"></param>
	public static void AddPlacedTile(HexBoardTile hexBoardTile){
		_tilesPlaced.Add(hexBoardTile);
		UpdateFreeNeighborTiles();
	}

	/// <summary>
	/// This list always keeps of track of all unoccupied hexBoardTiles, which are neighbored to occupied hexBoardTiles
	/// </summary>
	private static void UpdateFreeNeighborTiles(){
		_freeNeighborTiles = new List<HexBoardTile>();
		foreach(HexBoardTile hexBoardTile in _tilesPlaced) {
			foreach (HexBoardTile neighborTile in hexBoardTile.HexNeighborList){
				//Debug.Log(hexBoardTile.HexNeighborList.Count);
				if (neighborTile != null && neighborTile.Available) {
					_freeNeighborTiles.Add(neighborTile);
				}
			}
		}
	}

	/// <summary>
	/// Iterates over all neighbored, unoccupied hexBoardTiles and highlights them, while disabling any change to this state.
	/// </summary>
	public static void HighlightFreeNeighborTiles() {
		if (_freeNeighborTiles != null) {
			foreach (HexBoardTile hexBoardTile in _freeNeighborTiles) {
				hexBoardTile.HighlightColor();
				hexBoardTile.IsHighlighted = true;

			}
		}
	}
	
	/// <summary>
	/// Iterates over all neighbored, unoccupied hexBoardTiles and disables their highlights them, as well as unlocks this state.
	/// </summary>
	public static void UnHighlightFreeNeighborTiles() {
		if (_freeNeighborTiles != null) {
			foreach (HexBoardTile hexBoardTile in _freeNeighborTiles) {
				hexBoardTile.ResetColor();
				hexBoardTile.IsHighlighted = false;
			}
		}
	}

	public static bool IsFreeNeighbor(HexBoardTile hexBoardTile){
		return _freeNeighborTiles.Contains(hexBoardTile);
	}
	
}
