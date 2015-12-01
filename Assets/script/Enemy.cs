using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour, IKillable
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
	void Update () {
	
	}

	public void hit(int damage){
		PV -= damage;

		if (PV <= 0)
			death ();
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

	public bool isDead(){
		return dead;
	}
}
