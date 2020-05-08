using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// NOTE: RequireComponent tells Unity that we need to have the component or this class won't work.
[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour
{
	Transform target;
	int wavepointIndex = 0;

	private Enemy enemy;

	void Start()
	{
		enemy = GetComponent<Enemy>();
		target = Waypoints.points[0];
	}

	// Script Execution Order: T+100
	void Update()
	{
		Vector3 dir = target.position - transform.position;
		transform.Translate(dir.normalized * enemy.speed * Time.deltaTime, Space.World);

		if (Vector3.Distance(transform.position, target.position) <= 0.2f)
		{
			GetNextWaypoint();
		}

		// NOTE: Using this to reset the speed when the target gets out of range of a slow effect.
		// This seems terribly unperformant though. Each is resetting itself every time. 
		// Seems better to have an IsSlowed variable to read off of and maybe a target aquired/target unacquired
		// method to update (sent from the turret).
		// TODO: Find a better way to control slow state.
		enemy.speed = enemy.startSpeed;
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
}
