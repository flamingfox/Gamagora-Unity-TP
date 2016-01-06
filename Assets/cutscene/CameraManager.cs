using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraManager : MonoBehaviour {

	public List<GameObject> Cameras;
	private int indice = 0;

	public void next(){
		Cameras [indice].SetActive (false);
		indice++;
		Cameras [indice].SetActive (true);
	}
}
