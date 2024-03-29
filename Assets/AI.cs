﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour {

	//public Health target;

	public float speed;

	PathFollower pathFollower;


	List<GameObject> walls = new List<GameObject>();

	Vector3 lastPos;
	
	//public LineRenderer laser;

	public class PathFollower
	{
		public PathFollower(List<Vector3>path)
		{
			for(int i = 0; i < path.Count; i++)
			{
				//int equalCount = 0;
				for(int j = 0; j < path.Count; j++)
				{
					if(path[i] == path[j] && i != j)
					{
						path.RemoveAt(j);
					}
				}
			}
			currentPath = path;
			if(path.Count < 2)
			{
				currentLen = 1;
				pathProgress = 1;
				return;
			}
			currentLen = (path[0] - path[1]).magnitude;
			if(currentLen == 0)
			{
				Debug.LogWarning("currentlen is zero");
			}
		}
		public List<Vector3> currentPath;

		public int pathIndex = 0;
		public float pathProgress = 0;

		public float currentLen;
		

		public Vector3 step(Vector3 curPos, float speed)
		{
//			print("speed " + speed + " currentLen: " + currentLen + " pathProgress " + pathProgress);
			float nextProgress = pathProgress + (speed / currentLen * Time.deltaTime);
			if(nextProgress >= 1)
			{
				nextProgress -= 1;
				pathIndex++;

				if (pathIndex + 1 >= currentPath.Count)
				{
					return currentPath[currentPath.Count - 1];
				}
				currentLen = (currentPath[pathIndex] - currentPath[pathIndex+1]).magnitude;
			}

			if (pathIndex + 1 == currentPath.Count)
			{
				return currentPath[currentPath.Count - 1];
			}
			pathProgress = nextProgress;
			return Vector3.Lerp(currentPath[pathIndex], currentPath[pathIndex + 1],pathProgress);
		}
	}


	// Use this for initialization
	void Start () {
		//laser = Instantiate(laser);
	}
	
	// Update is called once per frame
	void Update () {
		if(pathFollower == null)
		{
			getPath();
			//pathFollower = new PathFollower(MapModifier.singleton.path);
		}
		else
		{
			if(pathFollower.pathIndex == pathFollower.currentPath.Count)
			{
				Attack();
			}
			else
			{
				//for(int i = )
				if (!hittingWall())
				{
					Vector3 look = transform.position - lastPos;
					if (look.sqrMagnitude > 0)
					{
						transform.rotation = Quaternion.LookRotation(look);
					}

					lastPos = transform.position;
					transform.position = pathFollower.step(transform.position, speed);
				}
			}
		}
	}

	void getPath()
	{
		List<Vector3> p = MapModifier.singleton.getPath(transform.position);
		if (p != null)
		{
			pathFollower = new PathFollower(p);
		}
		//pathFollower = MapModifier.singleton
		//print("getting path");
	}

	void Attack()
	{
		if(Random.value < .01)
		{
			getPath();
		}
		/*
		laser.SetPosition(0, transform.position);
		laser.SetPosition(1, target.transform.position);
		
		print("Attacking");*/
	}

	bool hittingWall()
	{
	//	int count = 0;
		for(int i = 0; i < walls.Count; i++)
		{
			if (walls[i])
			{
				return true;
			}
		}
		return false;
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.CompareTag("Wall")) {
			walls.Add(col.gameObject);
		}
	}
	void OnTriggerExit(Collider col)
	{
		print("Exiting " + col.gameObject);
		if (col.gameObject.CompareTag("Wall"))
		{
			walls.Add(col.gameObject);
		}
	}
}
