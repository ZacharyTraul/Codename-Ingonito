using UnityEngine;
using System.Collections;

[System.Serializable]
public class PlayerScript : MonoBehaviour {

	[Tooltip("The player model")]
	public GameObject player_model;
	[Tooltip("Where to instantiate the player at the beginning of a level.")]
	public Vector3 start_position;
	[Tooltip("How fast the player moves left and right, pretty self-explanatory.")]
	public float speed;
	[Tooltip("How high the player jumps. This is the y in an AddForce Vector3.")]
	public float jump_power;

	public GameObject feet_model;

	private static GameObject player;
	private Rigidbody rb;

	private static bool hidden = false;
	private GameObject feet;
	private static bool jump_allowed = false;

	// Use this for initialization
	void Start () {
		//Just instantiates the player and gets a rigidbody.
		player = Instantiate (player_model, start_position, Quaternion.identity) as GameObject;
		feet = Instantiate (feet_model, start_position + new Vector3 (0, -1, 0), Quaternion.identity) as GameObject;
		rb = player.GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		//If the player is not hidden, move them in the desired direction.
		if (Input.GetKey (KeyCode.RightArrow) && !hidden) {
			Vector3 new_velocity = rb.velocity;
			new_velocity.z = speed;
			rb.velocity = new_velocity;
			feet.transform.position = player.transform.position + new Vector3 (0, -1.1f, 0);

		}

		if (Input.GetKey (KeyCode.LeftArrow) && !hidden) {
			Vector3 new_velocity = rb.velocity;
			new_velocity.z = speed * -1;
			rb.velocity = new_velocity;		
			feet.transform.position = player.transform.position + new Vector3 (0, -1.1f, 0);

		}

		if (Input.GetKey (KeyCode.UpArrow) && !hidden) {
			//Test if the player is standing on something, if so, jump.
			if(jump_allowed){
				rb.AddForce (new Vector3 (0, jump_power, 0), ForceMode.VelocityChange);
			}
		}
		//Allows the player to hide and unhide.
		if (Input.GetKeyDown (KeyCode.S)) {
			hidden = true;
		}

		if (Input.GetKeyUp (KeyCode.S)) {
			hidden = false;
		}
		//Allows the player to interact, not done with this yet.
		if (Input.GetKey (KeyCode.E)) {
			Debug.Log ("Interaction");
		}

	}
		
	void FixedUpdate(){
		feet.transform.position = player.transform.position + new Vector3 (0, -1.1f, 0);


		//Makes it so we don't actually run into things. This fixes the wall sticking problem.
		Vector3 velocity = rb.velocity;
		velocity.y = 0;

		float distance = velocity.magnitude * Time.fixedDeltaTime;
		velocity.Normalize();

		RaycastHit hit;

		if (rb.SweepTest (velocity, out hit, distance)) {
			rb.velocity = new Vector3 (0, rb.velocity.y, 0);
		}

	}

	public static Vector3 PlayerLocation(){
		return player.transform.position;
	}

	public static void Jumper(bool jump){
		jump_allowed = jump;
	}

}
