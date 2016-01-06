using UnityEngine;
using System.Collections;

public class Ship : MonoBehaviour
{
	public GameObject mesh;
	public Animator haloAnimator;
	public ParticleSystem effect;

	public void spawn ()
	{
		effect.Play ();
		haloAnimator.SetTrigger("active");
		Invoke ("shipAppared", 1.0f);
	}

	private void shipAppared(){
		mesh.SetActive (true);
	}
}

