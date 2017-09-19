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

	private const float XOffset = 1f/2;
	private const float ZOffset = 0.86f/2;

	private int _centerX;


	private void Start () {
		_centerX = Width / 2;
		CreateHexBoardTileLine();
		transform.position = new Vector3(0, 12,-1.65f); //set final position
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

	
	private void CreateTile(float x, float z, int coodX, int coodZ){
		GameObject hexTileGameObject = Instantiate(HexPlayerTilePrefab, new Vector3(x - _centerX, 0, z), Quaternion.identity); //subtract half of width to place under root/ in center of parent
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
		List<int> colorIdList = new List<int>();
		
		foreach (Transform tilePart in tilePartsGameObject.transform){
			int randColorId =  Random.Range(0, ColorCount - 1); //Get a random Color from predefined List
			colorIdList.Add(randColorId); //save added colors by id to avoid additional iterations
			tilePart.GetComponent<MeshRenderer>().material.color = Utility.ColorList[randColorId];
		}
		hexTileGameObject.GetComponent<HexPlayerTile>().ColorIdList = colorIdList;
	}
}

