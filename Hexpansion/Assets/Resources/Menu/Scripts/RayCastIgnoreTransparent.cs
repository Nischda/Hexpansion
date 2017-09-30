using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

public class RayCastIgnoreTransparent : MonoBehaviour{


	void Start () {
		this.GetComponent<Image>().alphaHitTestMinimumThreshold = 0.1f;
	}

}
