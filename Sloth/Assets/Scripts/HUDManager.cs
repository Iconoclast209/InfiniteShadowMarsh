using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manager class for HUD functionality.
/// </summary>
public class HUDManager : MonoBehaviour {
    [Tooltip("This is the image component used for displaying lives remaining.\n\nIn designer, place the prefab Image in the desired spawn location for the left-most image.")]
    [SerializeField]
    private Image livesRemainingImage;

    [Tooltip("This is the image component used for displaying lives lost.\n\nIn designer, place the prefab Image in the desired spawn location for the left-most image.")]
    [SerializeField]
    private Image livesLostImage;

    [Tooltip("This is the image component used for displaying the energy bar.\n\nIn designer, place the prefab Image in the desired spawn location for the image.")]
    [SerializeField]
    private Image energyBarImage;

    [Tooltip("This is the image component used for displaying the 'Items Collected' icon.\n\nIn designer, place the prefab Image in the desired spawn location for the image.")]
    [SerializeField]
    private Image itemsCollectedImage;

    [Tooltip("This is the text component used for displaying items collected.\n\nIn designer, place the prefab Text in the desired spawn location.")]
    [SerializeField]
    private Text itemsCollectedText;

    ///<summary>Reference to screen canvas.</summary>
    private Canvas HUDCanvas;

    //TODO: Replace this with value from player manager to make lives controlled by player, not UI.
    ///<summary>TEMP</summary>
    private int totalLives = 3;

    /// <summary>Early set-up.  Internal.</summary>
    private void Awake()
    {
        CheckSerializedFieldsForValues();
        GetReferenceToCanvas();
        CreateLifeHUD();
        CreateEnergyHUD();
        CreateItemsCollectedHUD();
    }

    //TODO: Add functionality that utilizes Items Collected.
    /// <summary>Create HUD element that tracks items collected throughout level.</summary>
    private void CreateItemsCollectedHUD()
    {
        Image img = Instantiate<Image>(itemsCollectedImage);
        img.rectTransform.SetParent(HUDCanvas.transform, false);
        Text txt = Instantiate<Text>(itemsCollectedText);
        txt.rectTransform.SetParent(HUDCanvas.transform, false);
    }

    //TODO: Add functionality that utilizes Energy Bar HUD
    /// <summary>Create HUD element that tracks player energy level.</summary>
    private void CreateEnergyHUD()
    {
        Image img = Instantiate<Image>(energyBarImage);
        img.rectTransform.SetParent(HUDCanvas.transform, false);
    }

    //TODO: Add functionality that utilizes Life HUD
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

    ///<summary>Get reference to the level canvas.</summary>
    private void GetReferenceToCanvas()
    {
        HUDCanvas = GameObject.FindObjectOfType<Canvas>();
        if (HUDCanvas == null)
            print(gameObject.name + " cannot locate Canvas object in scene.");
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
}
