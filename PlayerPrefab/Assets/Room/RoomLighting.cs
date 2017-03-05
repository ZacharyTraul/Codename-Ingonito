using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoomLighting : MonoBehaviour {

	public Room[] rooms;

	List<Bounds> bounds = new List<Bounds> ();

	private GameObject[] objects;

	void Start(){
		for (int i = 0; i < rooms.Length; i++) {
			bounds.Add (new Bounds (Vector3.zero, Vector3.one));
			bounds [i].SetMinMax (rooms [i].room_min, rooms [i].room_max);
		}

		objects = GetGameObjects ();

	}

	// Update is called once per frame
	void Update () {
		Bounds current = new Bounds(Vector3.zero, Vector3.one);
		bool inside = false;
		for (int i = 0; i < bounds.Count; i++) {
			if (bounds [i].Contains (PlayerScript.PlayerLocation ())) {
				current = bounds [i];
				inside = true;
				break;
			}
		}
			
		if (inside) {
			for (int i = 0; i < objects.Length; i++) {
				if (current.Contains (objects [i].transform.position)) {
					objects [i].SetActive (true);
				} else {
					objects [i].SetActive (false);
					Debug.Log ("yo");
				}
			}
		}
	}

	GameObject[] GetGameObjects(){
		Object[] all_things = Resources.FindObjectsOfTypeAll (typeof(GameObject));
		List<GameObject> game_objects = new List<GameObject> ();

		foreach (Object obj in all_things) {
			if (obj is GameObject) {
				game_objects.Add ((GameObject)obj);
			}
		}

		return game_objects.ToArray();

	}

}
