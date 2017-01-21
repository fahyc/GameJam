using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapModifier : MonoBehaviour {
	Dictionary<Vector3, SimpleRoad> pathmap = new Dictionary<Vector3, SimpleRoad>();
	public static MapModifier singleton;


	public List<Vector3> path;
	/*
	public void EffectMesh(Mesh m, GoMap.Layer l, string s, Vector3 pos)
	{
		GoMap.GOTile.
	}*/

	public void ProcessTile(GoMap.GOTile tile)
	{
///		print("Processing tile");
//		print(tile);
		SimpleRoad[] roads = tile.GetComponentsInChildren<SimpleRoad>();
		for(int i = 0; i < roads.Length; i++)
		{
			print("vertlen: " + roads[i].verts.Length);
		}
	}

	void Awake()
	{
		singleton = this;
	}

	/*

	// Use this for initialization
	
	// Update is called once per frame
	void Update () {
		
	}*/

}
