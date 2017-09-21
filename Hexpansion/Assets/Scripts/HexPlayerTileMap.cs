using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class HexPlayerTileMap : MonoBehaviour {
	//Note: camera gets placed by HexBoardTileMap relative to board size
	public GameObject HexPlayerTilePrefab;

	public int ColorCount = 3;
	public int Width = 6;
	public int Height = 2;

	private const float XOffset = 1f/2;
	private const float ZOffset = 0.86f/2;

	private float _centerX;


	private void Start ()
	{
		_centerX = 0.24f * (Width-1);
		
		CreateHexBoardTileLine();
	}
	
	private void CreateHexBoardTileLine(){
		for (int x = 0; x < Width; x++) {
			for (int z = 0; z < Height; z++) {
			
				float xPos = x * XOffset;
				float zPos = z * ZOffset;
				if (z % 2 == 1) {
					xPos += XOffset / 2f;
				}
				CreateTile(xPos, zPos, x, z);
			}
		}
	}

	
	private void CreateTile(float x, float z, int xCood, int zCood){
		GameObject hexTileGameObject = Instantiate(HexPlayerTilePrefab, new Vector3(0,0,0), Quaternion.identity); //subtract half of width to place under root/ in center of parent
		hexTileGameObject.transform.SetParent(this.transform);
		hexTileGameObject.transform.localPosition = new Vector3((x-0.05f)- _centerX , -4, z - 1.65f); // set coods after parenting to avoid relative misplacement
		hexTileGameObject.name = "HexPlayerTile_" + xCood + "_" + zCood;
		hexTileGameObject.isStatic = true;
		var hexTile = hexTileGameObject.GetComponent<HexPlayerTile>();
		hexTile.XCood = xCood;
		hexTile.ZCood = zCood;
		ColorizeParts(hexTileGameObject);
		
		//optional
		//hexTileGameObject. transform.localScale += new Vector3(0, Random.value*10, 0);
	}

	private void ColorizeParts(GameObject hexTileGameObject)
	{
		var tilePartsGameObject = hexTileGameObject.transform.Find("tile_parts");
		List<int> colorIdList = new List<int>();
		
		foreach (Transform tilePart in tilePartsGameObject.transform){
			int randColorId =  Random.Range(0, ColorCount - 1); //Get a random Color from predefined List
			colorIdList.Add(randColorId); //save added colors by id to avoid additional iterations
			tilePart.GetComponent<MeshRenderer>().material.color = Utility.ColorList[randColorId];
		}
		hexTileGameObject.GetComponent<HexPlayerTile>().ColorIdList = colorIdList;
	}

	public void AddTile(int x, int z) {
		float xPos = x * XOffset;
		float zPos = z * ZOffset;
		if (z % 2 == 1) {
			xPos += XOffset / 2f;
		}
		CreateTile(xPos, zPos, x, z);
	}
}

