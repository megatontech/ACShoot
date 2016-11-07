using UnityEngine;
using System.Collections;

public class InstadieOnCollide : MonoBehaviour {
	public ParticleSystem blood;

	void OnCollisionEnter2D(Collision2D coll) {
		if(coll.collider.name != "Ground") {
			GetComponent<Animator>().SetInteger ("HP", 0);

			blood = Instantiate(blood) as ParticleSystem;
			blood.transform.position = transform.position;
			blood.Play();

			GetComponent<Collider2D>().enabled = false;
			GetComponent<Rigidbody2D>().Sleep ();
			
			enabled = false;
		}

	}
}
