using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	// Update is called once per frame
	void Update () {
		this.transform.position = new Vector3 (40f, 4.25f + PlayerScript.PlayerPosition().y, PlayerScript.PlayerPosition().z + 5f);
	}
}
