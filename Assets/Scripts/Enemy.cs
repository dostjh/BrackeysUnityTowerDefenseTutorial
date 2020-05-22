using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
	// NOTE: He's using startSpeed and speed as separate concepts to make sure that 
	// we don't continually decrease speed gradually going to zero. Avoids stacking problem.
	// However, I think a cleaner approach might be to have a private variable IsSlowed. Wonder
	// if that would be as performanent though...
	public float startSpeed = 10f;
	public float startHealth = 100f;

	[HideInInspector] // TODO: Using this to prevent editing. However, I would want to make it so that I can still inspect. See https://answers.unity.com/questions/489942/how-to-make-a-readonly-property-in-inspector.html
	public float speed;
	[HideInInspector]
	public float health;

	public int worth = 50;
	public GameObject deathEffect;

	[Header("Unity Required Refereces")]
	public Image healthBar;

	private void Start()
	{
		speed = startSpeed;
		health = startHealth;
	}

	void Die()
	{
		PlayerStats.Money += worth;
		WaveSpawner.EnemiesAlive--;

		var effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
		Destroy(effect, 5f);
		Destroy(gameObject);
	}

	public void TakeDamage(float amount)
	{
		health -= amount;
		healthBar.fillAmount = health / startHealth;

		if (health <= 0)
		{
			Die();
		}
	}

	public void TakeSlow(float amount)
	{
		speed = startSpeed * (1f - amount);
	}
}
