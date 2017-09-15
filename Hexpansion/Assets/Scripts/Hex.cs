using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class Hex : MonoBehaviour
{

	public int X;
	public int Y;
	public int MaxWidth;
	public int MaxHeight;
	public List<GameObject> HexNeighborList;
	
	private  void SetNeighbours() {
		if (Y % 2 == 1){
			AddNeighbour(X+1, Y+1);
			AddNeighbour(X+1, Y);
			AddNeighbour(X+1, Y-1);
			AddNeighbour(X, Y-1);
			AddNeighbour(X-1, Y);
			AddNeighbour(X, Y+1);
		}
		else{
			AddNeighbour(X, Y+1);
			AddNeighbour(X+1, Y);
			AddNeighbour(X, Y-1);
			AddNeighbour(X-1, Y-1);
			AddNeighbour(X-1, Y);
			AddNeighbour(X-1, Y+1);
		}
	}

	private void AddNeighbour(int x, int y){
		var neighbour = GameObject.Find("Hex_" + x + "_" + y);
		if (neighbour != null){
			HexNeighborList.Add(neighbour);
		}
	}
	private void Start () {
		HexNeighborList = new List<GameObject>();
		SetNeighbours();
	}

	private void Update () {
		
	}
}
