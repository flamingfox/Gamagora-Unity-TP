using UnityEngine;
using System.Collections;

public class MainMenuUI : MonoBehaviour {

	public void newGame(){
		Debug.Log ("newGame");
		Application.LoadLevel ("game");
	}

	public void exit(){
		Debug.Log ("Exit");
		Application.Quit ();
	}
}
