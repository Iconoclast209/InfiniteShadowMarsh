using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Invoke("LoadNextLevel", 2.5f);
	}
	
	// Update is called once per frame
	void LoadNextLevel ()
    {
        SceneManager.LoadScene(1);
	}
}
