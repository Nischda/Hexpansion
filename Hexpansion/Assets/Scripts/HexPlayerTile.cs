using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Permissions;
using NUnit.Framework.Constraints;
using UnityEditor;
using UnityEditorInternal.VR;
using UnityEngine.Networking;

//Todo: preview valid tiles to place
//Todo: split player tiles into current and upcoming
//Todo: pop up matching parts and display texts in matching color and +1
//Todo: move mouseover etc into mousemanager
[RequireComponent(typeof(MeshCollider))]
 
public class HexPlayerTile : MonoBehaviour 
{
 	
	private Vector3 _screenPoint;
	private Vector3 _offset;
	private Vector3 _startPosition;
	
	private Renderer _rend;
	private Shader _baseShader;
	private LayerMask _baseLayer;
	public Shader HighlightShader;
	public List<int> ColorIdList;
	
	public int XCood;
	public int ZCood;
	
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
		transform.Translate(0,0.5f,0); //move up slightly to be above other tiles.
		_rend.material.shader = HighlightShader;
		gameObject.layer = LayerMask.NameToLayer("Ignore Raycast"); //ignore Raycasts of selected object to be able to place it
		_screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
		_offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, _screenPoint.z));
	}
 
	private void OnMouseDrag(){
		GameObject ourHitObject = Utility.GetHitObjectFromRayCast();
		if (ourHitObject != null && ourHitObject.CompareTag("HexBoardTile")){
			SnapToBoardTile(ourHitObject);
		}
		else{
			Drag();
		}
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
			transform.parent.GetComponent<HexPlayerTileMap>().AddTile(XCood, ZCood);
			PlaceOnBoardTile(ourHitObject);
			CalculatePoints(ourHitObject.GetComponent<HexBoardTile>().HexNeighborList);
			DisableInteraction();
		}
		else{
			ResetPosition(); //failed to snap onto object so reposition to origin
		}
		_rend.material.shader = _baseShader;
	}

	private void SnapToBoardTile(GameObject ourHitObject){
		Utility.Move(gameObject, ourHitObject);
		transform.localScale = new Vector3(1, 1, 1);
	}

	private void PlaceOnBoardTile(GameObject ourHitObject){
		Utility.Move(gameObject, ourHitObject);
		gameObject.transform.SetParent(ourHitObject.transform.parent);
		gameObject.transform.localScale = new Vector3(1,1,1);
	}

	private void Drag(){
		Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, _screenPoint.z);
		Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + _offset;
		transform.position = curPosition;
		transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
	}

	private void DisableInteraction(){
		GetComponent<MeshCollider>().enabled = false;
		var children = GetComponentsInChildren<Transform>();
		foreach (Transform go in children) {
			go.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
		}
		//transform.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
		//Destroy(this);
	}
	
	private void ResetPosition(){
		transform.position = _startPosition;
		transform.gameObject.layer = _baseLayer;
	}
	
	//SCORE
	private void CalculatePoints(List<GameObject> hexNeighborList)
	{
		int index = 0;
		foreach (var neighbor in hexNeighborList){
			if (neighbor != null){ // null is used store empty neighbour tiles (for valid indexing)
				var playerTile = neighbor.GetComponentInChildren<HexPlayerTile>();
				
				if (playerTile != null){ //If a playerTile has already been placed on nearby boardtile
					int oppositeHexId = (index + 3) % 6;
					if (ColorIdList[index] == playerTile.ColorIdList[oppositeHexId]){
						Debug.Log("match");
					}
				}
			}
			index++;
		}
	}
}       