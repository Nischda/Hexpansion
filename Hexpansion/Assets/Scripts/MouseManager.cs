using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour {

	void Start () {
		
	}

	void Update () {
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hitInfo;
		if (Physics.Raycast(ray, out hitInfo)) {
			GameObject ourhitObject = hitInfo.collider.transform.gameObject;
			
			if (Input.GetMouseButtonDown(0)) { //left mouse button clicked
				MeshRenderer mr = ourhitObject.GetComponentInChildren<MeshRenderer>();

				if (mr.material.color == Color.red){
					mr.material.color = Color.white;
				}
				else{
					mr.material.color = Color.red;
				}
			}
		}
		//Ray fpsRay = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
	}
}
