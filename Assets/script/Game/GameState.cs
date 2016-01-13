using UnityEngine;
using System.Collections;

public class GameState : Singleton<GameState> {

	[Range(0, 50)]
	public int PlayerPV = 10;
	public bool gameOver = false;

	private float startSurviveTime = 0f;

	private UIManager UI;

	public void Start(){
		startSurviveTime = Time.time;
		UI = GameManager.Instance.UI;
	}

	public void FixedUpdate(){
		UI.SetChrono(Time.time - startSurviveTime);
	}

	public void EnemyReach(){
		if (!gameOver) {
			PlayerPV--;
			UI.setPV(PlayerPV);
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
