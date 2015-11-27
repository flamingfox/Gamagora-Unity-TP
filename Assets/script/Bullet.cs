using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	
	public GameObject projectile;
	public float speed = 2f;
	public float lifeTime = 5f;

	private Vector3 direction;

	private GameObject gunner;
	public int damage = 1;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (lifeTime > 0f) {

			Vector3 directionBullet = Vector3.Cross( projectile.transform.forward, projectile.transform.right);
			projectile.transform.position +=  (directionBullet * speed);

			GameObject[] listEnemy = GameObject.FindGameObjectsWithTag("enemy");


			/*
			foreach(GameObject gO in listEnemy){
				//Debug.Log( gO );
				//Debug.Log(Vector3.Distance(gO.transform.position, this.transform.position));
				if( gO.GetComponent<Enemy>().collision(this.transform.position) ){
					gO.GetComponent<Enemy>().hit(damage);

					Destroy(this.gameObject);
					break;
				}
			}
			*/

			lifeTime -= Time.deltaTime;
		} else
			GameObject.Destroy (projectile);
	}

	public void setDirection(Vector3 _direction){
		_direction.Normalize ();
		projectile.transform.LookAt (projectile.transform.position + direction);
		projectile.transform.Rotate(90, 0, 0);

		direction = _direction;
	}

	public void setGunner(GameObject _gunner){
		gunner = _gunner;
	}

	void OnTriggerEnter(Collider collision) {
		/*if (collision.gameObject != gunner && collision.gameObject.tag != projectile.gameObject.tag) {
			Debug.Log ("works!");
			GameObject.Destroy (projectile);
		}*/		
	}
}
