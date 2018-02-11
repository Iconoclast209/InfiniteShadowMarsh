using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manager class for HUD functionality.
/// </summary>
public class HUDManager : MonoBehaviour
{
    ///<summary>Reference to HUDManager singleton</summary>
    static private HUDManager singleton;

    /// <summary>Constant defined as 100.</summary>
    private const int HUNDRED_PERCENT = 100;

    [Tooltip("This is the image component used for displaying lives remaining.\n\nIn designer, place the prefab Image in the desired spawn location for the left-most image.")][SerializeField]
    private Image livesRemainingImage;

    [Tooltip("This is the image component used for displaying lives lost.\n\nIn designer, place the prefab Image in the desired spawn location for the left-most image.")][SerializeField]
    private Image livesLostImage;

    [Tooltip("This is the image component used for displaying the life bar.\n\nIn designer, place the prefab Image in the desired spawn location for the image.")][SerializeField]
    private Image healthBarImage;

    [Tooltip("This is the image component used for displaying the energy bar.\n\nIn designer, place the prefab Image in the desired spawn location for the image.")][SerializeField]
    private Image energyBarImage;

    [Tooltip("This is the image component used for displaying the 'Items Collected' icon.\n\nIn designer, place the prefab Image in the desired spawn location for the image.")][SerializeField]
    private Image itemsCollectedImage;

    [Tooltip("This is the text component used for displaying items collected.\n\nIn designer, place the prefab Text in the desired spawn location.")][SerializeField]
    private Text itemsCollectedText;

    /// <summary>Transform component for this object's parent -- should be set to the health bar UI object</summary>
    private RectTransform healthBarRect;

    /// <summary>Starting width of the health bar UI object's transform -- needed for calculating health bar size</summary>
    private float healthBarStartingWidth;

    /// <summary>Maximum possible health count for player -- needed for calculating health bar size</summary>
    private int playersMaximumHealth;

    /// <summary>Current health count for player -- needed for calculating health bar size</summary>
    private int playersCurrentHealth;

    /// <summary>Percentage of health remaining in player -- needed for calculating health bar size</summary>
    private int playersCurrentHealthPercentage;

    /// <summary>Transform component for this object's parent -- should be set to the energy bar UI object</summary>
    private RectTransform energyBarRect;

    /// <summary>Starting width of the energy bar UI object's transform -- needed for calculating energy bar size</summary>
    private float energyBarStartingWidth;

    /// <summary>Maximum possible energy count for player -- needed for calculating energy bar size</summary>
    private int playersMaximumEnergy;

    /// <summary>Current energy count for player -- needed for calculating energy bar size</summary>
    private int playersCurrentEnergy;

    /// <summary>Percentage of energy remaining in player -- needed for calculating energy bar size</summary>
    private int currentEnergyPercentage;

    ///<summary>Reference to screen canvas.</summary>
    private Canvas HUDCanvas;

    //TODO: Replace this with value from player manager to make lives controlled by player, not UI.
    ///<summary>TEMP</summary>
    private int totalLives = 3;





    ///<summary>Access HUDManager singleton</summary>
    static public HUDManager Singleton
    {
        get
        {
            if (singleton == null)
                singleton = FindObjectOfType<HUDManager>();
            return singleton;
        }
    }

    /// <summary>Calculate perentage of health remaining and use that to resize the health bar HUD</summary>
    public void ResizeHealthBar()
    {
        CalculateHealthPercentage();
        ResizeHealthBarToPercentage();
    }

    /// <summary>Calculate perentage of energy remaining and use that to resize the energy bar HUD</summary>
    public void ResizeEnergyBar()
    {
        CalculateEnergyPercentage();
        ResizeEnergyBarToPercentage();
    }





    /// <summary>Early set-up.  Internal.</summary>
    private void Awake()
    {
        InitializeSingleton();
        CheckSerializedFieldsForValues();
        GetReferenceToCanvas();
        CreateLifeHUD();
        CreateHealthHUD();
        SetupHealthHUD();
        CreateEnergyHUD();
        SetupEnergyHUD();
        CreateItemsCollectedHUD();

    }
    
    /// <summary>Late Set-up.  External</summary>
    private void Start()
    {        
        InitializeHealthHUD();
        InitializeEnergyHUD();
    }

    ///<summary>Initialize HUDManager singleton</summary>
    private void InitializeSingleton()
    {
        if (singleton == null)
            singleton = this;
        else if (singleton != this)
            Destroy(gameObject);
    }

    ///<summary>Error checking on serialized fields.</summary>
    private void CheckSerializedFieldsForValues()
    {
        if (livesRemainingImage == null)
            print(gameObject.name + " has no livesRemainingIcon set.");
        if (livesLostImage == null)
            print(gameObject.name + " has no livesRemainingIcon set.");
        if (energyBarImage == null)
            print(gameObject.name + " has no energyBarIcon set.");
        if (itemsCollectedImage == null)
            print(gameObject.name + " has no itemsCollectedIcon set.");
    }

    ///<summary>Get reference to the level canvas.</summary>
    private void GetReferenceToCanvas()
    {
        HUDCanvas = GameObject.FindObjectOfType<Canvas>();
        if (HUDCanvas == null)
            print(gameObject.name + " cannot locate Canvas object in scene.");
    }

    //PRONTO: Add functionality that utilizes Life HUD
    ///<summary>Create HUD element that tracks player lives.</summary>
    private void CreateLifeHUD()
    {
        for (int x = 0; x < totalLives; x++)
        {
            Image img = Instantiate<Image>(livesRemainingImage);
            img.rectTransform.anchoredPosition = new Vector2(img.rectTransform.sizeDelta.x * x, 0.0f);
            img.rectTransform.SetParent(HUDCanvas.transform, false);
        }
    }

    ///<summary>Create HUD element that tracks player health.</summary>
    private void CreateHealthHUD()
    {
        healthBarImage = Instantiate<Image>(healthBarImage);
        healthBarImage.rectTransform.SetParent(HUDCanvas.transform, false);
    }

    ///<summary>Health HUD Set-up.  Needed for later resizing functions.</summary>
    private void SetupHealthHUD()
    {
        healthBarRect = healthBarImage.GetComponent<RectTransform>();
        healthBarStartingWidth = healthBarRect.sizeDelta.x;
    }

    /// <summary>Create HUD element that tracks player energy level.</summary>
    private void CreateEnergyHUD()
    {
        energyBarImage = Instantiate<Image>(energyBarImage);
        energyBarImage.rectTransform.SetParent(HUDCanvas.transform, false);
    }

    /// <summary>Energy HUD Set-up.  Needed for later resizing functions.</summary>
    private void SetupEnergyHUD()
    {
        energyBarRect = energyBarImage.GetComponent<RectTransform>();
        energyBarRect.sizeDelta = new Vector2(300.0f, energyBarRect.sizeDelta.y);
        energyBarStartingWidth = energyBarRect.sizeDelta.x;
    }

    //PRONTO: Add functionality that utilizes Items Collected.
    /// <summary>Create HUD element that tracks items collected throughout level.</summary>
    private void CreateItemsCollectedHUD()
    {
        Image img = Instantiate<Image>(itemsCollectedImage);
        img.rectTransform.SetParent(HUDCanvas.transform, false);
        Text txt = Instantiate<Text>(itemsCollectedText);
        txt.rectTransform.SetParent(HUDCanvas.transform, false);
    }

    /// <summary>Calculate percentage of health remaining.</summary>
    private void CalculateHealthPercentage()
    {
        playersCurrentHealth = PlayerManager.Singleton.CurrentHealth;
        playersCurrentHealthPercentage = (playersCurrentHealth * HUNDRED_PERCENT / playersMaximumHealth);
    }

    /// <summary>Performs health HUD resizing.</summary>
    private void ResizeHealthBarToPercentage()
    {
        healthBarRect.sizeDelta = new Vector2((healthBarStartingWidth * playersCurrentHealthPercentage / HUNDRED_PERCENT), healthBarRect.sizeDelta.y);
    }

    /// <summary>Calculate percentage of energy remaining.</summary>
    private void CalculateEnergyPercentage()
    {
        playersCurrentEnergy = (int)PlayerManager.Singleton.CurrentEnergy;
        if (playersCurrentEnergy > 0)
            currentEnergyPercentage = (playersCurrentEnergy * HUNDRED_PERCENT / playersMaximumEnergy);
        else
            currentEnergyPercentage = 0;
    }

    /// <summary>Performs energy HUD resizing.</summary>
    private void ResizeEnergyBarToPercentage()
    {
            energyBarRect.sizeDelta = new Vector2((energyBarStartingWidth * currentEnergyPercentage / HUNDRED_PERCENT), energyBarRect.sizeDelta.y);
    }

    /// <summary>Initialize health HUD settings using Player Manager.</summary>
    private void InitializeHealthHUD()
    {
        playersMaximumHealth = PlayerManager.Singleton.MaximumHealth;
        ResizeHealthBar();
    }

    /// <summary>Initialize energy HUD settings using Player Manager</summary>
    private void InitializeEnergyHUD()
    {
        playersMaximumEnergy = PlayerManager.Singleton.MaximumEnergy;
        ResizeEnergyBar();
    }
}

