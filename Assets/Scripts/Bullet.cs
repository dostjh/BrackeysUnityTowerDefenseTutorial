using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	// NOTE: Get the target information from the turret using the seek method.
	Transform target;

	public float speed = 70f;
	public int damage = 50;
	public float explosionRadius = 0f;
	public GameObject impactEffect;

	public void Seek(Transform _target)
	{
		target = _target;
	}
	
	// Update is called once per frame
    void Update()
    {
        if (target == null)
		{
			Destroy(gameObject);
			return;
		}

		Vector3 direction = target.position - transform.position;
		float distanceThisFrame = speed * Time.deltaTime;

		// If we hit the target, don't overshoot
		if (direction.magnitude <= distanceThisFrame)
		{
			HitTarget();
			return;
		}

		// Move bullet
		transform.Translate(direction.normalized * distanceThisFrame, Space.World);
		// Rotate bullet
		transform.LookAt(target);
    }

	private void HitTarget()
	{
		// Do this first so that we don't hit the object multiple times since it'll still exist if we only damage it.
		Destroy(gameObject);

		// TODO: It would be cool if the debris effect matched the color of the enemy. Enemies should specify their debris effect.
		var effectInstance = Instantiate(impactEffect, transform.position, transform.rotation);
		Destroy(effectInstance, 5.0f);

		if (explosionRadius > 0f)
		{
			Explode();
		}
		else
		{
			Damage(target);
		}
	}

	/// <summary>
	/// Damages all enemies in the explosion radius
	/// </summary>
	void Explode()
	{
		// Create a sphere to check for Enemy colliders in the radius of the sphere and damage them.
		var colliders = Physics.OverlapSphere(transform.position, explosionRadius);
		foreach (var collider in colliders)
		{
			if (collider.tag == "Enemy")
			{
				// TODO: Damage in proportion to radius, so that closer enemies receive more damage than further ones.
				Damage(collider.transform);
			}
		}
	}

	/// <summary>
	/// Damages a single enemy.
	/// </summary>
	/// <param name="enemy"></param>
	void Damage (Transform enemy)
	{
		var enemyComponent = enemy.GetComponent<Enemy>();

		// NOTE: Doing a check for null here because we could implement an Enemy that doesn't have an Enemy component. However, isn't that what an interface would account for? So if we had IEnemy, we could guarantee this better.
		if (enemyComponent != null)
		{
			enemyComponent.TakeDamage(damage);
		}
	}

	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, explosionRadius);
	}
}
