using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

public class ConstructionPanel : Singleton<ConstructionPanel> {
	public Button gatling;
	public Button grenade;
	public Button missile;
	public GameObject thisPanel;
	public RectTransform panelTransform;

	public void setPosition(Vector3 position){
		//thisPanel.transform.position = position;
		panelTransform.position = position; // Vector2(position.x, position.y);
	}

	/*
	public getDimension(){
		return thisPanel.transform.
	}
	*/


	public void Choice (UnityAction gatlingConstructionEvent, UnityAction grenadeConstructionEvent, UnityAction missileConstructionEvent){
		thisPanel.SetActive (true);

		gatling.onClick.RemoveAllListeners ();
		gatling.onClick.AddListener (gatlingConstructionEvent);
		gatling.onClick.AddListener (ClosePanel);

		grenade.onClick.RemoveAllListeners ();
		grenade.onClick.AddListener (grenadeConstructionEvent);
		grenade.onClick.AddListener (ClosePanel);

		missile.onClick.RemoveAllListeners ();
		missile.onClick.AddListener (missileConstructionEvent);
		missile.onClick.AddListener (ClosePanel);

	}

	void ClosePanel(){
		thisPanel.SetActive (false);
	}
}
