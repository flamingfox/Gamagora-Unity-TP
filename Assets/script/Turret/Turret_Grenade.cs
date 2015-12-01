using UnityEngine;
using System.Collections;

public class Turret_Grenade : Turret {

	public PollingManager grenadeManager;

	[Range(10, 100)]
	public float impulsionForce = 50f;


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
		
		GameObject grenade = grenadeManager.getFirstAvailable ();
		DelayProjectille projectille = grenade.GetComponent<DelayProjectille> ();
		
		projectille.transform.position = gun.transform.position + gun.transform.up*0.4f;
		projectille.setGunner (tower);
		projectille.damage = damage;
		projectille.lifeDistance = range;
		projectille.GetComponent<Rigidbody> ().velocity = gun.transform.up * impulsionForce;
	}
}
