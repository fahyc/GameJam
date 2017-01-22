using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {
	public float range;
	public float damage;
	public float timeBetweenShots;
	public Projectile projectile;
	float timer = 0;
	public string[] targetLayers;//what side this turret shoots at.

	Transform target;
	Vector3 targetLastPos = Vector3.zero;

	public GameObject firingParticles;
	public float particlesDuration;
	public float particleTimer;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (!target)
		{
			getTarget();
		}
		particleTimer -= Time.deltaTime;
		//timer -= Time.deltaTime;
		if(particleTimer < 0 && firingParticles)
		{
			firingParticles.SetActive(false);
		}
		if (target)
		{

			if((transform.position - target.transform.position).magnitude > range)
			{
				getTarget();
				if (!target)
				{
					return;
				}
			}

			Vector3 aimPoint = target.transform.position + ((target.transform.position - targetLastPos) / Time.deltaTime * (transform.position - target.transform.position).magnitude / projectile.speed);
			//aimPoint = new Vector3(aimPoint.x, transform.position.y, aimPoint.z);
			aimPoint.y = transform.position.y;
			transform.rotation = Quaternion.LookRotation(aimPoint - transform.position);
			if(timer >= timeBetweenShots)
			{
				Projectile temp = Instantiate(projectile,transform.position,transform.rotation);
				if (firingParticles)
				{
					firingParticles.SetActive(true);
					particleTimer = particlesDuration;
				}
				timer = 0;
			}
			timer += Time.deltaTime;
			targetLastPos = target.transform.position;
		}
	}

	void getTarget()
	{
		Collider[] col = Physics.OverlapSphere(transform.position, range,LayerMask.GetMask(targetLayers));
		List<Transform> possibleTargets = new List<Transform>();
		for(int i = 0; i < col.Length; i++)
		{
			Health temp = col[i].GetComponent<Health>();
			if (temp)
			{
				//target = temp.GetComponent<Transform>();
				possibleTargets.Add(temp.transform);
			}
		}
		target = closest(possibleTargets, transform.position);
	}



	

	public static Transform closest(List<Transform> list,Vector3 pos)
	{
		Transform t = null;// list[0];
		float dist = float.MaxValue;
		for(int i = 0; i < list.Count; i++)
		{
			float sqrDist = (pos - list[i].position).sqrMagnitude;
			if (sqrDist < dist)
			{
				t = list[i];
				dist = sqrDist;
			}
		}
		return t;
	}
}
