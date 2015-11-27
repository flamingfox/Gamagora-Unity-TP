using UnityEngine;
using System.Collections;

public class EnemySpawn : MonoBehaviour {

	public GameState gameState;
	public PollingManager enemyPolling;
	public GameObject SpawnArea;
	[Range(0.1f, 50f)]
	public float SpawnPerSecond = 0.5f;

	public bool spawning = true;

	private float nextSpawn = 0f;
		
	// Update is called once per frame
	void Update () {
	
		if (Time.time > nextSpawn && spawning) {
			
			nextSpawn = Time.time + 1/SpawnPerSecond;
			GameObject enemy = enemyPolling.getFirstAvailable();

			enemy.GetComponent<Enemy>().PV = 50;
			enemy.GetComponent<Enemy>().gameState = gameState;

			Vector3 colliderSize = SpawnArea.GetComponent<BoxCollider>().size;

			Vector3 variance = new Vector3(
				Random.Range(-colliderSize.x, -colliderSize.x),
				Random.Range(-colliderSize.y, -colliderSize.y),
				Random.Range(-colliderSize.z, -colliderSize.z)
				);

			enemy.transform.position = SpawnArea.transform.position + variance;


		}
	}
}
