using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {
	public float maxHealth;
	public float health;

	public void takeDamage(float amount)
	{
		health -= amount;
		if (health <= 0)
		{
			Destroy(gameObject);
		}
	}
}
