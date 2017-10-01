using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PlayMenu : MonoBehaviour
{

//ToDo: move into loops
	//Slider GameObjects
	public GameObject BoardSizeSliderGo;
	public GameObject ColorCountSliderGo;
	public GameObject TurnTimeSliderGo;

		
	//Slider
	private Slider _boardSizeSlider;
	private Slider _colorCountSlider;
	private Slider _turnTimeSlider;
	

	//Slider Values
	private int _boardSizeSliderValue;
	private int _colorCountSliderValue;
	private int _turnTimeSliderValue;

	//Slider Text
	private Text _boardSizeSliderText;
	private Text _colorCountSliderText;
	private Text _turnTimeSliderText;
	
	//Slider Text Values
	private string _boardSizeSliderTextValue;
	private string _colorCounteSliderTextValue;
	private string _turnTimeSliderTextValue;
	private int _turnTimeMax;
	
	
	void Start (){
		//Initialize Slider
		_boardSizeSlider = BoardSizeSliderGo.GetComponent<Slider>();
		_colorCountSlider = ColorCountSliderGo.GetComponent<Slider>();
		_turnTimeSlider = TurnTimeSliderGo.GetComponent<Slider>();
		
		//Initialize Slider Values
		_boardSizeSlider.value = PlayerPrefs.GetInt("BoardSize");
		_colorCountSlider.value = PlayerPrefs.GetInt("ColorCount");
		_turnTimeSlider.value = PlayerPrefs.GetInt("TurnTime");
		
		//Iinitialize Text Objects
		_boardSizeSliderText = BoardSizeSliderGo.GetComponentInChildren<Text>();
		_colorCountSliderText = ColorCountSliderGo.GetComponentInChildren<Text>();
		_turnTimeSliderText = TurnTimeSliderGo.GetComponentInChildren<Text>();
		
		//Initialize Text Values to be concatted later on
		_boardSizeSliderTextValue = _boardSizeSliderText.text;
		_colorCounteSliderTextValue = _colorCountSliderText.text;
		_turnTimeSliderTextValue = _turnTimeSliderText.text;
		_turnTimeMax = (int) _turnTimeSlider.maxValue;

		UpdateBoardSizeSettings();
		UpdateColorCountSettings();
		UpdateTurnTimeSettings();

	}
	
	private void Update(){
		_boardSizeSliderValue = (int) _boardSizeSlider.value;
		_colorCountSliderValue = (int)_colorCountSlider.value;
		_turnTimeSliderValue =  (int) _turnTimeSlider.value;
	}

	
	public void UpdateBoardSizeSlider(){
		PlayerPrefs.SetInt("BoardSize", _boardSizeSliderValue);
	}
	
	public void UpdateColorCountSlider(){
		PlayerPrefs.SetInt("ColorCount", _colorCountSliderValue);
	}
	
	public void UpdateTurnTimeSlider(){
		PlayerPrefs.SetInt("TurnTime", _turnTimeSliderValue);
	}

	public void UpdateBoardSizeSettings(){
		_boardSizeSliderText.text = _boardSizeSliderTextValue + PlayerPrefs.GetInt("BoardSize");
	}

	public void UpdateColorCountSettings(){
		_colorCountSliderText.text = _colorCounteSliderTextValue + PlayerPrefs.GetInt("ColorCount");
	}
	
	public void UpdateTurnTimeSettings(){
		int turnTime = PlayerPrefs.GetInt("TurnTime");
		if (turnTime == _turnTimeMax){
			_turnTimeSliderText.text = _turnTimeSliderTextValue + "-";
		}
		else {
			_turnTimeSliderText.text = _turnTimeSliderTextValue + PlayerPrefs.GetInt("TurnTime");
		}
	}
	

}