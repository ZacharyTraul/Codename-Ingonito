using UnityEngine;
using System.Collections;

public class Jumper : MonoBehaviour {
	void OnTriggerStay(Collider other){
		if (!other.CompareTag("Player")) {
			PlayerScript.Jumper (true);
		}
	}

	void OnTriggerExit(Collider other){
		//Debug.Log ("Sup");
		PlayerScript.Jumper (false);
	}
}
