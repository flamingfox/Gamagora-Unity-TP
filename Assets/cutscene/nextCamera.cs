using UnityEngine;
using System.Collections;

public class nextCamera : MonoBehaviour {

	public CameraManager cameraManager;

	public void next(){
		cameraManager.next ();
	}
}
