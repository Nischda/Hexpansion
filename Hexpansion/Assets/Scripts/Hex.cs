using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hex : MonoBehaviour
{

	public int X;
	public int Y;

	public GameObject[] GetNeighbours()
	{
		GameObject[] hexTileArray = new GameObject[6];
		if (Y % 2 == 1)
		{
			hexTileArray[0] = GameObject.Find("Hex_" + (X+1) + "_" + (Y + 1)); //Top Right
			hexTileArray[1] = GameObject.Find("Hex_" + (X+1) + "_" + Y); //Right
			hexTileArray[2] = GameObject.Find("Hex_" + (X+1) + "_" + (Y -1)); // Bottom Right
			hexTileArray[3] = GameObject.Find("Hex_" + X + "_" + (Y -1)); // Bottom Left
			hexTileArray[4] = GameObject.Find("Hex_" + (X-1) + "_" + Y); // Left
			hexTileArray[5] = GameObject.Find("Hex_" + X + "_" + (Y + 1)); //Top Left
		}
		else
		{
			hexTileArray[0] = GameObject.Find("Hex_" + X + "_" + (Y + 1)); //Top Right
			hexTileArray[1] = GameObject.Find("Hex_" + (X+1) + "_" + Y); //Right
			hexTileArray[2] = GameObject.Find("Hex_" + X + "_" + (Y -1)); // Bottom Right
			hexTileArray[3] = GameObject.Find("Hex_" + (X-1) + "_" + (Y -1)); // Bottom Left
			hexTileArray[4] = GameObject.Find("Hex_" + (X-1) + "_" + Y); // Left
			hexTileArray[5] = GameObject.Find("Hex_" + (X-1) + "_" + (Y + 1)); //Top Left
		}
		return hexTileArray;
	}

	private void Start () {
		
	}

	private void Update () {
		
	}
}
