using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class HexPlayerTileMap : MonoBehaviour {

	public GameObject HexPlayerTilePrefab;

	public int ColorCount = 3;
	public int Width = 6;
	public int Height = 2;

	private const float XOffset = 1f;
	private const float ZOffset = 0.86f;


	private void Start (){
		CreateHexBoardTileLine();
	}

	private void CreateHexBoardTileLine(){
		for (int x = 0; x < Width; x++) {
			for (int y = 0; y < Height; y++) {
			
				float xPos = x * XOffset;
				float zPos = y * ZOffset;
				if (y % 2 == 1) {
					xPos += XOffset / 2f;
				}
				CreateTile(xPos, zPos, x, y);
			}
		}
	}

	
	private void CreateTile(float x, float z, int coodX, int coodZ){
		GameObject hexTileGameObject = Instantiate(HexPlayerTilePrefab, new Vector3(x+transform.position.x, 0, z+transform.position.z), Quaternion.identity); //subtract x,z-Axis to place under object root
		hexTileGameObject.name = "HexPlayerTile_" + coodX + "_" + coodZ;
		hexTileGameObject.transform.SetParent(this.transform);
		hexTileGameObject.isStatic = true;
		ColorizeParts(hexTileGameObject);
		//optional
		//hexTileGameObject. transform.localScale += new Vector3(0, Random.value*10, 0);
	}

	private void ColorizeParts(GameObject hexTileGameObject)
	{
		var tilePartsGameObject = hexTileGameObject.transform.Find("tile_parts");
		//Debug.Log(tilePartsGameObject.name);
		foreach (Transform tilePart in tilePartsGameObject.transform){
			int randColorId = Random.Range(0, ColorCount);
			Debug.Log(tilePart.name + " " + randColorId);
			tilePart.GetComponent<MeshRenderer>().material.color = Utility.ColorList[randColorId];
		}
	}
}

