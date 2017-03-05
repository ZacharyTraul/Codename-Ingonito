using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	public GameObject player_prefab;
	public float speed;

	private static GameObject player;
	private Rigidbody rb;

	private static bool jumping = false;

	// Use this for initialization
	void Start () {
		player = Instantiate (player_prefab, new Vector3(0, 7, 24), Quaternion.identity) as GameObject;
		rb = player.GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.RightArrow)) {
			Vector3 new_velocity = rb.velocity;
			new_velocity.z = speed;
			rb.velocity = new_velocity;
		}
		if (Input.GetKey (KeyCode.LeftArrow)) {
			Vector3 new_velocity = rb.velocity;
			new_velocity.z = speed * -1;
			rb.velocity = new_velocity;		}
		if (Input.GetKey (KeyCode.UpArrow) && !jumping) {
			jumping = true;
			rb.AddForce (new Vector3 (0, 11, 0) ,ForceMode.VelocityChange);
		}
	}

	public static void ChangeJump(){
		jumping = false;
	}

	public static Vector3 PlayerPosition(){
		return player.transform.position;
	}

}
