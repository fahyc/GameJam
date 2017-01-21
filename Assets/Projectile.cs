using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
	public float damage;
	public float speed;

	Rigidbody r;
	// Use this for initialization
	void Start () {
		r = GetComponent<Rigidbody>();
		r.velocity = speed * transform.forward;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision col)
	{
		print(col.gameObject);
		Health h = col.gameObject.GetComponent<Health>();
		if (h)
		{
			h.takeDamage(damage);
		}
		Destroy(gameObject);
	}
}
