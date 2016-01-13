using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Enemy : Poolable, IKillable
{
	private int PVMax = 50;
	public int PV = 50;
	public float speed = 5f;

	public GameObject mesh;
	public ParticuleManager deathEffect;

	private bool dead = false;

	public Slider healthBar;

	void Start(){
		deathEffect.parent = this;
		PVMax = PV;
		healthBar.maxValue = PVMax;
		healthBar.value = PV;
	}

	// Use this for initialization
	void OnEnable () {
		GetComponent<Rigidbody> ().isKinematic = false;
		GetComponent<BoxCollider> ().isTrigger = false;
		mesh.SetActive(true);
		deathEffect.init ();
		dead = false;

		healthBar.gameObject.SetActive (true);
		healthBar.maxValue = PVMax;
		healthBar.value = PV;

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
		healthBar.value = PV;
		if (PV <= 0) {
			dying ();
			return true;
		}

		return false;
	}

	public void kill(){
		dead = true;
		poolRelease ();
	}

	private void dying(){
		iTween.Stop(this.gameObject);
		healthBar.gameObject.SetActive (false);
		GetComponent<BoxCollider> ().isTrigger = true;
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
