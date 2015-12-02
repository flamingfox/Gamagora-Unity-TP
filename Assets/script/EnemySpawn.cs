using UnityEngine;
using System.Collections;

public class EnemySpawn : MonoBehaviour {

	public GameState gameState;
	public PoolingManager enemyPooling;
	public GameObject spawnArea;
	[Range(0.1f, 50f)]
	public float spawnPerSecond = 0.5f;

	[Range(5, 1000)]
	public int spawnPV = 10;

	public bool spawning = true;

	private float nextSpawn = 0f;
		
	// Update is called once per frame
	void Update () {
	
		if (Time.time > nextSpawn && spawning) {
			
			nextSpawn = Time.time + 1/spawnPerSecond;
			GameObject enemy = enemyPooling.getObject();

			enemy.GetComponent<Enemy>().PV = spawnPV;

			Vector3 colliderSize = spawnArea.GetComponent<BoxCollider>().size;

			Vector3 variance = new Vector3(
				Random.Range(-colliderSize.x, -colliderSize.x),
				Random.Range(-colliderSize.y, -colliderSize.y),
				Random.Range(-colliderSize.z, -colliderSize.z)
				);

			enemy.transform.position = spawnArea.transform.position + variance;


		}
	}
}
