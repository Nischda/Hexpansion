using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class HexPlayerTileMap : MonoBehaviour {
	//Note: camera gets placed by HexBoardTileMap relative to board size
	public GameObject HexPlayerTilePrefab;

	public int ColorCount = 3;
	public int Width = 4;
	public int Height = 1;

	private const float XOffset = 1f/2 +0.1f;
	private const float ZOffset = 0.86f/2;

	private float _centerX;

	private GameObject[] _upcomingPlayerTiles;

	private void Start (){
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
			int randColorId =  Random.Range(0, ColorCount); //Get a random Color from predefined List
			colorIdList.Add(randColorId); //save added colors by id to avoid additional iterations
			tilePart.GetComponent<MeshRenderer>().material.color = Utility.ColorList[randColorId];
		}
		hexTileGameObject.GetComponent<HexPlayerTile>().ColorIdList = colorIdList;
	}

	public void AddTile(int x, int z) {
		Debug.Log("Added Tile");
		float xPos = x * XOffset;
		float zPos = z * ZOffset;
		if (z % 2 == 1) {
			xPos += XOffset / 2f;
		}
		CreateTile(xPos, zPos, x, z);
	}

	public void UpdatePlayerTiles(string tileName)
	{
		if (tileName == "HexPlayerTile_0_0"){
			Destroy(GameObject.Find("HexPlayerTileMap/HexPlayerTile_1_0"));
		}
		else{
			Destroy(GameObject.Find("HexPlayerTileMap/HexPlayerTile_0_0"));
		}
		GameObject playerTile3 = GameObject.Find("HexPlayerTileMap/HexPlayerTile_2_0");
		GameObject playerTile4 = GameObject.Find("HexPlayerTileMap/HexPlayerTile_3_0");
		
		playerTile3.transform.position -= new Vector3(2*XOffset,0,0);
		playerTile3.name = "HexPlayerTile_0_0";
		playerTile3.GetComponent<HexPlayerTile>().StartPosition = playerTile3.transform.position;
		playerTile4.transform.position -= new Vector3(2*XOffset,0,0);
		playerTile4.name = "HexPlayerTile_1_0";
		playerTile4.GetComponent<HexPlayerTile>().StartPosition = playerTile4.transform.position;
		AddTile(2,0);
		AddTile(3,0);
	}
}

