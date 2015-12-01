using UnityEngine;
using System.Collections;

public class GuidedProjectille : Projectille {

	public Enemy target = null;
	public float rotationSpeed = 5f;
	public float speed = 2f;

	private Vector3 direction;

	
	// Update is called once per frame
	override protected void Update () {
	
		if (!dead) {
			
			if (Vector3.Distance (this.transform.position, gunner.transform.position) < lifeDistance) {
				
				this.transform.position += (direction * speed);
				
				if (target == null || target.isDead ()) {
					target = null;
				} else {
					
					float stepRotation = rotationSpeed * Time.deltaTime;
					
					Vector3 newDir = Vector3.RotateTowards (direction,
					                                        target.transform.position - this.transform.position,
					                                        stepRotation,
					                                        0f);
					
					this.setDirection (newDir);
					
				}
			} else {
				kill ();
			}
		}

	}

	
	
	public void setDirection (Vector3 _direction)
	{
		_direction.Normalize ();
		mesh.transform.LookAt (mesh.transform.position + _direction);
		mesh.transform.Rotate (90, 0, 0);
		
		//direction = _direction;
		
		direction = Vector3.Cross (mesh.transform.forward, mesh.transform.right);
	}
}
