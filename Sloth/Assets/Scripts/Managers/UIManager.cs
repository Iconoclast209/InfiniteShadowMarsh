using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

///<summary>Manager class for UI behavior.</summary>
public class UIManager : MonoBehaviour
{
    [Tooltip("Reference to the Text prefab to use to display in-game messages.")][SerializeField]
    private Text inGameMessageBoxToSpawn;

    /// <summary>Reference to the spawned instance of inGameMessageBoxToSpawn</summary>
    private Text spawnedMessageBox;

    /// <summary>Singleton reference to UIManager object</summary>
    static private UIManager singleton;

    /// <summary>Reference to scene canvas.  Checked every scene change!</summary>
    private Canvas sceneCanvas;





    /// <summary>Accessor to singleton reference to UIManager object;</summary>
    static public UIManager Singleton
    {
        get
        {
            if (singleton == null)
                singleton = FindObjectOfType<UIManager>();
            return singleton;
        }
    }
  
    /// <summary>Accessor to the message box prefab we should spawn.</summary>
    public Text InGameMessageBoxToSpawn
    {
        get
        {
            return inGameMessageBoxToSpawn;
        }

        private set
        {
            inGameMessageBoxToSpawn = value;
        }
    }
    
    /// <summary>Accessor to the spawned message box.</summary>
    public Text SpawnedMessageBox
    {
        get
        {
            return spawnedMessageBox;
        }

        private set
        {
            spawnedMessageBox = value;
        }
    }





    /// <summary>Start a new game.</summary>
    public void StartNewGame()
    {
        //TODO: Add any necessary DataManager references.
        AudioManager.Singleton.MenuLoop(false);
        StartCoroutine(AudioManager.Singleton.MenuStartGame());

    }

    public void LoadNewGame()
    {
        SceneManager.LoadScene("Level01", LoadSceneMode.Single);
    }
    

    /// <summary>Load "Credits" scene.</summary>
    public void ShowCredits()
    {
        AudioManager.Singleton.MenuSelect();
        SceneManager.LoadScene("Credits", LoadSceneMode.Single);
    }

    /// <summary>Load "Main Menu" scene.</summary>
    public void LoadMainMenu()
    {
        AudioManager.Singleton.MenuSelect();
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    /// <summary>Exit the game in player; quit test in Editor.</summary>
    public void ExitGame()
    {
        AudioManager.Singleton.MenuSelect();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();    
#endif
    }

    /// <summary>Creates a text message on-screne using inGameMessageBox</summary>
    public void CreateMessage(string messageToDisplay)
    {
        //Redundancy check to make sure a spawned message box exists!
        if (SpawnedMessageBox == null)
        {
            SetupMessageBox();
        }
            SpawnedMessageBox.text = messageToDisplay;
    }

    /// <summary>Removes any on-screen message created by CreateMessage.</summary>
    public void RemoveMessage()
    {
        SpawnedMessageBox.text = "";
    }





    /// <summary>Early set-up.  Internal.  Establish singleton.</summary>
    private void Awake()
    {
        InitializeSingletonPattern();
        AudioManager.Singleton.MenuLoop(true);
        SceneManager.sceneLoaded += FindSceneCanvas;
    }

    /// <summary>Initialize the Unity Singleton Pattern.</summary>
    private void InitializeSingletonPattern()
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

    /// <summary>Gets reference to each scene's canvas.<para>Delegate method.</para></summary>
    private void FindSceneCanvas(Scene scene, LoadSceneMode loadSceneMode)
    {
        sceneCanvas = FindObjectOfType<Canvas>();
        if (sceneCanvas == null)
            print("FindSceneCanvas failed!");
    }

    /// <summary>Gets reference to each scene's canvas.<para>Delegate method.</para></summary>
    private void SetupMessageBox()
    {
            SpawnedMessageBox = Instantiate<Text>(InGameMessageBoxToSpawn);
            SpawnedMessageBox.rectTransform.SetParent(sceneCanvas.transform, false);
            SpawnedMessageBox.text = "";
    }
}
