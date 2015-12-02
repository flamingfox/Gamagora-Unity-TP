using UnityEngine;
using System.Collections;

public class Turret_Gatling : Turret {

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

		RaycastHit hit;
		Physics.Raycast (getGunPosition (), getGunDirection (), out hit, range);

		if (hit.collider != null && hit.collider.tag == "enemy") {
			Enemy enemyStrike = hit.collider.GetComponent<Enemy>();
			enemyStrike.hit(damage);
		}
	}
}
