using UnityEngine;
using System.Collections;

public class DelayProjectille : Projectille {

	[Range(1,10)]
	public float timeBeforeExplosion = 2f;
	public float timer;

	private bool gizmos = false;

	[Range(1f,10f)]
	public float AOE = 2f;

	[Range(0.5f,10f)]
	public float explosionForce = 2f;


	public void OnDrawGizmos(){
		if(gizmos)
			Gizmos.DrawWireSphere (transform.position, AOE);
	}

	protected override void OnEnable ()
	{
		gizmos = false;
		base.OnEnable ();
		timer = timeBeforeExplosion;
	}

	protected override void Update ()
	{
//		base.Update ();

		if (!dead) {
			timer -= Time.deltaTime;

			if(timer <= 0){
				impact();
			}
		}
	}

	protected override void impact ()
	{
		base.impact ();

		gizmos = true;
		RaycastHit[] hits = Physics.SphereCastAll (transform.position, AOE, Vector3.right, AOE);

		foreach(RaycastHit hit in hits){
			if(hit.collider.tag == "enemy"){
				if(hit.collider.GetComponent<Enemy>().hit(damage)){
					hit.collider.GetComponent<Rigidbody>().velocity =
						((hit.transform.position-transform.position).normalized+(Vector3.up*0.2f)) *explosionForce;
				}
			}
		}
	}
}
