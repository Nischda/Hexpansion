using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {

		
	public float TurnSpeed = 1.0f;
	public float PanSpeed = 1.0f;
	public float ZoomSpeed = 1.0f;
	public int MaxZoomIn = 2;
	public int MaxZoomOut = 8;
	
	private Vector3 _mouseOrigin;	//Cursor location on drag init
	private bool _isRotating;	
	private bool _isPanning;	
	private bool _isZooming;	
	
	/*
	private void Start () {
		// set the desired aspect ratio (the values in this example are
		// hard-coded for 16:9, but you could make them into public
		// variables instead so you can set them at design time)
		float targetaspect = 16.0f / 9.0f;

		// determine the game window's current aspect ratio
		float windowaspect = (float)Screen.width / (float)Screen.height;

		// current viewport height should be scaled by this amount
		float scaleheight = windowaspect / targetaspect;

		// obtain camera component so we can modify its viewport
		Camera camera = GetComponent<Camera>();

		// if scaled height is less than current height, add letterbox
		if (scaleheight < 1.0f)
		{  
			Rect rect = camera.rect;

			rect.width = 1.0f;
			rect.height = scaleheight;
			rect.x = 0;
			rect.y = (1.0f - scaleheight) / 2.0f;
        
			camera.rect = rect;
		}
		else // add pillarbox
		{
			float scalewidth = 1.0f / scaleheight;

			Rect rect = camera.rect;

			rect.width = scalewidth;
			rect.height = 1.0f;
			rect.x = (1.0f - scalewidth) / 2.0f;
			rect.y = 0;

			camera.rect = rect;
		}
	}
	*/

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
			if (GetComponent<Transform>().position.y > MaxZoomIn){
				GetComponent<Transform>().position = new Vector3(transform.position.x, transform.position.y - 2 * ZoomSpeed, transform.position.z); // z - ZoomSpeed
				// 2D : Camera.main.orthographicSize -= .1f;}
			}
			//transform.Rotate(-2,0,0);
		}
		//Mouse wheel back scroll
		if (Input.GetAxis("Mouse ScrollWheel") < 0){
			if (GetComponent<Transform>().position.y < MaxZoomOut){
				Debug.Log(GetComponent<Transform>().position.z + " " + MaxZoomOut);
				GetComponent<Transform>().position = new Vector3(transform.position.x, transform.position.y + 2 * ZoomSpeed, transform.position.z ); // z - ZoomSpeed
				//transform.Rotate(2,0,0);
			}
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
