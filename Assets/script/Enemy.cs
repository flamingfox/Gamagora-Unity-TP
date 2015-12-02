using UnityEngine;
using System.Collections;

public class Enemy : Poolable, IKillable
{
	public int PV = 50;
	public float speed = 5f;

	public GameObject mesh;
	public ParticuleManager deathEffect;

	private bool dead = false;

	void Start(){
		deathEffect.parent = this;
	}

	// Use this for initialization
	void OnEnable () {
		GetComponent<Rigidbody> ().isKinematic = false;
		GetComponent<BoxCollider> ().enabled = true;
		mesh.SetActive(true);
		deathEffect.init ();
		dead = false;

		Hashtable argsMoveTo = new Hashtable();
		argsMoveTo.Add ("path", iTweenPath.GetPath ("enemyPath"));
		argsMoveTo.Add ("speed", speed);
		argsMoveTo.Add ("easetype", "linear");
		argsMoveTo.Add ("orienttopath", true);
		argsMoveTo.Add ("oncomplete", "OnTarget");

		iTween.MoveTo (gameObject, argsMoveTo);
	}
	
	// Update is called once per frame
	//void Update () {}

	public bool hit(int damage){
		PV -= damage;

		if (PV <= 0) {
			dying ();
			return true;
		}

		return false;
	}

	public void kill(){
		poolRelease ();
	}

	private void dying(){
		iTween.Stop(this.gameObject);
		GetComponent<BoxCollider> ().enabled = false;
		GetComponent<Rigidbody> ().velocity = transform.forward*speed ;
		dead = true;
		Invoke ("disappear", 1f);
	}

	private void disappear(){
		transform.rotation.SetLookRotation( new Vector3 (0, 1, 0), new Vector3(0,1,0) );
		GetComponent<Rigidbody> ().isKinematic = true;
		mesh.SetActive (false);
		deathEffect.run();
	}

	public bool isDead(){
		return dead;
	}
}
