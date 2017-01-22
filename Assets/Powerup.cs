using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour {
	public int moneyAmount;
	public float healthAmount;
	public Health player;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.CompareTag("Player"))
		{
			player.takeDamage(-healthAmount);
			SpawnManager.singleton.money += moneyAmount;
			Destroy(gameObject);
		}
	}
}
