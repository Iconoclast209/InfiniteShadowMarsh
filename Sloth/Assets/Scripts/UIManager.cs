using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

///<summary>Manager class for UI behavior.</summary>
public class UIManager : MonoBehaviour
{
    /// <summary>Singleton reference to UIManager object</summary>
    static private UIManager singleton;
    /// <summary>Access singleton reference to UIManager object;</summary>
    static public UIManager Singleton
    {
        get
        {
            if (singleton == null)
                singleton = FindObjectOfType<UIManager>();
            return singleton;
        }
    }

    /// <summary>Early set-up.  Internal.  Establish singleton.</summary>
    private void Awake()
    {
        if (singleton == null)
        {
            singleton = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>Start a new game.</summary>
    public void StartNewGame()
    {
        //TODO: Add any necessary DataManager references.
        SceneManager.LoadScene("Test", LoadSceneMode.Single);
    }

    /// <summary>Exit the game in player; quit test in Editor.</summary>
    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();    
#endif
    }
}
