using UnityEngine;

public class Enemy : MonoBehaviour
{
	public float speed = 10f;

	public int health = 100;
	public int reward = 50;
	public GameObject deathEffect;

	Transform target;
	int wavepointIndex = 0;

	void Start()
	{
		target = Waypoints.points[0];
	}

	void Update()
	{
		Vector3 dir = target.position - transform.position;
		transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

		if (Vector3.Distance(transform.position, target.position) <= 0.2f)
		{
			GetNextWaypoint();
		}
	}

	void GetNextWaypoint()
	{
		if (wavepointIndex >= Waypoints.points.Length - 1)
		{
			EndPath();
			return;
		}
		
		wavepointIndex++;
		target = Waypoints.points[wavepointIndex];
	}

	// TODO: Hate this method name. Change it.
	void EndPath()
	{
		PlayerStats.Lives--;
		Destroy(gameObject);
	}

	void Die()
	{
		PlayerStats.Money += reward;

		var effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
		Destroy(effect, 5f);
		Destroy(gameObject);
	}

	public void TakeDamage (int amount)
	{
		Debug.Log($"Health before hit: {health}");
		health -= amount;
		Debug.Log($"Health after hit: {health}");

		if (health <= 0)
		{
			Die();
		}
	}
}
