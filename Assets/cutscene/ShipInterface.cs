using UnityEngine;
using System.Collections;

public class ShipInterface : MonoBehaviour {

	public Ship ship;

	public void SpawnShip(){
		ship.spawn ();
	}

	public void TakeOffShip(){
		ship.takeOff ();
	}
}
