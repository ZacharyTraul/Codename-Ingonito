using UnityEngine;
using System.Collections;

public class LaserScript : MonoBehaviour {

	public GameObject emmiter_prefab;
	public GameObject receiver_prefab;

	public LaserSystem[] lasers;

	private GameObject[] lines;

	// Use this for initialization
	void Start () {
	
		for (int i = 0; i < lasers.Length; i++) {
			LaserSystem laser = lasers [i];

			Instantiate (emmiter_prefab, laser.emitter, Quaternion.identity);
			Instantiate (receiver_prefab, laser.receiver, Quaternion.identity);
		
			DrawLine (laser.emitter, laser.receiver, laser.width, laser.mat, i);
		}

	}
	
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < lasers.Length; i++) {
			LaserSystem laser = lasers [i];

			RaycastHit hit;

			if (Physics.Linecast (laser.emitter, laser.receiver, out hit)) {
				if (hit.collider.CompareTag ("Player")) {
					Debug.Break ();
				}
			}
		}
	}

	void DrawLine(Vector3 start, Vector3 end, float width, Material mat, int index){
		GameObject line = new GameObject ();
		line.tag = "Laser";
		line.transform.position = start;

		line.AddComponent<LineRenderer> ();

		LineRenderer lr = line.GetComponent<LineRenderer> ();
		lr.material = mat;
		lr.SetWidth (width, width);
		lr.SetPosition (0, start);
		lr.SetPosition (1, end);
	}
}
