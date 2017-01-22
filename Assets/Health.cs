using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {
	public float maxHealth;
	public float health;
	public GameObject explosion;

	public void takeDamage(float amount)
	{
		health -= amount;
		if (health <= 0)
		{
			if (explosion)
			{
				Instantiate(explosion, transform.position, transform.rotation);
			}
			Destroy(gameObject);
		}
	}
}
