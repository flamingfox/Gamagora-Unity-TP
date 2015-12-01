using UnityEngine;
using System.Collections;

public class EndScript : MonoBehaviour {

	public GameState gameState;

	void OnTriggerEnter (Collider collision)
	{
		if (collision.tag == "enemy" && !collision.GetComponent<Enemy>().isDead() ) {
			collision.GetComponent<Enemy> ().kill();
			gameState.EnemyReach();
		}
	}
}
