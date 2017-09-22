using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class MouseManager : MonoBehaviour
{

	private void Start () {
		
	}

	private void Update () {
		//Stop Raycast over UI Elements etc.
		if (EventSystem.current.IsPointerOverGameObject()) {
			return;
		}
		
		//_ourHitObject = Utility.GetHitObjectFromRayCast();
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





	//MOVE
	
	//COLORMANAGER
	/*
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
	*/
}
