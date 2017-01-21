using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyMonitor : MonoBehaviour {
	SpawnManager manager;
	Text text;
	string originalText;
	// Use this for initialization
	void Start () {
		manager = GetComponentInParent<SpawnManager>();
		text = GetComponent<Text>();
		originalText = text.text;
	}
	
	// Update is called once per frame
	void Update () {
		text.text = originalText.Replace("[money]", manager.money.ToString());
	}
}
