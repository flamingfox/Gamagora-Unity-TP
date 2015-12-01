using UnityEngine;
using System.Collections;

public class GameState : MonoBehaviour {

	[Range(0, 50)]
	public int PlayerPV = 10;
	public bool gameOver = false;

	private float startSurviveTime = 0f;

	public void Start(){
		startSurviveTime = Time.time;
	}

	public void EnemyReach(){
		if (!gameOver) {
			PlayerPV--;

			if (PlayerPV <= 0) {
				float surviveTime = Time.time - startSurviveTime;

				Debug.Log ("surviveTime : " + surviveTime);

				if (PlayerPrefs.GetFloat ("surviveTime") > surviveTime) {
					PlayerPrefs.SetFloat ("surviveTime", surviveTime);
					PlayerPrefs.Save ();
					gameOver = true;
				}
			}
		}
	}
}
