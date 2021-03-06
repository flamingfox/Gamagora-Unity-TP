﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Poolable : MonoBehaviour{

	PoolingManager poolParent;

	public void setPoolParent(PoolingManager parent){
		poolParent = parent;
	}

	protected void poolRelease(){
		poolParent.releaseObject(this.gameObject);
	}
}

public class PoolingManager : MonoBehaviour {

	public int initNbInstanciate = 5;
	public GameObject model;

	private List<GameObject> _available = new List<GameObject>();
	private List<GameObject> _inUse = new List<GameObject>();

	public bool newInstanciationAutorization = true;

	public void preLoad(){
		for(int i=0; i< initNbInstanciate; i++){
			GameObject clone = Instantiate (model, gameObject.transform.position, gameObject.transform.rotation) as GameObject;
			clone.transform.SetParent( gameObject.transform );
			clone.GetComponent<Poolable>().setPoolParent(this);
			clone.SetActive(false);
			_available.Add (clone);
		}
	}

	public GameObject getFirstObjectAvailable(){

		lock(_available)
		{
			if (_available.Count != 0)
			{
				GameObject instance = _available[0];
				instance.SetActive(true);
				_inUse.Add(instance);
				_available.RemoveAt(0);

				return instance;
			}
			else
			{
				if(_inUse.Count == 0){
					preLoad();

					return getFirstObjectAvailable();
				}
				else if(newInstanciationAutorization){
					GameObject instance = Instantiate (model, gameObject.transform.position, gameObject.transform.rotation) as GameObject;
					instance.GetComponent<Poolable>().setPoolParent(this);
					instance.transform.SetParent( gameObject.transform );
					_inUse.Add(instance);

					return instance;
				}
			}
		}

		return null;
	}

	public GameObject getObject(int id){

		if (_inUse.Count != 0) {
			GameObject instance = _inUse [id];
			return instance;
		}

		
		return null;
	}

	public void releaseObject(GameObject instance)
	{
		lock (_available)
		{
			instance.SetActive(false);
			_available.Add(instance);
			_inUse.Remove(instance);
		}
	}

	public List<GameObject> getListActive(){
		List<GameObject> retour = new List<GameObject>();

		foreach(GameObject instance in _inUse){
			if(instance.activeSelf)
				retour.Add(instance);
		}

		return retour;
	}
}