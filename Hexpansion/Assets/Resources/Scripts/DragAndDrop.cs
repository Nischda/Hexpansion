using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour {

	public Vector3 StartPosition;
	public Shader HighlightShader;
	public HexPlayerTile HexPlayerTile;
	
	private Renderer _rend;
	private Shader _baseShader;
	private LayerMask _baseLayer;
	 	
	private Vector3 _screenPoint;
	private Vector3 _offset;
		
	private bool _mouseDown = false;
	//INIT
	private void Start(){
		_rend = GetComponent<Renderer>();
		_baseShader = _rend.material.shader;
		_baseLayer =  gameObject.layer;
		StartPosition = transform.position;
	}
	
		//MOVE
	private void OnMouseDown() {
		PlacedTilesManager.HighlightFreeNeighborTiles();
		//Debug.Log("clicked on: " + transform.name);
		_mouseDown = true;
		transform.Translate(0,0.5f,0); //move up slightly to be above other tiles.
		_rend.material.shader = HighlightShader;
		gameObject.layer = LayerMask.NameToLayer("Ignore Raycast"); //ignore Raycasts of selected object to be able to place it
		SnapToCursor();

	}
 
	private void OnMouseDrag(){
		if (Input.GetKeyDown("r")) {
			Rotate();
		}
		GameObject ourHitObject = Utility.GetHitObjectFromRayCast();
		if (ourHitObject != null && ourHitObject.CompareTag("HexBoardTile") && IsValidBoardTile(ourHitObject.GetComponent<HexBoardTile>())){
			SnapToBoardTile(ourHitObject);
		}
		else{
			Drag();
		}
	}

	private void OnMouseOver(){
		_rend.material.shader = HighlightShader;
		if (Input.GetKeyDown("r")) {
			Rotate();
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
		if (ourHitObject != null && ourHitObject.CompareTag("HexBoardTile") && IsValidBoardTile(ourHitObject.GetComponent<HexBoardTile>())){
			transform.parent.GetComponent<HexPlayerTileMap>().UpdatePlayerTiles(name); // Add new PlayerTile as one has been placed
			PlaceOnBoardTile(ourHitObject); //Tile gets placed on board
			HexPlayerTile.CalculatePoints(ourHitObject.GetComponent<HexBoardTile>()); //update score points based on matching corners
			//HexPlayerTile.DisableInteraction();
			Utility.FirstRound = false;
			Destroy(this);
		}
		else{
			ResetPosition(); //failed to snap onto object so reposition to origin
		}
		PlacedTilesManager.UnHighlightFreeNeighborTiles();
		_rend.material.shader = _baseShader;
	}
	
	private void SnapToCursor(){
		_screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
		_offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, _screenPoint.z));
	}

	private void Rotate(){
		transform.Rotate(0 ,60 ,0); //rotate y-axis for another color layout
	}
	
	private void Drag(){
		Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, _screenPoint.z);
		Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + _offset;
		transform.position = curPosition;
		transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
	}

	private bool IsValidBoardTile(HexBoardTile hexBoardTile) {
		if (Utility.FirstRound) {
			return true;
		}
		return PlacedTilesManager.IsFreeNeighbor(hexBoardTile);
	}
	
	private void SnapToBoardTile(GameObject ourHitObject){
		Utility.Move(gameObject, ourHitObject);
		transform.localScale = new Vector3(0.9f,0.9f,0.9f);
	}
	
	
	private void PlaceOnBoardTile(GameObject ourHitObject){
		Utility.Move(gameObject, ourHitObject);
		gameObject.transform.SetParent(ourHitObject.transform.parent);
		gameObject.transform.localScale = new Vector3(0.9f,0.9f,0.9f);
	}

	private void ResetPosition() {
		PlacedTilesManager.UnHighlightFreeNeighborTiles();
		transform.position = StartPosition;
		transform.gameObject.layer = _baseLayer;
	}
}
