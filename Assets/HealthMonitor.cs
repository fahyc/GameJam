using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthMonitor : MonoBehaviour {
	Health health;
	Image img;

	Canvas parent;
	// Use this for initialization
	void Start () {
		img = GetComponent<Image>();
		health = GetComponentInParent<Health>();
		parent = GetComponentInParent<Canvas>();
	}
	
	// Update is called once per frame
	void Update () {
		img.fillAmount = health.health / health.maxHealth;
		parent.transform.rotation = Quaternion.LookRotation(Camera.main.transform.position - transform.position);
	}
}
