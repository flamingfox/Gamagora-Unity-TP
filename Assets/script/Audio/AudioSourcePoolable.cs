using UnityEngine;
using UnityEngine.Audio;
using System.Collections;


public class AudioSourcePoolable : Poolable
{
	private AudioSource audioSource;

	void Awake(){
		audioSource = this.gameObject.GetComponent<AudioSource> ();
	}

	void OnEnable(){
		audioSource.pitch = 1f;
		audioSource.volume = 1f;
		audioSource.loop = false;
		audioSource.mute = false;

		//audioSource.
	}

	public float pitch{
		get { return audioSource.pitch; }
		set { audioSource.pitch = value; }
	}

	public float volume{
		get { return audioSource.volume; }
		set { audioSource.volume = value; }
	}

	public int priority{
		get { return audioSource.priority; }
		set { audioSource.priority = value; }
	}

	public AudioMixerGroup outputAudioMixerGroup{
		get { return audioSource.outputAudioMixerGroup; }
		set { audioSource.outputAudioMixerGroup = value; }
	}

	public bool loop{
		get { return audioSource.loop; }
		set { audioSource.loop = value; }
	}

	public void Play(AudioClip sound){
		audioSource.clip = sound;

		audioSource.Play ();
		Invoke ("poolRelease", sound.length);
	}

	public void PlayOneShot(AudioClip audioClip, float volumeScale){

		audioSource.PlayOneShot (audioClip, volumeScale);
		Invoke ("poolRelease", audioClip.length);
	}
}