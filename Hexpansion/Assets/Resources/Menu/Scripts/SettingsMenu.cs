using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{


	//Audio
	public GameObject MusicSlider;
	public GameObject SoundSlider;

	
	//Difficulty
	public GameObject TurnTimeSlider;
	
	//Sensitivity
	public GameObject MouseSensitiviySlider;


	private float _musicSliderValue =1f;
	private float _soundSliderValue = 1f;
	private float _mouseSensitivitySliderValue = 1f;
	//Todo: Tooltips and display?
	
	void Start (){
		//Audio
		MusicSlider.GetComponent<Slider>().value = PlayerPrefs.GetFloat("MusicVolume");
		SoundSlider.GetComponent<Slider>().value = PlayerPrefs.GetFloat("SoundSlider");
		
		//Difficulty
		TurnTimeSlider.GetComponent<Slider>().value = PlayerPrefs.GetFloat("TurnTime");
	}
	
	private void Update(){
		_musicSliderValue = MusicSlider.GetComponent<Slider>().value;
		_soundSliderValue = SoundSlider.GetComponent<Slider>().value;
		_mouseSensitivitySliderValue = TurnTimeSlider.GetComponent<Slider>().value;
	}

	public void UpdateMusicSlider(){
		PlayerPrefs.SetFloat("MusicVolume", _musicSliderValue);
	}
	
	public void UpdateSoundSlider(){
		PlayerPrefs.SetFloat("SoundVolume", _soundSliderValue);
	}
	
	public void UpdateMouseSensitivitySlider(){
		PlayerPrefs.SetFloat("MouseSensitivity", _mouseSensitivitySliderValue);
	}

}
