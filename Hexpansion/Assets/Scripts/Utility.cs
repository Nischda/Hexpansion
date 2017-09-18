using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Constraints;
using UnityEngine;

public static  class Utility
{

	public static Color RedColor = new Color32(238, 58, 57, 255);
	public static Color OrangeColor = new Color32(255, 128, 28, 255);
	public static Color YellowColor = new Color32(255, 239, 0, 255);
	public static Color GreenColor = new Color32(0, 243, 0, 255);
	public static Color BlueColor = new Color32(0,165,255, 255);
	public static Color PurpleColor = new Color32(141,80,202, 255);
	
	/*
	public static Color RedColor = new Color32(255, 137, 181, 255);
	public static Color OrangeColor = new Color32(247, 161, 111, 255);
	public static Color YellowColor = new Color32(255, 219, 138, 255);
	public static Color GreenColor = new Color32(114, 224, 150, 255);
	public static Color BlueColor = new Color32(138,140,255, 255);
*/
	public static List<Color> ColorList = new List<Color>(){
		RedColor,
		OrangeColor,
		YellowColor,
		GreenColor,
		BlueColor,
		PurpleColor
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
		toDrop.transform.position = target.transform.position;
	}
	
	
}

/*
		Vector3 dir = destination - transform.position;
		Vector3 velocity = dir.normalized * _speed * Time.deltaTime;
		velocity = Vector3.ClampMagnitude(velocity, dir.magnitude);
		transform.Translate(velocity);
*/