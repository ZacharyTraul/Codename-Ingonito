using UnityEngine;
using System.Collections;

public class PlayerCollide : MonoBehaviour {
	void OnCollisionEnter(Collision c){
		if (c.gameObject.CompareTag ("Enemy")) {
			Debug.Break();
		} else {
			PlayerScript.ChangeJump ();
		}
	}

}
