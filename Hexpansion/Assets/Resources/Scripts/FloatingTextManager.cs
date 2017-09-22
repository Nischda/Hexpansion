using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class FloatingTextManager : MonoBehaviour
{

	private static ScorePopUp _tileScorePopUp;
	private static GameObject _canvas;

	public static void Initialize(){
		_canvas = GameObject.Find("Canvas");
		_tileScorePopUp = Resources.Load<ScorePopUp>("Prefabs/UI/TileScorePopUpParent");

	}

	public static void CreateScorePopUp(int score, Color color, Vector3 location){
		ScorePopUp instance = Instantiate(_tileScorePopUp);
		Vector2 screenPosition = Camera.main.WorldToScreenPoint(location);
		
		instance.transform.SetParent(_canvas.transform, false);
		instance.transform.position = screenPosition;
		instance.SetScore(score, color);
	}
}
