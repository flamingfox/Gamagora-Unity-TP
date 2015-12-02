using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : Singleton<AudioManager> {

	public PoolingManager audioSources;

	public bool soundOn = true;

	public bool useFilenameAsSoundName = true;
	public List<string> soundNames;
	public AudioClip[] sounds;
	Dictionary<string,AudioClip> soundMap = new Dictionary<string,AudioClip>();

	public int numberOfChannels = 16;

	void Awake(){
		audioSources.initNbInstanciate = numberOfChannels;

		for (int i = 0; i < sounds.Length; i++) {
			if (useFilenameAsSoundName) {
				soundNames.Add(sounds[i].name);
				soundMap.Add(sounds[i].name, sounds[i]);
			} else {
				soundMap.Add(soundNames[i], sounds[i]);
			}
		}
	}


	public void Play(string soundname) {
		Play(soundname, 1.0f, 1.0f);
	}

	public void Play(string soundname, float volume) {
		Play(soundname, volume, 1.0f);
	}

	public void Play(string soundname, float volume, float pitch) {
		if (!soundMap.ContainsKey(soundname)) {
			Debug.LogWarning("SoundManager: Tried to play undefined sound: " + soundname);
			return;
		}
		if (soundOn) {

			AudioSourcePoolable audioPoolable = audioSources.getObject().GetComponent<AudioSourcePoolable>();

			audioPoolable.pitch = pitch;
			audioPoolable.volume = volume;
			float v = volume;
			audioPoolable.PlayOneShot(soundMap[soundname], v);

			//audio.transform

		}
	}
}
