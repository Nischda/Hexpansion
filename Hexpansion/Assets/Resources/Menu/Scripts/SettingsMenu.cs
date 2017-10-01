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

	private float _musicSliderValue =0.5f;
	private float _soundSliderValue = 0.5f;
	//Todo: Tooltips and display?
	
	void Start (){
		//Audio
		MusicSlider.GetComponent<Slider>().value = PlayerPrefs.GetFloat("MusicVolume");
		SoundSlider.GetComponent<Slider>().value = PlayerPrefs.GetFloat("SoundVolume");
	}
	
	private void Update(){
		_musicSliderValue = MusicSlider.GetComponent<Slider>().value;
		_soundSliderValue = SoundSlider.GetComponent<Slider>().value;
	}

	
	public void UpdateMusicSlider(){
		PlayerPrefs.SetFloat("MusicVolume", _musicSliderValue);
	}
	
	public void UpdateSoundSlider(){
		PlayerPrefs.SetFloat("SoundVolume", _soundSliderValue);
	}
	

}
