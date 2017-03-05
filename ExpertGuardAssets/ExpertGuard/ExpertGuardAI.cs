using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ExpertGuardAI : MonoBehaviour {

	[Tooltip("The model of the guard.")]
	public GameObject guardprefab;

	[Tooltip("An array to hold the guard data.")]
	public Guard[] guard;

	private List<GameObject> guards = new List<GameObject>();
	private List<Rigidbody> rbs = new List<Rigidbody>();
	private List<int> directions = new List<int>();
	private List<bool> actives = new List<bool> ();
	private List<bool> jumping = new List<bool>();

	void Start () {
		for(int i = 0; i < guard.Length; i ++) {
			GameObject new_guard = Instantiate (guardprefab, guard[i].start_positon, Quaternion.identity) as GameObject;
			guards.Add(new_guard);
			rbs.Add (new_guard.GetComponent<Rigidbody> ());
			directions.Add (1);
			actives.Add (false);
			jumping.Add (false);
		}
	}

	void Update(){
		for (int i = 0; i < guards.Count; i++) {
			float distance_to_player = Vector3.Distance (guards[i].transform.position, PlayerScript.PlayerPosition ());

			if (distance_to_player < 5) {
				actives[i] = true;
			}

			RaycastHit hit;

			Vector3 guard_head = guards[i].transform.position;
			guard_head.y += 0.5f;
			float difference = guards[i].transform.position.z - PlayerScript.PlayerPosition ().z;

			if (Physics.Linecast (guard_head, PlayerScript.PlayerPosition (), out hit)) {
				if ((directions[i] < 0 && difference > 0) || (directions[i] > 0 && difference < 0)) {
					if (hit.collider.transform.position == PlayerScript.PlayerPosition ()) {
						actives[i] = true;
					}
				}
			}
		}
	}

	void FixedUpdate () {
		for (int i = 0; i < guards.Count; i++) {
			Vector3 new_velocity = rbs [i].velocity;
			float distance_to_player = Vector3.Distance (guards [i].transform.position, PlayerScript.PlayerPosition ());

			if (distance_to_player < 5 && !jumping [i] && actives [i]) {
				Jump (i);
				jumping [i] = true;
			}

			Vector3 fwd = transform.TransformDirection (Vector3.forward);
			Vector3 bwd = transform.TransformDirection (Vector3.back);
			Vector3 dwn = transform.TransformDirection (Vector3.down);
			RaycastHit hit_fwd;
			RaycastHit hit_bwd;

			if (actives [i]) {
				new_velocity.z = guard[i].speed * -1 * Mathf.Sign (guards[i].transform.position.z - PlayerScript.PlayerPosition ().z);
				if (Physics.Raycast (guards[i].transform.position, fwd, out hit_fwd, 1) && !jumping[i]) {
					if (!hit_fwd.collider.CompareTag ("Enemy")) {
						Jump (i);
						jumping [i] = true;
					}
				}
				if(Physics.Raycast (guards[i].transform.position, bwd, out hit_bwd, 1) && !jumping[i]){
					if(!hit_bwd.collider.CompareTag("Enemy")){
						Jump (i);
						jumping[i] = true;
					}
				}
				if (Physics.Raycast (guards[i].transform.position, dwn, 1)) {
					AllowJumping(i);
				}

				if (Physics.Raycast (guards [i].transform.position, fwd, out hit_fwd, 1.5f)) {
					if(hit_fwd.collider.gameObject.CompareTag("Door")){
						hit_fwd.collider.gameObject.SetActive (false);
					}
				}

				if (Physics.Raycast (guards [i].transform.position, bwd, out hit_bwd, 1.5f)) {
					if(hit_bwd.collider.gameObject.CompareTag("Door")){
						hit_bwd.collider.gameObject.SetActive (false);
					}
				}
			} else {
				if (Physics.Raycast (guards[i].transform.position, fwd, 1) || Physics.Raycast (guards[i].transform.position, bwd, 1)) {
					ChangeDirection (i);
				}
				new_velocity.z = guard[i].speed * directions[i]; 
			}

			rbs[i].velocity = new_velocity;

		}
	}

	void ChangeDirection(int i){
		directions[i] *= -1;
	}

	void Jump(int i){
		rbs[i].AddForce (new Vector3 (0, guard[i].jump_height, 0), ForceMode.VelocityChange);
	}

	void AllowJumping(int i){
		jumping[i] = false;
	}
}
