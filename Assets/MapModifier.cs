using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MapModifier : MonoBehaviour {
	Dictionary<Vector3, Node> pathmap = new Dictionary<Vector3, Node>();
	public static MapModifier singleton;

	public int secondsToTimeout = 10;

	public List<Vector3> path;

	public float recalculateDist;
	public float recalculateTime;
	public int recalculateTiles;

	public float pickupSpawnChance = .01f;

	Vector3 lastRecalcPoint;
	float lastRecalcTime;
	int unusedTiles;

	Transform player;

	Dijkstra paths;

	public GameObject pickup;

	public bool shouldRecalculate()
	{//if we have too many tiles not in the path or if we are far enough away from the last point recalculate, but only if a certain amount of time has passed.
		return (unusedTiles > recalculateTiles || (lastRecalcPoint - player.position).magnitude > recalculateDist) && lastRecalcTime + recalculateTime < Time.time;
	}

	public class Node
	{
		public List<Node> connections;
		public Vector3 position;
		public Node(Vector3 p)
		{
			connections = new List<Node>();
			position = p;
		}
		public void addConnection(Node c)
		{
			if (connections.Contains(c))
			{
				return;
			}
			connections.Add(c);
		}
	}

	public class Dijkstra {
		Node root;
		Vector3 player;
		Dictionary<Vector3, Node> endPoints;
		public Dijkstra(MapModifier owner, Vector3 player, Dictionary<Vector3, Node> graph)
		{
			refresh(owner,player, graph);
		}
		public List<Vector3> getPath(Vector3 start)
		{
			//Gets a prebuilt path from the endpoints dictionary. 
			if(endPoints == null || endPoints.Count < 1)
			{
				return null;
			}
			
			IEnumerable<Vector3> points = endPoints.Keys;//.GetEnumerator();
			Vector3 close = closest(points, start);
			List<Vector3> ret = new List<Vector3>();
			ret.Add(close);
			Node cur = endPoints[close];
			ret.Add(cur.position);
			while (true)
			{
				if(cur.connections.Count < 1)
				{
					Debug.LogWarning("Unconnected node that isn't the root!");
					break;
				}
				Node next = cur.connections[0];
				ret.Add(next.position);
				if (next == root)
				{
					break;
				}
				cur = next;
			}
			return ret;
		}
		public void refresh(MonoBehaviour owner, Vector3 player,Dictionary<Vector3,Node> graph)
		{
			this.player = player;
			buildPath(player, graph);

			//buildPath(player, graph);
		}

		void buildPath(Vector3 player, Dictionary<Vector3, Node> graph)
		{
			print("Starting build path");
			if(graph.Count < 1)
			{
				print("graph size 0");
				return;
			}
			SortedDictionary<float, List<Node>> openList = new SortedDictionary<float,List< Node>>();
			Dictionary<Vector3, Node> newEndPoints = new Dictionary<Vector3, Node>();
			Node home = new Node(graph[closest(graph.Keys, player)].position);
			root = home;
			//print("home: " + home);
			List<Node> z = new List<Node>();
			z.Add(home);
			openList.Add(0,z);
			while(openList.Count > 0)
			{
				KeyValuePair<float, List<Node>> pair = openList.First();//openList.GetEnumerator().Current;
				Node prev = pair.Value[pair.Value.Count-1];
				pair.Value.Remove(prev);
				if(pair.Value.Count == 0)
				{
					openList.Remove(pair.Key);
				}
				if (newEndPoints.ContainsKey(prev.position))
				{
					continue;
				}
				newEndPoints.Add(prev.position, prev);
				float prevDist = pair.Key;
				Node currentEQ = graph[prev.position];
				for (int i = 0; i < currentEQ.connections.Count; i++)
				{
					Vector3 cur = currentEQ.connections[i].position;
					if (newEndPoints.ContainsKey(cur))
					{
						continue;
					}
					float dist = (cur - prev.position).magnitude;
					Node temp = new Node(cur);
			//		print("temp: " + temp);
					temp.addConnection(prev);
					if(openList.ContainsKey(prevDist + dist))
					{
						openList[prevDist + dist].Add(temp);
						//print("dict already contains " + (prevDist + dist) + ". Original: " + openList[prevDist + dist].position + " current: " + temp.position);
						continue;
					}
					List<Node> x = new List<Node>();
					x.Add(temp);
					openList.Add(prevDist + dist, x);
					/*if(openList.Count > 100)
					{
						return;
					}*/
				//	yield return new WaitForSeconds(.1f);
				}
				//yield return new WaitForSeconds(.1f);
			}
			endPoints = newEndPoints;
			//print("end");

			//yield return new YieldInstruction();
		}
	}


	public void ProcessTile(GoMap.GOTile tile)
	{
		///		print("Processing tile");
		//		print(tile);
		StartCoroutine(onTileLoaded(tile));
	}

	void Awake()
	{
		singleton = this;
	}


	IEnumerator onTileLoaded(GoMap.GOTile tile)
	{
		for(int seconds = 0; seconds < secondsToTimeout; seconds++)
		{
			SimpleRoad[] roads = tile.GetComponentsInChildren<SimpleRoad>();
			if(roads.Length > 0)
			{
				recalculateTiles++;
				for (int i = 0; i < roads.Length; i++)
				{
					for(int j = 0; j < roads[i].verts.Length; j++)
					{
						if(Random.value < pickupSpawnChance)
						{
							Instantiate(pickup, roads[i].verts[j], Quaternion.identity);
						}
						Vector3 point = roads[i].verts[j];
						Node n;
						if (pathmap.ContainsKey(point))
						{
							n = pathmap[point];
						}
						else
						{
							n = new Node(point);
							pathmap.Add(point, n);
						}
						if(j > 0)
						{
							Node other = pathmap[roads[i].verts[j - 1]];
							n.addConnection(other);
							other.addConnection(n);
						}
					}
				}

				continue;
			}
			else
			{
//				print("waiting until next frame.");
			}
			yield return new WaitForSeconds(1);
		}
	}

	public static Vector3 closest(IEnumerable<Vector3> list, Vector3 pos)
	{
		Vector3 p = Vector3.zero;// list[0];
		float dist = float.MaxValue;
		foreach(Vector3 v in list) {
			float sqrDist = (pos - v).sqrMagnitude;
			if (sqrDist < dist)
			{
				p = v;
				dist = sqrDist;
			}
		}
		/*for (int i = 0; i < list.Length; i++)
		{
			
		}*/
		return p;
	}
	public List<Vector3> getPath(Vector3 start)
	{
		return paths.getPath(start);
	}

	public static Vector3 closest(Vector3[] list, Vector3 pos)
	{
		Vector3 p = Vector3.zero;// list[0];
		float dist = float.MaxValue;
		for (int i = 0; i < list.Length; i++)
		{
			float sqrDist = (pos - list[i]).sqrMagnitude;
			if (sqrDist < dist)
			{
				p = list[i];
				dist = sqrDist;
			}
		}
		return p;
	}

	void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player").transform;
		paths = new Dijkstra(this, player.position, pathmap);
	}

	void Update()
	{
		if (shouldRecalculate())
		{
			lastRecalcTime = Time.time;
			lastRecalcPoint = player.position;
			recalculateTiles = 0;
			print("Refreshing");
			paths.refresh(this, player.position, pathmap);
		}
	}

}
