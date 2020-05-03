using System.Collections;
using UnityEngine;

public class Turret : MonoBehaviour
{
    Transform target;

	[Header("General")]

	public float range = 15f;

	// TODO: Create Unity custom editor script that allows user to select turret type enum (projectile/laser/etc) and shows fields depending on type

	[Header("Use Bullets (default)")]

	public float fireRate = 1f;
	float fireCountdown = 0f;

	[Header("Use Laser")]

	public bool useLaser = false;
	public LineRenderer lineRenderer;

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

	// TODO: Refactor so that useLaser isn't such a one-off case (see all the calls to 'useLaser'. 
	// Ideally I think we'd have a different behavior depending on the type of turret. We could have 
	// a type for instance that is a wave turret (not projectile or laser) and may want to define a different
	// behavior. This to me screams ITurret interface where some things are required for definition (damage rate)
	// and others are left up to the implementation.

    // Update is called once per frame
    void Update()
	{
		if (target == null)
		{
			if (useLaser)
			{
				if (lineRenderer.enabled)
				{
					lineRenderer.enabled = false;
				}
			}

			return;
		}

		LockOnTarget();

		if (useLaser)
		{
			ShootLaser();
		}
		else
		{
			if (fireCountdown <= 0f)
			{
				ShootProjectile();
				fireCountdown = 1f / fireRate;
			}

			fireCountdown -= Time.deltaTime;
		}
	}

	private void LockOnTarget()
	{
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
		partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
	}

	void ShootProjectile()
	{
		var bulletGO = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
		Bullet bullet = bulletGO.GetComponent<Bullet>();

		if (bullet != null)
		{
			bullet.Seek(target);
		}
	}

	void ShootLaser()
	{
		if (!lineRenderer.enabled)
		{
			lineRenderer.enabled = true;
		}

		lineRenderer.SetPosition(0, firePoint.position);
		lineRenderer.SetPosition(1, target.position);
	}

    void OnDrawGizmosSelected()
    {
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, range);
    }

	// TODO: Could I draw a debug on the aquired target?
}
