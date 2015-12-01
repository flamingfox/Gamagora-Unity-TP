using UnityEngine;
using System.Collections;

public class TPSCamera : MonoBehaviour
{

	public Turret player;
	public float distanceCameraPlayer = 3f;
	public float hauteurCameraPlayer = 1f;
	private Vector3 positionRTS;
	private Quaternion rotationRTS;

	void Start ()
	{
		positionRTS = transform.position;
		rotationRTS = transform.rotation;
	}

	/*
	// Update is called once per frame
	void Update ()
	{
		transform.position = positionRTS;
		transform.rotation = rotationRTS;
	}
	*/

}
