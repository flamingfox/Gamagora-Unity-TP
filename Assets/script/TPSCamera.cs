using UnityEngine;
using System.Collections;

public class TPSCamera : MonoBehaviour {

	public Turret player;

	public float distanceCameraPlayer = 3f;
	public float hauteurCameraPlayer = 1f;
	
	// Update is called once per frame
	void Update () {

		if (player != null) {
			if (!player.auto) {
				Vector3 positionCamera = player.getGunPosition();

				positionCamera -= player.getGunDirection() * distanceCameraPlayer;
				positionCamera += Vector3.Cross (player.transform.forward, player.transform.right) * hauteurCameraPlayer;

				this.gameObject.transform.position = positionCamera;
				this.gameObject.transform.rotation = player.getGunRotation();
			}
			else {
				transform.position = new Vector3(-12f, 15.5f, 8.8f);
				transform.rotation = Quaternion.Euler(50, 90, 0);
			}
		}
		else {
			transform.position = new Vector3(-12f, 15.5f, 8.8f);
			transform.rotation = Quaternion.Euler(50, 90, 0);
		}


	}
}
