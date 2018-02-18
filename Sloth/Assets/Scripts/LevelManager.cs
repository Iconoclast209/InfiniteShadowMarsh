using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>Manager class responsible for level behavior -- includes camera control</summary>
public class LevelManager : MonoBehaviour {

    [Tooltip("Camera acceleration (0 = none; 1 = same as player)")]
    [SerializeField]
    [Range(0.0f, 1.0f)]
    private float camAcceleration = 0.5f;

    ///<summary>Reference to LevelManager singleton.</summary>
    static private LevelManager singleton;

    /// <summary>Number of collectibles on the level.</summary>
    private int numOfCollectibles;

    /// <summary>Number of collectibles currently collected.</summary>
    private int collectiblesCollected;

    /// <summary>Reference to the camera on the level.</summary>
    private Camera levelCamera;





    /// <summary>Accessor for the LevelManager singleton.</summary>
    static public LevelManager Singleton
    {
        get
        {
            if (singleton == null)
                singleton = FindObjectOfType<LevelManager>();
            return singleton;
        }
    }

    /// <summary>Accessor for the number of collectibles on level.</summary>
    public int NumOfCollectibles
    {
        get
        { 
            return numOfCollectibles;
        }

        set
        {
            numOfCollectibles = value;
        }
    }

    /// <summary>Accessor for the number of collectibles currently collected.</summary>
    public int CollectiblesCollected
    {
        get
        {
            return collectiblesCollected;
        }

        set
        {
            collectiblesCollected = value;
        }
    }

    /// <summary>Accessor for the current level camera.</summary>
    public Camera LevelCamera
    {
        get
        {
            return levelCamera;
        }

        private set
        {
            levelCamera = value;
        }
    }

    /// <summary>Accessor for the level camera's acceleration</summary>
    public float CamAcceleration
    {
        get
        {
            return camAcceleration;
        }

        private set
        {
            camAcceleration = value;
        }
    }

    /// <summary>Call this when Stanley dies -- prints message w/ delay then main menu loads.</summary>
    public IEnumerator GameOverWithText(string text)
    {
        UIManager.Singleton.CreateMessage(text);
        yield return new WaitForSecondsRealtime(5.0f);
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    // SLOPPY:  This is bad implementation.  FIX LATER!
    /// <summary>Wrapper to call co-routine from PlayerManager without bugs...</summary>
    public void GameOverWrapper()
    {
        StartCoroutine(GameOverWithText("Stanley was unable to escape the Clock-Tower!"));
    }
  

    /// <summary>Early set-up.  Internal.</summary>
    private void Awake()
    {
        PerformSingletonPattern();
    }

    /// <summary>Late set-up.  External.</summary>
    private void Start()
    {
        LevelCamera = Camera.main;
        StartBGM();
    }

    private void StartBGM()
    {
        StartCoroutine(AudioManager.Singleton.BgmStart());
    }

    /// <summary>Per-frame functionality</summary>
    private void Update()
    {
        if (PlayerManager.Singleton != null)
        {
            Vector3 cameraViewTarget = new Vector3(PlayerManager.Singleton.RB.position.x, PlayerManager.Singleton.RB.position.y, LevelCamera.transform.position.z);
            LevelCamera.transform.position = Vector3.Lerp(LevelCamera.transform.position, cameraViewTarget, CamAcceleration);
        }

    }

    /// <summary>Initializees the singleton object.</summary>
    private void PerformSingletonPattern()
    {
        if (singleton == null)
            singleton = this;
        else if (singleton != this)
            Destroy(gameObject);
    }


}
