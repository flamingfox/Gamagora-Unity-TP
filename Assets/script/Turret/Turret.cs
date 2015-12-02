using UnityEngine;
using System.Collections;

abstract public class Turret : MonoBehaviour
{

	public GameObject tower;
	public GameObject support;
	public GameObject gun;
	public Enemy target;
	public PollingManager pollingEnemy;
	public ParticleSystem fireEffect;
	[Range(0, 5f)]
	public float
		speedRotate = 2f;
	[Range(0, 100f)]
	public float
		shootPerSecond = 0.25f;
	[Range(0, 50f)]
	public float
		range = 20f;
	[Range(1, 100)]
	public int
		damage = 1;
	private float nextFire = 0f;
	public float gunRotationSpeedCoeff = 1;

	// Update is called once per frame
	void Update ()
	{
		if (target == null) {
			target = selectTarget();
		} else {

			if (target.isDead () || Vector3.Distance(transform.position, target.transform.position) > range) {
				target = null;
			} else {
				aim ();
			}
		}


		Debug.DrawRay (support.transform.position, support.transform.forward * 2f, Color.red, 0f);

		if (target != null) {

			gun.transform.RotateAround (support.transform.position, support.transform.forward, shootPerSecond * gunRotationSpeedCoeff);

			if (Time.time > nextFire && Vector3.Angle (support.transform.forward, target.transform.position - support.transform.position) < 5f) {

				fireEffect.Play ();
				nextFire = Time.time + 1 / shootPerSecond;

				fire ();
			}
		}
	}

	public void OnDrawGizmosSelected ()
	{
		int size = 24; //Total number of points in circle.
		float theta_scale = (2.0f * 3.14f) / size;             //Set lower to add more points

		Vector3 pos, posPrecedent;

		/************ ghost Range *************/
		Gizmos.color = Color.green;

		posPrecedent = new Vector3 (transform.position.x + range, 0, transform.position.z);

		for (int i = 1; i <= size; i++) {
			float theta = i * theta_scale;
			float x = range * Mathf.Cos (theta);
			float z = range * Mathf.Sin (theta);
			
			pos = new Vector3 (transform.position.x + x, 0, transform.position.z + z);
			Gizmos.DrawLine (posPrecedent, pos);
			posPrecedent = pos;
		}

		/*
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
		 */

		/************ ghost Congestion *************/
		/*
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
		*/
	}

	virtual protected Enemy selectTarget(){
		Enemy retour = null;

		float distance = range;
		ArrayList listEnemy = pollingEnemy.getListActive ();
		foreach (GameObject gO in listEnemy) {
			
			if (!gO.GetComponent<Enemy> ().isDead ()) {
				if (Vector3.Distance (gO.transform.position, this.transform.position) <= distance) {
					retour = gO.GetComponent<Enemy> ();
					distance = Vector3.Distance (gO.transform.position, this.transform.position);
				}
			}
		}

		return retour;
	}

	abstract protected void aim ();
	abstract protected void fire ();

	public Vector3 getGunPosition ()
	{
		return support.transform.position;
	}

	public Vector3 getGunDirection ()
	{
		return support.transform.forward;
	}

	public Quaternion getGunRotation ()
	{
		return support.transform.rotation;
	}
}




























