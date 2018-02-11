using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

///<summary>Manager class for UI behavior.</summary>
public class UIManager : MonoBehaviour
{
    [Tooltip("Reference to the Text prefab to use to display in-game messages.")][SerializeField]
    private Text inGameMessageBox;

    /// <summary>Singleton reference to UIManager object</summary>
    static private UIManager singleton;

    /// <summary>Reference to scene canvas.  Checked every scene change!</summary>
    private Canvas sceneCanvas;





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

    /// <summary>Creates a text message on-screne using inGameMessageBox</summary>
    public void CreateMessage(string messageToDisplay)
    {
        inGameMessageBox.text = messageToDisplay;
    }

    /// <summary>Removes any on-screen message created by CreateMessage.</summary>
    public void RemoveMessage()
    {
        inGameMessageBox.text = "";
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

        SceneManager.sceneLoaded += FindSceneCanvas;
        SceneManager.sceneLoaded += SetupMessageBox;
    }

    /// <summary>Gets reference to each scene's canvas.<para>Delegate method.</para></summary>
    private void FindSceneCanvas(Scene scene, LoadSceneMode loadSceneMode)
    {
        sceneCanvas = FindObjectOfType<Canvas>();
        if (sceneCanvas == null)
            print("FindSceneCanvas failed!");
    }

    /// <summary>Gets reference to each scene's canvas.<para>Delegate method.</para></summary>
    private void SetupMessageBox(Scene scene, LoadSceneMode loadSceneMode)
    {
        inGameMessageBox = Instantiate<Text>(inGameMessageBox);
        inGameMessageBox.rectTransform.SetParent(sceneCanvas.transform, false);
        inGameMessageBox.text = "";
    }
}
