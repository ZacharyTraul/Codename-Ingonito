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

	private Vector3 shoes = new Vector3 (0, -1.1f, 0); //creative variable naming
	private Vector3 jump_vector = Vector3.zero;
	private float face = 0;
	private Vector3 new_velocity = Vector3.zero;
	private static bool jump_allowed = false;
	private static int jump_state = 0;
	public int max_jumps = 2;
	private static int jump_timer_reset = 7;
	private static int jump_timer = jump_timer_reset;

	private static int visibility;

	private Vector3 shoes = new Vector3 (0, -1.1f, 0); //creative variable naming
	private Vector3 jump_vector = Vector3.zero;
	private float face = 0;
	private Vector3 new_velocity = Vector3.zero;

	private static int jump_state = 0;
	private int max_jumps = 2;
	private static int jump_timer_reset = 7;
	private static int jump_timer = jump_timer_reset;

	// Use this for initialization
	void Start () {
		//Just instantiates the player and gets a rigidbody.
		player = Instantiate (player_model, start_position, Quaternion.identity) as GameObject;
		feet = Instantiate (feet_model, start_position + new Vector3 (0, -1, 0), Quaternion.identity) as GameObject;
		rb = player.GetComponent<Rigidbody> ();
		jump_vector = new Vector3 (0, jump_power, 0);
	}
	
	// Update is called once per frame
	void Update () {
		//Get the block behind our head in order to determine how visible we are. Pretty neat.
		RaycastHit hit;
		if (Physics.Linecast (player.transform.position, player.transform.position - Vector3.right, out hit)) {
			//Basically just checks for the how bright it is in the name of the block.
			if (hit.collider.name.Contains ("Dark")) {
				visibility = 1;
			} else if (hit.collider.name.Contains ("Gray")) {
				visibility = 2;
			} else if (hit.collider.name.Contains ("Light")) {
				visibility = 3;
			}

			//Debug.Log (visibility);
		}

		//If the player is not hidden, move them in the desired direction.
		if (!hidden) { //watch as i do some crazy math
			face = Input.GetAxis ("Horizontal"); //horizontal is a built in unity axis that is -1 when either A or left arrow, and is 1 when either D or right arrow.
			//project settings -> edit -> input
			//if you don't have this update unity LOL

			if (face != 0) {
				new_velocity = rb.velocity;
				new_velocity.z = speed * face; //multiplying it by face. isn't that just so clever?? i feel smart.
				rb.velocity = new_velocity;
			}

			//i rewrote jumping a little bit so I'll give you the rundown

			if (jump_state != max_jumps) {
				//jump_state is a variable that refers to the current jump that you aer doing.
				//max_jumps is a changeable variable for if you want single-jump or triple-jump or whatever. i like double-jumping so that's why i did this.
				if (Input.GetKeyDown (KeyCode.UpArrow) || Input.GetKeyDown ("w")) {
					//when we jump, we reset vertical momentum because it just kinda feels better
					new_velocity = rb.velocity;
					new_velocity.y = 0;
					rb.velocity = new_velocity;
				}
				if (Input.GetKey (KeyCode.UpArrow) || Input.GetKey ("w")) {
					if (jump_timer != 0) {
						//we have a jump timer now that adds force to the player over time, and it decrements over time as well.
						//as long as jump is held AND the jump_timer is stil gucci, force will be applied.
						rb.AddForce (jump_vector, ForceMode.VelocityChange);
						jump_timer--;
						//Debug.Log ("Jump " + jump_state);
						//Debug.Log ("Timer: " + jump_timer);
					}
				}
				if (Input.GetKeyUp (KeyCode.UpArrow) || Input.GetKeyUp ("w")) {
					//once the key is released, we increment the jump_state and reset the jump_timer.
					jump_state++; //increment the current jump
					jump_timer = jump_timer_reset;
				}
			}
		}
		//Allows the player to hide and unhide.
		if (Input.GetKeyDown ("s") || Input.GetKeyUp ("s")) {
			hidden = !hidden; //makes the variable 'hidden' exactly the opposite of what it was. you learn something new every day
		}
		//Allows the player to interact, not done with this yet.
		if (Input.GetKey ("e")) {
			Debug.Log ("Interaction");
			//to-do: gumball machines
		}

	}
		
	void FixedUpdate(){
		feet.transform.position = player.transform.position + shoes; //put some shoes on


		//Makes it so we don't actually run into things. This fixes the wall sticking problem.
		//[IT'S CALLED A HACK]
		Vector3 velocity = rb.velocity;
		velocity.y = 0;

		float distance = velocity.magnitude * Time.fixedDeltaTime;
		velocity.Normalize();

		RaycastHit hit;
			
		if (rb.SweepTest (velocity, out hit, distance)) {
			rb.velocity = new Vector3 (0, rb.velocity.y, 0); //i was really hoping to make this line less bad but it kept erroring so here we are
		}

	}

	public static Vector3 PlayerLocation(){
		return player.transform.position;
	}

	public static int PlayerVisibility(){
		return visibility;
	}

	public static void Jumper(bool jump){
		jump_allowed = jump;
	}

	public static void JumpSet(int setState, bool timer){
		jump_state = setState;
		if (timer) {
			jump_timer = jump_timer_reset;
		}
	}
}
