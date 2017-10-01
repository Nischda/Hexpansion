using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

	public AudioSource MusicSource;
	public AudioSource SoundSource;
	private AudioSource _soundSource;
	
	void Start (){
		UpdateMusicVolume();
		UpdateSoundVolume();
	}


	public void UpdateMusicVolume() {
		MusicSource.volume = PlayerPrefs.GetFloat("MusicVolume");
	}
	
	public void UpdateSoundVolume() {
		SoundSource.volume = PlayerPrefs.GetFloat("SoundVolume");
	}

}
