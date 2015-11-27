using UnityEngine;
using System.Collections;

public class PollingManager : MonoBehaviour {

	public GameObject model;

	private ArrayList pooling = new ArrayList();

	public GameObject getFirstAvailable(){

		foreach(GameObject instance in pooling){
			if(!instance.activeSelf){
				instance.SetActive(true);
				return instance;
			}
		}
		GameObject clone = Instantiate (model, gameObject.transform.position, gameObject.transform.rotation) as GameObject;
		pooling.Add (clone);

		return clone;
	}

	public ArrayList getListActive(){
		ArrayList retour = new ArrayList ();

		foreach(GameObject instance in pooling){
			if(instance.activeSelf)
				retour.Add(instance);
		}

		return retour;
	}
}
