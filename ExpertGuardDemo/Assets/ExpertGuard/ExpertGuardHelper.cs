using UnityEngine;

[System.Serializable]
public class Guard {
	[Tooltip("The guard's move speed")]
	public float speed;

	[Tooltip("Not really the height, but more the power.")]
	public float jump_height;

	[Tooltip("Where the guard is initially.")]
	public Vector3 start_positon;
}
