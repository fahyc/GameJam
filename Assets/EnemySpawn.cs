using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour {
	public float timeBetweenWaves;
	public float[] weights;
	public Transform[] enemies;

	public float spawnDistance;

	public float timer = 0;
	float total = 0;

	public int enemiesPerWave;

	public static EnemySpawn singleton;

	public float enemyMultiplier = 1;
	public float timePower = .5f;

	// Use this for initialization
	void Awake () {
		for (int i = 0; i < weights.Length; i++)
		{
			total += weights[i];
		}
		singleton = this;
	}
	
	// Update is called once per frame
	void Update () {
		timer -= Time.deltaTime;
		if (timer <= 0)
		{
			timer = timeBetweenWaves;
			float multiplier = GameObject.FindGameObjectsWithTag("Building").Length;// * enemyMultiplier + Mathf.Pow(Time.time,timePower);
			print("Spawning " + ( enemiesPerWave + multiplier));
			for(int i = 0; i < enemiesPerWave + multiplier; i++)
			{
				Spawn();
			}
		}
	}

	void Spawn()
	{
		float ran = Random.value * total;
		for(int i = 0; i < weights.Length; i++)
		{
			ran -= weights[i];
			if(ran <= 0)
			{
				Vector2 point = Random.insideUnitCircle.normalized * spawnDistance;
				Vector3 actual = new Vector3(point.x, 0, point.y) + transform.position;
				Instantiate(enemies[i], actual, transform.rotation);
				return;
			}
		}
	}
}
