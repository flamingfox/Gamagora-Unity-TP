using UnityEngine;
using System.Collections;


public class AudioSourcePoolable : MonoBehaviour, Poolable
{
	private AudioSource audioSource;

	/**********/
	PoolingManager poolParent;
	
	public void setPoolParent(PoolingManager parent){
		poolParent = parent;
	}
	/**********/

	void Awake(){
		audioSource = this.gameObject.GetComponent<AudioSource> ();
	}

	public float pitch{
		get { return audioSource.pitch; }
		set { audioSource.pitch = value; }
	}

	public float volume{
		get { return audioSource.volume; }
		set { audioSource.volume = value; }
	}

	public void PlayOneShot(AudioClip audioClip, float volumeScale){
		audioSource.PlayOneShot (audioClip, volumeScale);
		Invoke ("poolRelease", audioClip.length);
	}

	public void poolRelease(){
		poolParent.releaseObject(this.gameObject);
	}
	/**********/
}