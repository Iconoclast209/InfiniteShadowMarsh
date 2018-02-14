using UnityEngine;
using UnityEngine.SceneManagement;

///<summary>Class that handles Splash screen functionality.</summary>
public class SplashScreen : MonoBehaviour
{
    [Tooltip("Delay in seconds before loading next scene.")][SerializeField]
    private float delayInSeconds = 2.5f;
    
	///<summary>Early Set-up.  Internal.</summary>
	private void Start () {
        Invoke("LoadNextLevel", delayInSeconds);
	}
	
	///<summary>Loads specified scene/level.</summary>
	private void LoadNextLevel ()
    {
        SceneManager.LoadScene(1);
	}
}
