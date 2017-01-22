using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthMonitor : MonoBehaviour {
	Health health;
	Image img;
	// Use this for initialization
	void Start () {
		img = GetComponent<Image>();
		health = GetComponentInParent<Health>();
	}
	
	// Update is called once per frame
	void Update () {
		img.fillAmount = health.health / health.maxHealth;
	}
}
