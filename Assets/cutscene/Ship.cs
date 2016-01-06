using UnityEngine;
using System.Collections;

public class Ship : MonoBehaviour
{
	public GameObject mesh;
	public Animator haloAnimator;
	public ParticleSystem effect;
	public Animator shipAnimation;

	public void spawn ()
	{
		effect.Play ();
		shipAnimation.enabled = true;
		haloAnimator.SetTrigger("active");
		Invoke ("shipAppared", 1.0f);
	}

	private void shipAppared(){
		mesh.SetActive (true);
	}

	public void takeOff ()
	{
		shipAnimation.SetTrigger ("takeOff");
	}
}

