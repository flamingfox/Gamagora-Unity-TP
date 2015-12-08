using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioPlayer : MonoBehaviour{

	public AudioClip sound;
	[Range(0,10)]
	public uint priority = 5;
	[Range(0f,1f)]
	public float volume = 1f;

	public void play(){

		KeyValuePair<string, float>[] options = new KeyValuePair<string, float>[2];

		options[0] = new KeyValuePair<string, float> ("volume", volume);
		options[1] = new KeyValuePair<string, float> ("priority", priority);

		AudioManager.Instance.Play (sound, options);
	}
}
