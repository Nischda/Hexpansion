using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexMap : MonoBehaviour
{

	public GameObject HexTilePrefab;

	//number of tiles per axis.
	private const int Width = 20;
	private const int Height = 20;

	private const float XOffset = 1f;
	private const float ZOffset = 0.86f;

	private void Start () {
		CreateHexMap();
	}
	
	void Update () {
		
	}

	private void CreateHexMap() {
		for (int x = 0; x < Width; x++) {
			for (int y = 0; y < Height; y++) {
				
				float xPos = x * XOffset;

				if (y % 2 == 1) {
					xPos += XOffset / 2f;
				}
				GameObject hexTileGameObject = Instantiate(HexTilePrefab, new Vector3(xPos , 0, y * ZOffset), Quaternion.identity);
				hexTileGameObject.name = "Hex_" + x + "_" + y;
				hexTileGameObject.transform.SetParent(this.transform);
				hexTileGameObject.GetComponentInChildren<Hex>().X = x;
				hexTileGameObject.GetComponentInChildren<Hex>().Y = y;
				hexTileGameObject.isStatic = true;
				
				//optional
				//hexTileGameObject. transform.localScale += new Vector3(0, Random.value*10, 0);
			}
		}
	}
}
