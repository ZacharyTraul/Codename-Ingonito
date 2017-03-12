using UnityEngine;
using System.Collections;

public class Jumper : MonoBehaviour {
	void OnTriggerEnter(Collider other){
		//Debug.Log ("Sup");
		PlayerScript.Jumper (true);
		PlayerScript.JumpSet (0, true);
	}

	void OnTriggerExit(Collider other){
		//Debug.Log ("Sup");
		PlayerScript.Jumper (false);
		PlayerScript.JumpSet (0, false);
	}
}
