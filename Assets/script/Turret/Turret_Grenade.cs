using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Turret_Grenade : Turret {

	public PoolingManager grenadeManager;

	[Range(10, 100)]
	public float impulsionForce = 50f;


	protected override Enemy selectTarget ()
	{
		Enemy retour = null;
		
		float distance = range;
		List<GameObject> listEnemy = poolingEnemy.getListActive ();
		foreach (GameObject gO in listEnemy) {			
			if (!gO.GetComponent<Enemy> ().isDead ()) {

				Vector3 directionTarget = gO.transform.position+gO.transform.forward*3;

				//float dis = Vector3.Distance (directionTarget, this.transform.position);

				if (Vector3.Distance (directionTarget, this.transform.position) <= distance) {
					retour = gO.GetComponent<Enemy> ();
					distance = Vector3.Distance (gO.transform.position, this.transform.position);
				}
			}
		}
		
		return retour;
	}

	protected override void aim ()
	{

		//Vector3 directionTarget = target.transform.position - support.transform.position;
		//directionTarget = directionTarget.normalized;

		Vector3 directionTarget = target.transform.position+target.transform.forward*3 - support.transform.position;
		directionTarget = directionTarget.normalized;

		Debug.DrawLine (support.transform.position, support.transform.position + directionTarget * 2f, Color.cyan);
		
		float stepRotation = speedRotate * Time.deltaTime;
		
		Vector3 newDir = Vector3.RotateTowards (support.transform.forward,
		                                        directionTarget,
		                                        stepRotation,
		                                        0f);
		
		support.transform.rotation = Quaternion.LookRotation (newDir);
	}
	
	override protected void fire(){
		
		GameObject grenade = grenadeManager.getFirstObjectAvailable ();
		DelayProjectille projectille = grenade.GetComponent<DelayProjectille> ();
		
		projectille.transform.position = gun.transform.position + gun.transform.up*0.4f;
		projectille.setGunner (tower);
		projectille.damage = damage;
		projectille.lifeDistance = range;
		projectille.GetComponent<Rigidbody> ().velocity = gun.transform.up * impulsionForce;
	}
}
