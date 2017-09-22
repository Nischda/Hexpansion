using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{

	public Vector3 Destination;
	private float _speed = 2;

	private void Start (){
		Destination = transform.position;
	}
	
	private void Update ()
	{
		Vector3 dir = Destination - transform.position;
		Vector3 velocity = dir.normalized * _speed * Time.deltaTime;
		velocity = Vector3.ClampMagnitude(velocity, dir.magnitude);
		transform.Translate(velocity);
	}
}
