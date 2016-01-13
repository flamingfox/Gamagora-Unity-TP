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

	// Use this for initialization
	void Start () {
		game = GameState.Instance;
		setPV (game.PlayerPV);
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
		cp.Choice(gatlingConstructionEvent, grenadeConstructionEvent, missileConstructionEvent);
	}
}
