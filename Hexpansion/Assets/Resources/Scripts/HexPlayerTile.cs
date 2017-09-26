using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Permissions;
using NUnit.Framework.Constraints;
using UnityEditor;
using UnityEditorInternal.VR;
using UnityEngine.Networking;
using UnityEngine.UI;

//Todo: preview valid tiles to place
//Todo: Add Outline to playerTileParts or add contrast (edges especially)
//Todo: move mouseover etc into mousemanager
[RequireComponent(typeof(MeshCollider))]
 
public class HexPlayerTile : MonoBehaviour 
{

	public List<int> ColorIdList;
	
	public int XCood;
	public int ZCood;

	
	public void DisableInteraction() {
		GetComponent<MeshCollider>().enabled = false;
		var children = GetComponentsInChildren<Transform>();
		foreach (Transform go in children) {
			go.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
		}
	}

	
	//SCORE
	public void CalculatePoints(HexBoardTile hexBoardTile){
		
		PlacedTilesManager.AddPlacedTile(hexBoardTile); //ToDo Move
		hexBoardTile.Available = false;
		int index = 12;
		foreach (HexBoardTile neighbor in hexBoardTile.HexNeighborList){
			
			if (neighbor != null){ // null is used store empty neighbour tiles (for valid indexing)
				HexPlayerTile neighborTile = neighbor.transform.parent.GetComponentInChildren<HexPlayerTile>();
				if (neighborTile != null){ //If a playerTile has already been placed on nearby boardtile
					int rotateIndex = (index - GetRotationSkip())%6;
					if (ColorIdList[rotateIndex] == neighborTile.GetOppositeHexColorId(index)){
						GameManager.AddToScore(1);
						PlayAnimation(rotateIndex);
						neighborTile.PlayOppositeAnimation(index);
					}
				}
			}
			index++;
		}
	}
	

	/// <summary>
	/// Returns the colorId of the tile which is on the opposite end of the index given.
	/// +3 is added to get the opposite tile, as a hexagons consits of 6 parts
	/// For Each RotationStep (60Degrees) 1 index is subtracted to compare to get relative positions.
	/// </summary>
	/// <param name="index"></param>
	/// <returns>int</returns>
	public int GetOppositeHexColorId(int index){
		int oppositeHexIndex = index + 3;
		int rotateIndex = (oppositeHexIndex - GetRotationSkip()) % 6;
		return ColorIdList[rotateIndex];
	}

	/// <summary>
	/// Gets the current rotation and divides it by 60 to get the amount of rotations present
	/// EulerAngles can be negative, so 360 degrees is added to make it positive.
	/// EulerAngles are floats, so it is being cast to int (Should be exact numbers if unity does not fail too bad)
	/// </summary>
	/// <returns>int</returns>
	private int GetRotationSkip(){
		return (int) ((transform.rotation.eulerAngles.y+360)/ 60);
	}

	private void PlayAnimation(int id){
		GameObject tilePart = transform.Find("tile_parts").Find("tile_part" + id).gameObject;
		tilePart.GetComponent<Animation>().Play();
		Color color = Utility.ColorList[ColorIdList[id]];
		FloatingTextManager.CreateScorePopUp(1, color, tilePart.GetComponent<Renderer>().bounds.center);
	}
	
	private void PlayOppositeAnimation(int index){
		int oppositeHexIndex = index + 3;
		int rotateIndex = (oppositeHexIndex - GetRotationSkip()) % 6;
		PlayAnimation(rotateIndex);
	}
}       