using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexMap : MonoBehaviour
{

	public GameObject HexTilePrefab;

	//number of tiles per axis.
	private int _width = 20;
	private int _height = 20;

	private float _xOffset = 1f;
	private float _zOffset = 0.86f;
	void Start () {
		CreateHexMap();
	}
	
	void Update () {
		
	}

	private void CreateHexMap() {
		for (int x = 0; x < _width; x++) {
			for (int y = 0; y < _height; y++) {
				
				float xPos = x * _xOffset;

				if (y % 2 == 1) {
					xPos += _xOffset / 2f;
				}
				GameObject hexTileGameObject = Instantiate(HexTilePrefab, new Vector3(xPos , 0, y * _zOffset), Quaternion.identity);
				hexTileGameObject.name = "Hex_" + x + "_" + y;
				hexTileGameObject.transform.SetParent(this.transform);
				hexTileGameObject.isStatic = true;
				
				hexTileGameObject. transform.localScale += new Vector3(0, Random.value*10, 0);
			}
		}
	}
}
