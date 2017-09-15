using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {

		
	public float TurnSpeed = 1.0f;
	public float PanSpeed = 1.0f;
	public float ZoomSpeed = 1.0f; 
	
	private Vector3 _mouseOrigin;	//Cursor location on drag init
	private bool _isRotating;	
	private bool _isPanning;	
	private bool _isZooming;	
	
	private void Start () {
		
	}

	private void RotateCamera() {
		//left mouse button
		if (Input.GetMouseButtonDown(0)){
			_mouseOrigin = Input.mousePosition;
			_isRotating = true;
		}
		if (!Input.GetMouseButton(0)) _isRotating = false;// Disable movements on button release
		
		// Rotate camera along X and Y axis
		if (_isRotating){
			Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - _mouseOrigin);

			transform.RotateAround(transform.position, transform.right, -pos.y * TurnSpeed);
			transform.RotateAround(transform.position, Vector3.up, pos.x * TurnSpeed);
		}
	}
	private void PanCamera() {
		// right mouse button
		if (Input.GetMouseButtonDown(1)){
			_mouseOrigin = Input.mousePosition;
			_isPanning = true;
		}
		if (!Input.GetMouseButton(1)) _isPanning = false;// Disable movements on button release
		
		// Move camera on XZ axis
		if (_isPanning){
			Vector3 delta = Input.mousePosition - _mouseOrigin;
			transform.Translate(-delta.x * PanSpeed, -delta.y * PanSpeed, 0);
			_mouseOrigin = Input.mousePosition;
		}
	}
	
	private void ZoomCamera() {
		//Mouse wheel front scroll
		if (Input.GetAxis("Mouse ScrollWheel") > 0){
			GetComponent<Transform>().position = new Vector3(transform.position.x,transform.position.y-2*ZoomSpeed, transform.position.z+ZoomSpeed);
			transform.Rotate(-2,0,0);
		}
		//Mouse wheel back scroll
		if (Input.GetAxis("Mouse ScrollWheel") < 0){
			GetComponent<Transform>().position = new Vector3(transform.position.x,transform.position.y+2*ZoomSpeed, transform.position.z-ZoomSpeed);
			transform.Rotate(2,0,0);
		}
		/*
		// scroll mouse button
		if (Input.GetMouseButtonDown(2)){
			_mouseOrigin = Input.mousePosition;
			_isZooming = true;
			Debug.Log("works");
		}
		
		if (!Input.GetMouseButton(2)) _isZooming = false;
		
		// Move camera on Z axis
		if (_isZooming){
			Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - _mouseOrigin);

			Vector3 move = pos.y * ZoomSpeed * transform.forward;
			transform.Translate(move, Space.World);
		}
		*/
	}

	private void Update(){
		//CAMERA
		//RotateCamera();
		PanCamera();
		ZoomCamera();
	}
}
