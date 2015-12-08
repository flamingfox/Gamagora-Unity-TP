using UnityEngine;
using UnityEngine.Audio;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : Singleton<AudioManager> {

	public PoolingManager audioSources;
	public AudioMixer mixer;
	public bool soundOn = true;

	//public AudioClip[] sounds;
	//Dictionary<string,AudioClip> soundMap = new Dictionary<string,AudioClip>();

	public int numberOfChannels = 16;

	void Awake(){
		audioSources.initNbInstanciate = numberOfChannels;

		//for (int i = 0; i < sounds.Length; i++) {
		//	soundMap.Add(sounds[i].name, sounds[i]);
		//}
	}


	public void Play(AudioClip sound) {
		Play(sound, null);
	}

	public void Play(AudioClip sound, KeyValuePair<string, float>[] options) {
		//if (!soundMap.ContainsKey(soundname)) {
		//	Debug.LogWarning("SoundManager: Tried to play undefined sound: " + soundname);
		//	return;
		//}

		if (soundOn) {

			GameObject audioObject = audioSources.getObject();

			if(audioObject != null){
				AudioSourcePoolable audioPoolable = audioObject.GetComponent<AudioSourcePoolable>();

				foreach(KeyValuePair<string, float> option in options){

					switch(option.Key){

					case "volume":
						audioPoolable.volume = option.Value;
						break;

					case "priority":
						audioPoolable.priority = (int)option.Value;
						break;

					case "pitch":
						audioPoolable.pitch = option.Value;
						break;

					case "loop":
						if( option.Value != 0f)
							audioPoolable.loop = true;
						else
							audioPoolable.loop = false;
						break;

					}

				}
				audioPoolable.Play(sound);
				//audioPoolable.PlayOneShot(sound, audioPoolable.volume);

				//audio.transform
			}
		}
	}
}
