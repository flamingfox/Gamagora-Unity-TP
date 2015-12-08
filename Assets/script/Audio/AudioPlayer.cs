using UnityEngine;
using UnityEngine.Audio;
using System.Collections;
using System.Collections.Generic;

public class AudioPlayer : MonoBehaviour{

	public AudioClip sound;
	public AudioMixerGroup groupMixer;
	[Range(0,10)]
	public int priority = 5;
	[Range(0f,1f)]
	public float volume = 1f;
	[Range(0f,1f)]
	public float deltaPitch = 0f;

	public void play(){

		KeyValuePair<string, object>[] options = new KeyValuePair<string, object>[4];

		options[0] = new KeyValuePair<string, object> ("volume", volume);
		options[1] = new KeyValuePair<string, object> ("priority", priority);
		options[2] = new KeyValuePair<string, object> ("groupMixer", groupMixer.name);

		if(deltaPitch != 0f)
			options[3] = new KeyValuePair<string, object> ("pitch", 1f - (deltaPitch/2) + Random.value*deltaPitch);

		AudioManager.Instance.Play (sound, options);
	}
}
