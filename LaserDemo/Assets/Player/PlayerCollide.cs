using UnityEngine;
using System.Collections;

public class PlayerCollide : MonoBehaviour {
	void OnCollisionEnter(Collision c){
		if (c.gameObject.CompareTag ("Laser")) {
			Destroy (c.gameObject);
		} else {
			PlayerScript.ChangeJump ();
		}
	}
}
