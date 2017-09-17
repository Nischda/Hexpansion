using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using NUnit.Framework.Constraints;
using UnityEngine.Networking;

[RequireComponent(typeof(MeshCollider))]
 
public class HexPlayerTile : MonoBehaviour 
{
 	
	private Vector3 _screenPoint;
	private Vector3 _offset;
	private Vector3 _startPosition;
	
	private Renderer _rend;
	private Shader _baseShader;
	public Shader HighlightShader;

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
		_mouseDown = true;
		_rend.material.shader = HighlightShader;
		transform.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
		Debug.Log("clicked on: " + transform.name);
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
			DisableInteraction();
			CalculatePoints(ourHitObject.GetComponent<HexBoardTile>().HexNeighborList);
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
		Destroy(this);
	}
	
	private void ResetPosition(){
		transform.position = _startPosition;
	}
	
	//SCORE
	private void CalculatePoints(List<GameObject> hexNeighborList){
		
	}
}
		               