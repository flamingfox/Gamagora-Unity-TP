using UnityEngine;
using System.Collections;

public class Turret_Gatling : Turret {

	override protected void fire(Projectille projectille){
		projectille.transform.position = gun.transform.position;
		projectille.setGunner (tower);
		projectille.damage = damage;
		projectille.target = target;
		projectille.lifeDistance = range;
		projectille.setDirection (support.transform.forward);
		projectille.pollingEnemy = pollingEnemy;
	}
}
