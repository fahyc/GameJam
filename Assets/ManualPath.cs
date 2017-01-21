using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManualPath : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		MapModifier.singleton.path.Add(transform.position);
	}
	
}
