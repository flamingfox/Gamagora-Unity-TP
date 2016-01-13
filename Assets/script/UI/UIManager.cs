using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

public class UIManager : MonoBehaviour {

	GameState game;

	public Text PVText;
	public Text VagueText;
	public Text ChronoTextMin;
	public Text ChronoTextSec;

	public ConstructionPanel cp;
	private RectTransform cpTransform;

	// Use this for initialization
	void Start () {
		game = GameState.Instance;
		setPV (game.PlayerPV);
		cpTransform = cp.GetComponent<RectTransform> ();
	}

	public void setPV(int PV){
		PVText.text = PV.ToString ();
	}

	public void setVague(uint Vague){
		
	}

	public void SetChrono(float time){
		ChronoTextMin.text = ((int)time / 60).ToString();
		ChronoTextSec.text = ((int)(time % 60.0f)).ToString();
	}

	public void askConstructionChoise(UnityAction gatlingConstructionEvent, UnityAction grenadeConstructionEvent, UnityAction missileConstructionEvent){

		Vector3 mousePosition = Input.mousePosition;

		Debug.Log (Screen.width);
		Debug.Log (mousePosition);



		mousePosition.x = (mousePosition.x / Screen.width) * (Screen.width);
		mousePosition.y = (mousePosition.y / Screen.height) * (Screen.height);

		Debug.Log (mousePosition);

		cp.setPosition (mousePosition);
		cp.Choice(gatlingConstructionEvent, grenadeConstructionEvent, missileConstructionEvent);
	}
}
