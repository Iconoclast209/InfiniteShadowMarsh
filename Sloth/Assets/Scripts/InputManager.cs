using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages player input, calling out appropriate functionality.
/// </summary>
public class InputManager : MonoBehaviour {
    /// <summary>
    /// Singleton reference to InputManager.
    /// </summary>
    static private InputManager singleton;
    /// <summary>
    /// Access singleton reference to InputManager
    /// </summary>
    static public InputManager Singleton
    {
        get
        {
            if (singleton == null)
                singleton = FindObjectOfType<InputManager>();
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
        } else
        {
            Destroy(gameObject);
        }
    }
}
