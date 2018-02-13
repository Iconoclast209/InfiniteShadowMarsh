using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {
    private int numOfCollectables;
    private int collectablesCollected;
    static private LevelManager singleton;

    static public LevelManager Singleton
    {
        get
        {
            if (singleton == null)
                singleton = FindObjectOfType<LevelManager>();
            return singleton;
        }
    }

    public int NumOfCollectables
    {
        get
        {
            return numOfCollectables;
        }

        set
        {
            numOfCollectables = value;
        }
    }

    public int CollectablesCollected
    {
        get
        {
            return collectablesCollected;
        }

        set
        {
            collectablesCollected = value;
        }
    }

    private void Awake()
    {
        PerformSingletonPattern();

    }

    private void PerformSingletonPattern()
    {
        if (singleton == null)
            singleton = this;
        else if (singleton != this)
            Destroy(gameObject);
    }
}
