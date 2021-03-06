using UnityEngine;
using System.Collections;

public class Turret_Missile : Turret {

	public PoolingManager missileManager;

	protected override void aim ()
	{
		Vector3 directionTarget = target.transform.position - support.transform.position;
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

		GameObject missile = missileManager.getFirstObjectAvailable ();
		GuidedProjectille projectille = missile.GetComponent<GuidedProjectille> ();

		projectille.transform.position = gun.transform.position;
		projectille.setGunner (tower);
		projectille.damage = damage;
		projectille.target = target;
		projectille.lifeDistance = range;
		projectille.setDirection (gun.transform.up);
	}

}
