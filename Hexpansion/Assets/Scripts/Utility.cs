using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Constraints;
using UnityEngine;

public static  class Utility
{

	public static Color RedColor = new Color32(255, 61, 0, 255);
	public static Color OrangeColor = new Color32(255, 140, 0, 255);
	//public static Color YellowColor = new Color32(255, 255, 186, 255);
	public static Color GreenColor = new Color32(0, 179, 88, 255);
	public static Color BlueColor = new Color32(9, 105, 162, 255);

	public static List<Color> ColorList = new List<Color>(){
		RedColor,
		OrangeColor,
		//YellowColor,
		GreenColor,
		BlueColor
	};
	
	public static GameObject GetHitObjectFromRayCast() {
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hitInfo;
		if (Physics.Raycast(ray, out hitInfo)) {
			
			return hitInfo.collider.transform.gameObject;
		}
		return null;
	}

	public static void Move(GameObject toDrop, GameObject target){
		Debug.Log("Moved");
		toDrop.transform.position = target.transform.position;
	}
	
	
}

/*
		Vector3 dir = destination - transform.position;
		Vector3 velocity = dir.normalized * _speed * Time.deltaTime;
		velocity = Vector3.ClampMagnitude(velocity, dir.magnitude);
		transform.Translate(velocity);
*/