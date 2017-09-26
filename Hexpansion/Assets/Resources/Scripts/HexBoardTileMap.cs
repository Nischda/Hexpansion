using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Schema;
using UnityEngine;
using Random = UnityEngine.Random;

public class HexBoardTileMap : MonoBehaviour
{

	public GameObject HexBoardTilePrefab;

	//number of tiles per axis.
	public int Width = 14;
	public  int Height = 14;
	public bool IsCircle = true;

	private const float XOffset = 0.95263f;
	private const float ZOffset = 0.825f;

	private float _centerX;
	private int _centerY;

	
	
	private void Start () {
		//center hexBoardTileMap to world root 
		_centerX = Width/ 2 + Width/2*0.5f -1;
		_centerY =  Convert.ToInt32(Height * ZOffset / 2);
		
		AdjustCameraZoom();

		if (IsCircle){
			CreateHexMapCircle();
		}
		else{
			CreateHexMapRectangle();
		}

	}


	private void CreateHexMapRectangle() {
		for (int x = 0; x < Width; x++) {
			for (int y = 0; y < Height; y++) {
				
				float xPos = XOffset * x;
				float zPos = ZOffset * y;
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
			float moveToCenter = 0;
			for (int y = 0; y < tileList[x].Count; y++){
					
					float xPos = XOffset * x + moveToCenter;
					float zPos = ZOffset * y;
					
					if (y % 2 == 1){
						xPos += XOffset / 2f;
						moveToCenter += XOffset;
					}
				if (tileList[x][y] != null){
					CreateTile(xPos, zPos, x, y);
				}
			}
		}
	}

	private void CreateTile(float x, float z, int coodX, int coodZ){
		GameObject hexTileGameObject = Instantiate(HexBoardTilePrefab, new Vector3(x - _centerX, 0, z - _centerY), Quaternion.identity);
		hexTileGameObject.name = "HexBoardTile_" + coodX + "_" + coodZ;
		hexTileGameObject.transform.SetParent(this.transform);
		hexTileGameObject.GetComponentInChildren<HexBoardTile>().X = coodX;
		hexTileGameObject.GetComponentInChildren<HexBoardTile>().Y = coodZ;
		hexTileGameObject.isStatic = true;

		//optional
		//hexTileGameObject. transform.localScale += new Vector3(0, Random.value*10, 0);
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


	private void AdjustCameraZoom() {
		Camera.main.transform.position += new Vector3(0, Width -5, -1.5f);
		Camera.main.GetComponent<CameraManager>().MaxZoomOut += Width;
	}
}
