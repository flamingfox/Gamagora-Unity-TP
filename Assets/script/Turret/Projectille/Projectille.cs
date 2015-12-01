using UnityEngine;
using System.Collections;

public class Projectille : MonoBehaviour, IKillable
{
	
	public GameObject mesh;
	public float lifeDistance = 50f;
	public Enemy target = null;
	public int damage = 1;

	public ParticuleManager impactEffect;

	protected GameObject gunner;
	protected bool dead = false;

	void Start ()
	{
		impactEffect.parent = this;
	}

	virtual protected void OnEnable ()
	{
		mesh.SetActive (true);
		dead = false;
		impactEffect.init ();
	}

	// Update is called once per frame
	virtual protected void Update ()
	{
		if (!dead) {
			if (Vector3.Distance (this.transform.position, gunner.transform.position) > lifeDistance) {
				kill ();
			}
		}
	}

	public void setGunner (GameObject _gunner)
	{
		gunner = _gunner;
	}

	protected virtual void OnTriggerEnter (Collider collision)
	{
		if (collision.tag == "enemy" && !collision.GetComponent<Enemy>().isDead() ) {
			collision.GetComponent<Enemy> ().hit (damage);
			impact ();
		} else if (collision.tag == "environnement") {
			kill ();
		}
	}

	virtual protected void impact ()
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
