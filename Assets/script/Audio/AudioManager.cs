using UnityEngine;
using UnityEngine.Audio;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : Singleton<AudioManager> {

	public PoolingManager audioSources;
	public AudioMixer mixer;
	public bool soundOn = true;

	public AudioData[] initSounds;
	Dictionary<string, AudioData> soundMap = new Dictionary<string,AudioData>();

	public int numberOfChannels = 16;

	void Awake(){
		audioSources.initNbInstanciate = numberOfChannels;

		for (int i = 0; i < initSounds.Length; i++) {
			soundMap.Add(initSounds[i].name, initSounds[i]);
		}
	}

	public void addAudioData (AudioData audioData)
	{
		foreach (KeyValuePair<string, AudioData> sound in soundMap) {
			if(sound.Key == audioData.name)
				return;
		}

		//sounds[sounds.Length] = audioData;
		soundMap.Add(audioData.name, audioData);
	}

	public void removeAudioData (AudioData audioData)
	{
		soundMap.Remove (audioData.name);
	}

	public uint Play(string soundname) {
		return Play(soundname, null);
	}

	public uint Play(string soundname, KeyValuePair<string, object>[] options) {
		if (!soundMap.ContainsKey(soundname)) {
			Debug.LogWarning("SoundManager: Tried to play undefined sound: " + soundname);
			return 0;
		}

		if (soundOn) {

			GameObject audioObject = audioSources.getFirstObjectAvailable();

			if(audioObject != null){
				AudioSourcePoolable audioPoolable = audioObject.GetComponent<AudioSourcePoolable>();

				foreach(KeyValuePair<string, object> option in options){

					switch(option.Key){

					case "volume":
						audioPoolable.volume = (float)option.Value;
						break;

					case "priority":
						audioPoolable.priority = (int)option.Value;
						break;

					case "pitch":
						audioPoolable.pitch = (float)option.Value;
						break;

					case "loop":
						audioPoolable.loop = (bool)option.Value;
						break;

					case "groupMixer":
						audioPoolable.outputAudioMixerGroup =  (AudioMixerGroup)option.Value;
						break;
					}

				}
				audioPoolable.Play(soundMap[soundname].getSound());

				//audioPoolable.PlayOneShot(soundMap[soundname], audioPoolable.volume);
				//audioPoolable.PlayOneShot(sound, audioPoolable.volume);

				//audio.transform
			}
		}

		return 0;
	}
}
