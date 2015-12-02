using UnityEngine;
using System.Collections;

public class Buildable : MonoBehaviour {

	public GameObject turret_gatling;
	public GameObject turret_missile;
	public GameObject turret_grenade;

	void OnMouseDown(){
		//Debug.Log ("life : " + PV);
		GameObject instance = Instantiate (turret_gatling, transform.position, transform.rotation) as GameObject;
		instance.transform.position = transform.position + new Vector3(0f,0.5f,0f);

		instance.GetComponent<Turret> ().poolingEnemy = GameManager.Instance.enemyPooling;

		AudioManager.Instance.Play ("GatlingBuild");

		Destroy(gameObject.GetComponent<Buildable>());

	}
}
