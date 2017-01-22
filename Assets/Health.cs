using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour {
	public float maxHealth;
	public float health;
	public GameObject explosion;
	public bool restartGameOnDestroy = false;

	public void takeDamage(float amount)
	{
		health -= amount;
		if (health <= 0)
		{
			if (restartGameOnDestroy)
			{
				SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
			}
			if (explosion)
			{
				Instantiate(explosion, transform.position, transform.rotation);
			}
			Destroy(gameObject);
		}
	}
}
