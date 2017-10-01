using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

	public SoundManager SoundManager;
	private AudioSource _musicSource;
	private AudioSource _soundSource;
	
	void Start (){
		_musicSource = this.GetComponent<AudioSource>();
		_soundSource = SoundManager.GetComponent<AudioSource>();
		UpdateMusicVolume();
		UpdateSoundVolume();
	}


	public void UpdateMusicVolume() {
		_musicSource.volume = PlayerPrefs.GetFloat("MusicVolume");
	}
	
	public void UpdateSoundVolume() {
		_soundSource.volume = PlayerPrefs.GetFloat("SoundVolume");
	}

}
