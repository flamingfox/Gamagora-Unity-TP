using UnityEngine;
using System.Collections;

public class Turret : MonoBehaviour {

	public GameObject tower;
	public GameObject support;
	public GameObject gun;

	public GameObject target;

	public GameObject bullet;
	public ParticleSystem effet;

	public float speedRotate = 2f;

	public float speedFire = 0.25f;
	private float nextFire = 0f;

	private float gunRotationSpeedCoeff = 1;

	public bool auto = false;
	
	// Update is called once per frame
	void Update () {
		if (target == null || !auto) {
			if (Input.GetButton ("Horizontal") && Input.GetAxisRaw("Horizontal") > 0) {
				support.transform.RotateAround (support.transform.position, Vector3.up, speedRotate);
			}
			else if (Input.GetButton ("Horizontal") && Input.GetAxisRaw("Horizontal") < 0) {
				support.transform.RotateAround (support.transform.position, Vector3.up, -speedRotate);
			}
			else if (Input.GetButton ("Vertical") && Input.GetAxisRaw("Vertical") > 0 ) {

				if(support.transform.eulerAngles.x < 30 || support.transform.eulerAngles.x > 320){
					support.transform.Rotate ( Vector3.left * speedRotate);
				}
			}
			else if (Input.GetButton ("Vertical") && Input.GetAxisRaw("Vertical") < 0 ) {
				if( support.transform.eulerAngles.x < 20 || support.transform.eulerAngles.x > 310){
					support.transform.Rotate ( Vector3.left * -speedRotate);
				}
			}
		}
		else {
			Vector3 directionTarget = target.transform.position - support.transform.position;
			directionTarget = directionTarget.normalized;

			Debug.DrawLine(support.transform.position, support.transform.position + directionTarget*2f, Color.cyan);

			float stepRotation = speedRotate * Time.deltaTime;


			Vector3 newDir = Vector3.RotateTowards (support.transform.forward,
                                                    directionTarget,
			                                       	stepRotation,
                                                  	0f);

			support.transform.rotation = Quaternion.LookRotation(newDir);

		}

		Debug.DrawRay (support.transform.position, support.transform.forward*2f, Color.red, 0f);

		if (Input.GetButton ("Fire1")) {

			gun.transform.RotateAround (support.transform.position, support.transform.forward, (1/speedFire)*gunRotationSpeedCoeff );

			if (Time.time > nextFire) {
				effet.Play();

				nextFire = Time.time + speedFire;
				GameObject clone = Instantiate (bullet, support.transform.position + support.transform.forward * 1.5f, support.transform.rotation) as GameObject;
				Bullet ball = (Bullet)clone.GetComponent<Bullet> ();
				ball.setGunner (tower);
				ball.damage = 10;
				ball.setDirection (gun.transform.forward);
			}
		}
		else if(Input.GetButtonDown ("Jump")){
			auto = !auto;
		}


	}
}
