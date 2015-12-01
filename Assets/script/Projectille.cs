using UnityEngine;
using System.Collections;

public class Projectille : MonoBehaviour, IKillable
{
	
	public GameObject mesh;
	public float speed = 2f;
	public float lifeDistance = 50f;
	public Enemy target = null;
	public int damage = 1;
	public float rotationSpeed = 5f;
	public ParticuleManager impactEffect;
	private Vector3 direction;
	private GameObject gunner;
	private bool dead = false;

	void Start ()
	{
		impactEffect.parent = this;
	}

	void OnEnable ()
	{
		mesh.SetActive (true);
		dead = false;
		impactEffect.init ();
	}

	// Update is called once per frame
	void Update ()
	{
		if (!dead) {

			if (Vector3.Distance (this.transform.position, gunner.transform.position) < lifeDistance) {

				this.transform.position += (direction * speed);

				if (target == null || target.isDead ()) {
					target = null;
				} else {

					float stepRotation = rotationSpeed * Time.deltaTime;

					Vector3 newDir = Vector3.RotateTowards (direction,
				                                        target.transform.position - this.transform.position,
				                                        stepRotation,
				                                        0f);

					this.setDirection (newDir);

				}
			} else {
				kill ();
			}
		}
	}

	public void setDirection (Vector3 _direction)
	{
		_direction.Normalize ();
		mesh.transform.LookAt (mesh.transform.position + _direction);
		mesh.transform.Rotate (90, 0, 0);

		//direction = _direction;

		direction = Vector3.Cross (mesh.transform.forward, mesh.transform.right);
	}

	public void setGunner (GameObject _gunner)
	{
		gunner = _gunner;
	}

	void OnTriggerEnter (Collider collision)
	{
		if (collision.tag == "enemy" && !collision.GetComponent<Enemy>().isDead() ) {
			collision.GetComponent<Enemy> ().hit (damage);
			impact ();
		} else if (collision.tag == "environnement") {
			kill ();
		}
	}

	void impact ()
	{
		dead = true;
		impactEffect.run ();
		mesh.SetActive (false);
	}

	public void kill ()
	{
		gameObject.SetActive (false);
	}
}
