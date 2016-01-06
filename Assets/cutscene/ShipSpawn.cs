using UnityEngine;
using System.Collections;

public class ShipSpawn : MonoBehaviour {

	public Ship ship;

	public void SpawnShip(){
		ship.spawn ();
	}
}
