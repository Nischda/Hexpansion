using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public static class GameManager
{

	public static Text ScoreText = GameObject.Find("Canvas/ScoreText").GetComponent<Text>();
	private static int _score = 0;
	
	public static void AddToScore(int val){
		_score += val;
		UpdateScore();
	}

	private static  void UpdateScore(){
		ScoreText.text = _score.ToString();
	}
	
}
