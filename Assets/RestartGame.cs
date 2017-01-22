using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour {

	void OnDestroy()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}
