using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Schema;
using UnityEngine;
using Random = UnityEngine.Random;

public class HexMap : MonoBehaviour
{

	public GameObject HexTilePrefab;

	//number of tiles per axis.
	public int Width = 14;
	public  int Height = 14;
	public bool IsCircle = true;

	private const float XOffset = 1f;
	private const float ZOffset = 0.86f;

	private void Start ()
	{
		MoveCameraToCenter();

		if (IsCircle){
			CreateHexMapCircle();
		}
		else{
			CreateHexMapRectangle();
		}

	}
	
	void Update () {
		
	}

	private void CreateHexMapRectangle() {
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

	private void CreateHexMapCircle(){
		var tileList = CreatetileList();
		var filteredTileList = FilterTileList(tileList);
		CreateHexTilesFromList(filteredTileList);
	}

	private void CreateHexTilesFromList(List<List<List<int>>> tileList){
		for (int x = 0; x < tileList.Count; x++){
			var moveToCenter = 0;
			for (int y = 0; y < tileList[x].Count; y++){
					
					float xPos = x * XOffset + moveToCenter;
					float zPos = y * ZOffset;
					
					if (y % 2 == 1){
						xPos += XOffset / 2f;
						moveToCenter++;
					}
				if (tileList[x][y] != null){
					CreateTile(xPos, zPos, x, y);
				}
			}
		}
	}

	private void CreateTile(float x, float z, int coodX, int coodZ){
		GameObject hexTileGameObject = Instantiate(HexTilePrefab, new Vector3(x, 0, z), Quaternion.identity);
		hexTileGameObject.name = "Hex_" + coodX + "_" + coodZ;
		hexTileGameObject.transform.SetParent(this.transform);
		hexTileGameObject.GetComponentInChildren<Hex>().X = coodX;
		hexTileGameObject.GetComponentInChildren<Hex>().Y = coodZ;
		hexTileGameObject.GetComponentInChildren<Hex>().MaxWidth = Width;
		hexTileGameObject.GetComponentInChildren<Hex>().MaxHeight = Height;
		hexTileGameObject.isStatic = true;
		//optional
		hexTileGameObject. transform.localScale += new Vector3(0, Random.value*10, 0);
	}

	private static List<List<List<int>>> FilterTileList(List<List<List<int>>> tileList)
	{

		int length = tileList.Count;
		int width = tileList[0].Count;
		int toSkip = length / 2 -1;
		for (int x = 0; x < length; x++){
			int skipped = 0;
			for (int y = 0; y < width; y++){
				
				if (skipped < toSkip || y >= width+toSkip){
					skipped++;
					tileList[x][y] = null;
				}
			}
			toSkip--;
		}
		return tileList;
	}
	
	private List<List<List<int>>> CreatetileList(){

		List<List<List<int>>> tileList = new List<List<List<int>>>();
		var halfWidth = Width / 2 ;
		var halfHeight = Height / 2 ;

		var startX = -halfWidth;
		var startY = -halfHeight;

		for (int x = startX; x < halfWidth; x++){
			
			List<List<int>> columnList = new List<List<int>>();
			for (int y = startY; y < halfHeight; y++){
				List<int> tileCoods = new List<int>();
				tileCoods.Add(x);
				tileCoods.Add(y);
				columnList.Add(tileCoods);
			}
			tileList.Add(columnList);
		}
	//	Debug.Log(tileList[0][0]);
		return tileList;
	}


	private void MoveCameraToCenter() {
		float x = Width * XOffset / 2;
		float y = 10;
		float z = Height * ZOffset / 2;
		Camera.main.transform.position = new Vector3(x, y, z);
	}
}
