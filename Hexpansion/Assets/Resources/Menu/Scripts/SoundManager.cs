using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR.WSA.Input;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{

	public AudioClip ButtonClickSound;
	private AudioSource _audioSource;
	

	void Start (){
		_audioSource = this.GetComponent<AudioSource>();
	}

	public void PlayButtonClickSound(){
		_audioSource.PlayOneShot(ButtonClickSound);
	}
}
