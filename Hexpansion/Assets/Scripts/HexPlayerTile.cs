using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using NUnit.Framework.Constraints;
using UnityEngine.Networking;

//Todo: preview valid tiles to place
//Todo: move player tile selection to bottom ui bar
//Todo: pop up matching parts
[RequireComponent(typeof(MeshCollider))]
 
public class HexPlayerTile : MonoBehaviour 
{
 	
	private Vector3 _screenPoint;
	private Vector3 _offset;
	private Vector3 _startPosition;
	
	private Renderer _rend;
	private Shader _baseShader;
	
	public Shader HighlightShader;
	public List<int> ColorIdList;

	private LayerMask _baseLayer;

	private bool _mouseDown = false;
	
	//INIT
	private void Start(){
		_rend = GetComponent<Renderer>();
		_baseShader = _rend.material.shader;
		_baseLayer =  gameObject.layer;
		_startPosition = transform.position;
	}

	//MOVE
	private void OnMouseDown(){
		//Debug.Log("clicked on: " + transform.name);
		_mouseDown = true;
		transform.Translate(0,0.25f,0);
		_rend.material.shader = HighlightShader;
		gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
		_screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
		_offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, _screenPoint.z));
	}
 
	private void OnMouseDrag(){
		Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, _screenPoint.z);
		Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint)+ _offset;
			                      transform.position = curPosition;
	 }

	private void OnMouseOver(){
		_rend.material.shader = HighlightShader;
		if (Input.GetKeyDown("r")){
			transform.Rotate(0 ,60 ,0); //rotate y-axis for another color layout
		}
	}

	private void OnMouseExit(){
		if (!_mouseDown){
			_rend.material.shader = _baseShader;
		}
	}

	private void OnMouseUp(){
		_mouseDown = false;
		GameObject ourHitObject = Utility.GetHitObjectFromRayCast();
		if (ourHitObject != null && ourHitObject.CompareTag("HexBoardTile")){
			Utility.Move(gameObject, ourHitObject);
			
			gameObject.transform.SetParent(ourHitObject.transform.parent);
			CalculatePoints(ourHitObject.GetComponent<HexBoardTile>().HexNeighborList);
			DisableInteraction();
		}
		else{
			ResetPosition(); //failed to snap onto object so reposition to origin
			transform.gameObject.layer = _baseLayer;
		}
		_rend.material.shader = _baseShader;
	}

	private void DisableInteraction(){
		GetComponent<MeshCollider>().enabled = false;
		transform.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
		//Destroy(this);
	}
	
	private void ResetPosition(){
		transform.position = _startPosition;
	}
	
	//SCORE
	private void CalculatePoints(List<GameObject> hexNeighborList)
	{
		int index = 0;
		foreach (var neighbor in hexNeighborList){
			if (neighbor != null){ // null is used store empty neighbour tiles (for valid indexing)
				var playerTile = neighbor.GetComponentInChildren<HexPlayerTile>();
				
				if (playerTile != null){ //If a playerTile has already been placed on nearby boardtile
					if (ColorIdList[index] == playerTile.ColorIdList[(index+3)%6]){
						Debug.Log("match");
					}
				}
			}
			index++;
		}
	}
}       