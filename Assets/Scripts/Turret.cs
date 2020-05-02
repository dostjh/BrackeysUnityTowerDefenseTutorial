using System.Collections;
using UnityEngine;

public class Turret : MonoBehaviour
{
    Transform target;

	[Header("Attributes")]

	public float range = 15f;
	public float fireRate = 1f;
	float fireCountdown = 0f;

	[Header("Unity Setup Fields")]

	public string enemyTag = "Enemy";

	public Transform partToRotate;
	public float turnSpeed = 10f;

	public GameObject bulletPrefab;
	public Transform firePoint;
    
    // Start is called before the first frame update
    void Start()
    {
		InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

	// Don't have the target update every frame. Computationally expensive. Instead, invokerepeating to happen 2x per second.
	void UpdateTarget()
	{
		GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
		var shortestDistance = Mathf.Infinity;
		GameObject nearestEnemy = null;

		foreach (var enemy in enemies)
		{
			var distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

			// Update shortest distance (for tracking) and nearestEnemy (for targeting)
			if (distanceToEnemy < shortestDistance)
			{
				shortestDistance = distanceToEnemy;
				nearestEnemy = enemy;
			}

			if (nearestEnemy != null && shortestDistance <= range)
			{
				target = nearestEnemy.transform;
				//Debug.Log($"Turret {name} targeting {target.name}");
			}
			else
			{
				target = null;
			}
		}
	}

    // Update is called once per frame
    void Update()
    {
        if (target == null)
		{
			return;
		}

		// Target Lock On
		// Only rotate around the Y axis
		/* NOTE: This was ofset by 90 degrees on Y-axis when targeting, so we went into the 
		 * PartToRotate object on the Turret, removed the head, offset the PartToRotate by -90
		 * degress on the Y-axis rotation transform, then readded the head. This was all done on
		 * the prefab.
		 */
		// NOTE: Lurp: Unity Smooth rotation from one position to another.
		Vector3 direction = target.position - transform.position;
		Quaternion lookRotation = Quaternion.LookRotation(direction);
		Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
		partToRotate.rotation = Quaternion.Euler (0f, rotation.y, 0f);

		if (fireCountdown <= 0f)
		{
			Shoot();
			fireCountdown = 1f / fireRate;
		}

		fireCountdown -= Time.deltaTime;
    }

	void Shoot()
	{
		var bulletGO = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
		Bullet bullet = bulletGO.GetComponent<Bullet>();

		if (bullet != null)
		{
			bullet.Seek(target);
		}
	}

    void OnDrawGizmosSelected()
    {
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, range);
    }

	// TODO: Could I draw a debug on the aquired target?
}
