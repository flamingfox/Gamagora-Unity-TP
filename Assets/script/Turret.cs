﻿using UnityEngine;
using System.Collections;

public class Turret : MonoBehaviour
{

	public GameObject tower;
	public GameObject support;
	public GameObject gun;
	public Enemy target;
	public PollingManager bulletManager;
	public PollingManager pollingEnemy;
	public ParticleSystem fireEffect;
	[Range(0, 5f)]
	public float speedRotate = 2f;
	[Range(0, 100f)]
	public float shootPerSecond = 0.25f;
	[Range(0, 50f)]
	public float range = 20f;
	[Range(0.5f, 5f)]
	public float congestion = 1f;
	public bool auto = false;
	public bool fireAuto = false;

	private float nextFire = 0f;
	private float gunRotationSpeedCoeff = 1;

	// Update is called once per frame
	void Update ()
	{
		if (!auto) {
			if (Input.GetButton ("Horizontal") && Input.GetAxisRaw ("Horizontal") > 0) {
				support.transform.RotateAround (support.transform.position, Vector3.up, speedRotate);
			} else if (Input.GetButton ("Horizontal") && Input.GetAxisRaw ("Horizontal") < 0) {
				support.transform.RotateAround (support.transform.position, Vector3.up, -speedRotate);
			} else if (Input.GetButton ("Vertical") && Input.GetAxisRaw ("Vertical") > 0) {

				if (support.transform.eulerAngles.x < 30 || support.transform.eulerAngles.x > 320) {
					support.transform.Rotate (Vector3.left * speedRotate);
				}
			} else if (Input.GetButton ("Vertical") && Input.GetAxisRaw ("Vertical") < 0) {
				if (support.transform.eulerAngles.x < 20 || support.transform.eulerAngles.x > 310) {
					support.transform.Rotate (Vector3.left * -speedRotate);
				}
			}
		} else {

			if (target == null) {
				float distance = range;
				ArrayList listEnemy = pollingEnemy.getListActive ();
				foreach (GameObject gO in listEnemy) {

					if (!gO.GetComponent<Enemy> ().isDead ()) {
						if (Vector3.Distance (gO.transform.position, this.transform.position) <= distance) {
							target = gO.GetComponent<Enemy> ();
							distance = Vector3.Distance (gO.transform.position, this.transform.position);
						}
					}
				}
			} else {

				if(target.isDead ()){
					target = null;
				}
				else{
					Vector3 directionTarget = target.transform.position - support.transform.position;
					directionTarget = directionTarget.normalized;

					Debug.DrawLine (support.transform.position, support.transform.position + directionTarget * 2f, Color.cyan);

					float stepRotation = speedRotate * Time.deltaTime;

					Vector3 newDir = Vector3.RotateTowards (support.transform.forward,
	                                                    directionTarget,
				                                       	stepRotation,
	                                                  	0f);

					support.transform.rotation = Quaternion.LookRotation (newDir);
				}
			}

		}

		Debug.DrawRay (support.transform.position, support.transform.forward * 2f, Color.red, 0f);

		if (Input.GetButtonDown ("Jump")) {
			auto = !auto;
			
			if (!auto)
				target = null;
		}
		else if (Input.GetButton ("Fire1") || (fireAuto && target != null) ) {

			gun.transform.RotateAround (support.transform.position, support.transform.forward, (shootPerSecond) * gunRotationSpeedCoeff);

			if (Time.time > nextFire) {
				fireEffect.Play ();

				nextFire = Time.time + 1/shootPerSecond;
				GameObject bullet = bulletManager.getFirstAvailable ();
				bullet.transform.position = gun.transform.position;

				Bullet ball = bullet.GetComponent<Bullet> ();
				ball.setGunner (tower);
				ball.damage = 10;
				ball.target = target;
				ball.lifeDistance = range;
				ball.setDirection (support.transform.forward);
				ball.pollingEnemy = pollingEnemy;

			}
		}
	}

	public void OnDrawGizmosSelected(){
		int size = 24 ; //Total number of points in circle.
		float theta_scale = (2.0f * 3.14f)/size;             //Set lower to add more points

		Vector3 pos, posPrecedent;

		/************ ghost Range *************/
		Gizmos.color = Color.green;

		posPrecedent = new Vector3 (range, 0, 0);

		for(int i = 1; i <= size; i++) {
			float theta = i * theta_scale;
			float x = range*Mathf.Cos(theta);
			float z = range*Mathf.Sin(theta);
			
			pos = new Vector3(x, 0, z);
			Gizmos.DrawLine (posPrecedent, pos);
			posPrecedent = pos;
		}

		posPrecedent = new Vector3 (range, 0, 0);
		
		for(int i = 1; i <= size/2; i++) {
			float theta = i * theta_scale;
			float x = range*Mathf.Cos(theta);
			float y = range*Mathf.Sin(theta);
			
			pos = new Vector3(x, y, 0);
			Gizmos.DrawLine (posPrecedent, pos);
			posPrecedent = pos;
		}

		posPrecedent = new Vector3 (0, 0, range);
		
		for(int i = 1; i <= size/2; i++) {
			float theta = i * theta_scale;

			float y = range*Mathf.Sin(theta);
			float z = range*Mathf.Cos(theta);
			
			pos = new Vector3(0, y, z);
			Gizmos.DrawLine (posPrecedent, pos);
			posPrecedent = pos;
		}

		/************ ghost Congestion *************/
		Gizmos.color = Color.red;
		
		posPrecedent = new Vector3 (congestion, 0, 0);
		
		for(int i = 1; i <= size; i++) {
			float theta = i * theta_scale;
			float x = congestion*Mathf.Cos(theta);
			float z = congestion*Mathf.Sin(theta);
			
			pos = new Vector3(x, 0, z);
			Gizmos.DrawLine (posPrecedent, pos);
			posPrecedent = pos;
		}
	}

	public Vector3 getGunPosition ()
	{
		return support.transform.position;
	}

	public Vector3 getGunDirection(){
		return support.transform.forward;
	}

	public Quaternion getGunRotation ()
	{
		return support.transform.rotation;
	}
}




























