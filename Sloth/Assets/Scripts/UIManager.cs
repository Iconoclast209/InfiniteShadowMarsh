using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    /// <summary>
    /// Singleton reference to UIManager object
    /// </summary>
    static private UIManager singleton;
    /// <summary>
    /// Access singleton reference to UIManager object;
    /// </summary>
    static public UIManager Singleton
    {
        get
        {
            if (singleton == null)
                singleton = FindObjectOfType<UIManager>();
            return singleton;
        }
    }

    /// <summary>
    /// Early set-up.  Internal.  Establish singleton.
    /// </summary>
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

    public void StartNewGame()
    {
        //PRONTO: Add StartNewGame() functionality
        SceneManager.LoadScene("Test", LoadSceneMode.Single);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();    
#endif
    }
}
