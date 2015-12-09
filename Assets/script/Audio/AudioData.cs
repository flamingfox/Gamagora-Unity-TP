using UnityEngine;
using UnityEngine.Audio;
using System.Collections;
using System.Collections.Generic;


public class AudioData : AudioPlayer
{
	new public string name;
	public AudioMixerGroup groupMixer;
	public AudioClip[] sounds;

	public enum choise {unique, random, sequence};

	public choise lectureChoise;

	private int lectureIndice = 0;

	void Start(){
		AudioManager.Instance.addAudioData (this);
	}

	public AudioClip getSound(){

		switch (lectureChoise) {
		case choise.unique :
			lectureIndice = 0;
			break;

		case choise.sequence :
			lectureIndice = (lectureIndice+1)%sounds.Length;
			break;

		case choise.random :
			lectureIndice = (int)(Random.value*sounds.Length);
			break;
		}

		return sounds[lectureIndice];
	}
}