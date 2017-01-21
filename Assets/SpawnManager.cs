using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {

	public int money;

	public Transform player;

	public float turretRadius;

	public GameObject[] turrets;
	public int[] costs;

	public void SpawnTurret(int index) // GameObject turret,int price)
	{
		if(costs[index] > money)
		{
			notEnoughMoney();
			return;
		}
		else
		{
			Collider[] col = Physics.OverlapSphere(player.position, turretRadius);
			for (int i = 0; i < col.Length; i++)
			{
				print(col[i].gameObject);
				Turret tur = col[i].GetComponent<Turret>();
				if (tur)
				{
					collide();
					return;
				}
			}
			//print("Spawning");*
			GameObject temp = Instantiate(turrets[index]);
			temp.transform.position = player.position;
		}
	}


	public void collide()
	{
		print("collide");
	}
	public void notEnoughMoney()
	{
		print("not enough money");
	}
	/*
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}*/
}
