using UnityEngine;

[System.Serializable]
public class Wave
{
	// TODO: A wave should actually be a composition of these enemy spawns
	// TODO: How would we handle multiple possible paths?

	public GameObject enemy;
	public int count;
	public float rate;
}
