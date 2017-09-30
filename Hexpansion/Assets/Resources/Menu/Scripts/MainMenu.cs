using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;


//ToDo: Settings: Game: Sound On/Off Sound Volume Slider
//ToDo: Settings: Game: Music On/Off Music Volume Slider
//ToDo: Settings: Controls: Button: Click/Drag, Button: Rotate
//ToDo: Settings: Video: Bloom, Resolution?
//ToDo: New Game: Settings: Board Size?, Circle?, PlayerTiles? Slider, Name?
//ToDo: Highscore
//ToDo:
//ToDo:

public class MainMenu : MonoBehaviour
{

	//MainMenu
	public GameObject BtnAreYouSure;
	public GameObject BtnPlay;
	public GameObject BtnSettings;
	public GameObject BtnExit;
	
	//PlayMenu
	public GameObject BtnContinue;
	public GameObject BtnNewGame;
	public GameObject BtnLoadGame;
	public GameObject BtnBackFromPlay;
	
	public GameObject BtnGame;
	public GameObject BtnControls;
	public GameObject BtnVideo;
	public GameObject BtnBackFromSettings;
	

	private static List<GameObject> _mainMenuButtons = new List<GameObject>();
	private static List<GameObject> _playMenuButtons = new List<GameObject>();
	private static List<GameObject> _settingsMenuButtons = new List<GameObject>();
	
	private void Start()
	{
		_mainMenuButtons.Add(BtnPlay);
		_mainMenuButtons.Add(BtnSettings);
		_mainMenuButtons.Add(BtnExit);
		
		_playMenuButtons.Add(BtnContinue);
		_playMenuButtons.Add(BtnNewGame);
		_playMenuButtons.Add(BtnLoadGame);
		_playMenuButtons.Add(BtnBackFromPlay);
		
		_settingsMenuButtons.Add(BtnGame);
		_settingsMenuButtons.Add(BtnControls);
		_settingsMenuButtons.Add(BtnVideo);
		_settingsMenuButtons.Add(BtnBackFromSettings);
		
	}


	//Utility
	public static void SetListActive(List<GameObject> list, bool boolean){
		foreach (GameObject go in list){
			go.SetActive(boolean);
		}
	}
	
	public static void SetAnimatorBool(List<GameObject> list, String boolName, bool boolean){
		foreach (GameObject go in list){
			go.GetComponent<Animator>().SetBool(boolName, boolean);
		}
	}

	public void DisplayMainMenu(){
		SetListActive(_mainMenuButtons, true);
		SetAnimatorBool(_mainMenuButtons, "RotateOut", false);
		SetAnimatorBool(_mainMenuButtons, "RotateIn", true);
		
		SetAnimatorBool(_playMenuButtons, "RotateIn", false);
		SetAnimatorBool(_playMenuButtons, "RotateOut", true);
		
		SetAnimatorBool(_settingsMenuButtons, "RotateIn", false);
		SetAnimatorBool(_settingsMenuButtons, "RotateOut", true);
	}

	public void DisplayPlayMenu(){
		SetListActive(_playMenuButtons, true);
		SetAnimatorBool(_playMenuButtons, "RotateOut", false);
		SetAnimatorBool(_playMenuButtons, "RotateIn", true);
		
		SetAnimatorBool(_mainMenuButtons, "RotateIn", false);
		SetAnimatorBool(_mainMenuButtons, "RotateOut", true);
	}

	public void DisplaySettingsMenu(){
		SetListActive(_settingsMenuButtons, true);
		SetAnimatorBool(_settingsMenuButtons, "RotateOut", false);
		SetAnimatorBool(_settingsMenuButtons, "RotateIn", true);
		
		SetAnimatorBool(_mainMenuButtons, "RotateIn", false);
		SetAnimatorBool(_mainMenuButtons, "RotateOut", true);
	}
	
	//Approvals
	public void PromptAreYouSure() {
		SetListActive(_mainMenuButtons, false);
		BtnAreYouSure.SetActive(true);
	}

	public void No() {
		SetListActive(_mainMenuButtons, true);
		BtnAreYouSure.SetActive(false);
	}

	public void Yes(){
		Application.Quit();
	}

	public void Back(){
		DisplayMainMenu();
	}

	//PlayMenu
	public void NewGame(){
		SceneManager.LoadScene("game");
	}
	
	public void Continue(){
		SceneManager.LoadScene("game");
	}
	
	public void LoadGame(){
		SceneManager.LoadScene("game");
	}
	
}
