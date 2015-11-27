using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public int PV = 50;

	private float distanceContact;

	// Use this for initialization
	void Start () {
		distanceContact = Vector3.Distance (this.transform.position, this.GetComponent<Collider> ().bounds.max);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void hit(int damage){
		PV -= damage;

		if (PV <= 0)
			Destroy (this.gameObject);
	}

	public bool collision(Vector3 position){
		if (Vector3.Distance (position, this.transform.position) <= distanceContact)
			return true;
		return false;
	}
}
