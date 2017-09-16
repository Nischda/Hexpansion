using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class MouseManager : MonoBehaviour
{

	public Unit SelectedUnit;
	private void Start () {
		
	}

	private void Update () {
		//Stop Raycast over UI Elements etc.
		if (EventSystem.current.IsPointerOverGameObject()) {
			return;
		}
		
		GameObject ourHitObject = GetHitObjectFromRayCast();
		if (ourHitObject != null) {
			InputManager(ourHitObject);
		}
	}

	//INPUT
	private void InputManager(GameObject ourHitObject) {

		/*
		if (Input.GetMouseButtonDown(0)) { //left mouse button clicked
			if (ourHitObject.CompareTag("HexBoardTile")) {
				ColorManager(ourHitObject);
			}
			else if (ourHitObject.GetComponent<Unit>() != null) {
				MeshRenderer mr = ourHitObject.GetComponentInChildren<MeshRenderer>();
				mr.material.color = Color.green;
				//Utility.Move(ourHitObject);
			}
		}
		*/
	}

	private static GameObject GetHitObjectFromRayCast() {
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hitInfo;
		if (Physics.Raycast(ray, out hitInfo)) {
			return hitInfo.collider.transform.gameObject;
		}
		return null;
	}

	
	
	
	private Vector3 _screenPoint;
	private Vector3 _offset;
 
	void OnMouseDown()
	{
		//SelectedUnit = ourHitObject.transform.position
		
		_screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
		_offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, _screenPoint.z));
 
	}
 
	void OnMouseDrag()
	{
		Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, _screenPoint.z);
 
		Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint)+ _offset;
		transform.position = curPosition;
	}
	
	
	
	
	
	
	
	//MOVE
	
	//COLORMANAGER
	private void ColorManager(GameObject ourHitObject) {
		MeshRenderer mr = ourHitObject.GetComponentInChildren<MeshRenderer>();
		if (mr.material.color == Color.red){
			mr.material.color = Color.white;
			ColorNeighbours(ourHitObject, Color.green);
		}
		else{
			mr.material.color = Color.red;
			ColorNeighbours(ourHitObject, Color.white);
		}

		if (SelectedUnit != null)
		{
			SelectedUnit.Destination = ourHitObject.transform.position;
		}
	}

	private static void ColorNeighbours(GameObject ourHitObject, Color color){
		var neighbours = ourHitObject.GetComponent<HexBoardTile>().HexNeighborList;
		
		for (int i = 0; i < neighbours.Count; i++) {
			Debug.Log(i);
			neighbours[i].GetComponentInChildren<MeshRenderer>().material.color =color;
		}
	}
}
