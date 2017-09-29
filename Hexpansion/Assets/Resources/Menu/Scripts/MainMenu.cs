﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class MainMenu : MonoBehaviour
{

	public Animator CameraAnimator;
	//MainMenu
	public GameObject BtnPlay;
	public GameObject BtnSettings;
	public GameObject BtnExit;
	
	//PlayMenu
	public GameObject BtnContinue;
	public GameObject BtnNewGame;
	public GameObject BtnLoadGame;
	public GameObject BtnBack;
	public GameObject BtnAreYouSure;

	//Menus
	public void EnableMainMenu() {
		BtnAreYouSure.SetActive(false);
		BtnPlay.SetActive(true);
		BtnSettings.SetActive(true);
		BtnExit.SetActive(true);
	}
	
	
	public void DisableMainMenu() {
		BtnPlay.SetActive(false);
		BtnSettings.SetActive(false);
		BtnExit.SetActive(false);
	}
	
	public void EnablePlayMenu() {
		BtnAreYouSure.SetActive(false);
		BtnContinue.SetActive(true);
		BtnNewGame.SetActive(true);
		BtnLoadGame.SetActive(true);
	}
	
	//Animations
	public void DisplayMainMenu() {
		CameraAnimator.SetBool("PlayMenu", false);
		CameraAnimator.SetBool("SettingsMenu", false);
		CameraAnimator.SetBool("MainMenu", true);
	}

	public void DisplayPlayMenu() {
		CameraAnimator.SetBool("MainMenu", false);
		CameraAnimator.SetBool("PlayMenu", true);
	}
	
	public void DisplaySettingsMenu() {
		
		CameraAnimator.SetBool("MainMenu", false);
		CameraAnimator.SetBool("SettingsMenu", true);
	}

	//Approvals
	public void PromptAreYouSure() {
		BtnAreYouSure.SetActive(true);
		DisableMainMenu();
	}

	public void No() {
		BtnAreYouSure.SetActive(false);
		EnableMainMenu();
	}

	public void Yes(){
		Application.Quit();
	}

	public void Back(){
		DisplayMainMenu();
	}

	//PlayMenu
	public void NewGame(){
		Application.LoadLevel("game");
	}
	
	public void Continue(){
		Application.LoadLevel("game");
	}
	
	public void LoadGame(){
		Application.LoadLevel("game");
	}
	
}
