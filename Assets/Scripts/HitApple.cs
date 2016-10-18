using UnityEngine;
using System.Collections;

public class HitApple : MonoBehaviour 
{
	public AudioClip appleHittenSound;

	void OnCollisionEnter2D(Collision2D coll)
	{
		Main.scoreManager.appleHitten();
		Main.appleAnimation.appleHitten();
		Main.incrementAppleHitten();
		GetComponent<AudioSource>().PlayOneShot(appleHittenSound);
	}
}
