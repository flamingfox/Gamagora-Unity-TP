using UnityEngine;
using System.Collections;

public class ParticuleManager : MonoBehaviour {

	public IKillable parent;
	public ParticleSystem effect;
	public bool isRunning = false;

	private float lifeTime;

	// Use this for initialization
	void Start () {
		init ();
	}
	
	// Update is called once per frame
	void Update () {
		if (isRunning) {
			lifeTime -= Time.deltaTime;

			if (lifeTime <= 0){
				isRunning = false;
				parent.kill ();
			}
		}
	}

	public void init(){
		lifeTime = effect.startLifetime;
		isRunning = false;
	}

	public void run(){
		isRunning = true;
		effect.Play ();
	}
}
