using UnityEngine;
using System.Collections;

public class Buildable : MonoBehaviour {

	public bool spaceAvailable = true;

	public GameObject turret_gatling;
	public GameObject turret_missile;
	public GameObject turret_grenade;

	void OnMouseDown(){
		//Debug.Log ("life : " + PV);

		if (spaceAvailable) {

			GameObject instance = Instantiate (turret_gatling, transform.position, transform.rotation) as GameObject;
			instance.transform.position = transform.position + new Vector3 (0f, 0.5f, 0f);

			this.spaceAvailable = false;

			instance.GetComponent<Turret> ().poolingEnemy = GameManager.Instance.enemyPooling;
			instance.GetComponent<Turret> ().buildFinish ();


			//Destroy(gameObject.GetComponent<Buildable>());

		}
	}
}
