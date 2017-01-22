using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyMaker : MonoBehaviour {
	public int moneyPerSecond;

	float timer;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		if(timer >= 1)
		{
			timer = 0;
			SpawnManager.singleton.money += moneyPerSecond;
		}
	}
}
