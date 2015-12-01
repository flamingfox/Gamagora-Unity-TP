using UnityEngine;
using System.Collections;

public class DelayProjectille : Projectille {

	[Range(1,10)]
	public float timeBeforeExplosion = 2f;
	public float timer;

	private bool gizmos = false;

	[Range(1,10)]
	public float AOE = 2f;


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
		RaycastHit[] hits = Physics.SphereCastAll (transform.position, AOE, Vector3.right);

		foreach(RaycastHit hit in hits){
			if(hit.collider.tag == "enemy"){
				hit.collider.GetComponent<Enemy>().hit(damage);
			}
		}
	}

	protected override void OnTriggerEnter (Collider collision)	{}
}
