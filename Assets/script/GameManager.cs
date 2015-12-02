using UnityEngine;
using System.Collections;

public class GameManager : Singleton<GameManager> {

	public GameState game;
	public PoolingManager enemyPooling;
	public AudioManager audioManager;
}
