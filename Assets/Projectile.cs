using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
	public float damage;
	public float speed;

	public float blastRadius;
	public GameObject explosion;

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
		//		print(col.gameObject);
		if (explosion)
		{
			explosion = Instantiate(explosion, transform.position, transform.rotation);
		}
		if(blastRadius > 0)
		{
			Collider[] blastCol = Physics.OverlapSphere(transform.position, blastRadius);
			for(int i = 0; i < blastCol.Length; i++)
			{
				Health hp = blastCol[i].GetComponent<Health>();
				if (hp)
				{
					hp.takeDamage(damage);//Note direct hits take double damage.
				}
			}
		}
		Health h = col.gameObject.GetComponent<Health>();
		if (h)
		{
			h.takeDamage(damage);
		}
		Destroy(gameObject);
	}
}
