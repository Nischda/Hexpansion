using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEditor;
using UnityEngine;

public class HexBoardTile : MonoBehaviour
{

	public int X;
	public int Y;
	public List<HexBoardTile> HexNeighborList;
	public bool Available = true;
	public bool IsValid = false;
	
	private Renderer _rend;
	private Color _baseColor;
	public bool IsHighlighted = false;
	
	public Color ValidColor;
	public Color InvalidColor;
	
	public Shader HighlightShader;
	
	private void Start () {
		HexNeighborList = new List<HexBoardTile>();
		SetNeighbours();

		_rend = GetComponent<Renderer>();
		_baseColor = _rend.material.color;
	}

	private  void SetNeighbours() {
		if (Y % 2 == 1){
			AddNeighbour(X, Y+1);
			AddNeighbour(X+1, Y);
			AddNeighbour(X+1, Y-1);
			AddNeighbour(X, Y-1);
			AddNeighbour(X-1, Y);
			AddNeighbour(X-1, Y+1);
		}
		else{
			AddNeighbour(X, Y+1);
			AddNeighbour(X+1, Y);
			AddNeighbour(X+1, Y-1);
			AddNeighbour(X, Y-1);
			AddNeighbour(X-1, Y);
			AddNeighbour(X-1, Y+1);
		}
	}

	private void AddNeighbour(int x, int y){
		var neighbour = GameObject.Find("HexBoardTile_" + x + "_" + y);
		if (neighbour != null){
			HexNeighborList.Add(neighbour.GetComponentInChildren<HexBoardTile>());
		}
		else{
			HexNeighborList.Add(null);
		}
	}
	
	private void OnMouseOver(){
		if (IsValid){
			DisplayValidColor();
		}
		else{
			DisplayInvalidColor();
		}

	}

	private void OnMouseExit(){
		if (!IsHighlighted){
			ResetColor();
		}
	}

	public void DisplayValidColor(){
		_rend.material.color = ValidColor;
	}
	
	public void DisplayInvalidColor(){
		_rend.material.color = InvalidColor;
	}
	
	public void ResetColor(){
		_rend.material.color = _baseColor;
	}
	
}
