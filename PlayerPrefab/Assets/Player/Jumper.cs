using UnityEngine;
using System.Collections;

public class Jumper : MonoBehaviour {
	void OnTriggerEnter(Collider other){
		Debug.Log ("Sup");
		PlayerScript.Jumper (true);
	}

	void OnTriggerExit(Collider other){
		Debug.Log ("Sup");
		PlayerScript.Jumper (false);
	}
}
