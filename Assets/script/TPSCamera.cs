using UnityEngine;
using System.Collections;

public class TPSCamera : MonoBehaviour {

	public GameObject player;

	public float distanceCameraPlayer = 3f;
	public float hauteurCameraPlayer = 1f;
	
	// Update is called once per frame
	void Update () {

		Vector3 positionCamera = player.transform.position;

		positionCamera -= player.transform.forward * distanceCameraPlayer;
		positionCamera += Vector3.Cross (player.transform.forward, player.transform.right) * hauteurCameraPlayer;

		this.gameObject.transform.position = positionCamera;

		this.gameObject.transform.rotation = player.transform.rotation;

	}
}
