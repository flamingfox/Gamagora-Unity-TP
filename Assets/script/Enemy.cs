using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour, IKillable
{
	public GameState gameState;
	public int PV = 50;
	public float speed = 5f;

	public GameObject mesh;
	public ParticuleManager deathEffect;

	private float distanceContact;

	private bool dead = false;

	void Start(){
		deathEffect.parent = this;
	}

	// Use this for initialization
	void OnEnable () {
		mesh.SetActive(true);
		deathEffect.init ();
		dead = false;

		distanceContact = Vector3.Distance (this.transform.position, this.GetComponent<Collider> ().bounds.max);

		Hashtable argsMoveTo = new Hashtable();
		argsMoveTo.Add ("path", iTweenPath.GetPath ("enemyPath"));
		argsMoveTo.Add ("speed", speed);
		argsMoveTo.Add ("easetype", "linear");
		argsMoveTo.Add ("orienttopath", true);
		argsMoveTo.Add ("oncomplete", "OnTarget");

		iTween.MoveTo (gameObject, argsMoveTo);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void hit(int damage){
		PV -= damage;

		if (PV <= 0)
			death ();
	}

	public bool collision(Vector3 position){
		if (Vector3.Distance (position, this.transform.position) <= distanceContact)
			return true;
		return false;
	}

	public void kill(){
		gameObject.SetActive (false);
	}

	private void death(){
		dead = true;
		mesh.SetActive (false);
		deathEffect.run ();
		iTween.Stop(this.gameObject);
	}

	public void OnTarget(){
		gameState.EnemyReach ();
		death ();
	}

	public bool isDead(){
		return dead;
	}
}
