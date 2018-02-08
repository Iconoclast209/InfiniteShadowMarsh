using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour {
    public Image livesRemainingImage;
    public Image livesLostImage;
    public Image energyBarImage;
    public Image itemsCollectedImage;
    public Text itemsCollectedText;

    private Canvas HUDCanvas;
    private GameObject livesHUD;
    private int totalLives = 3;

    private void Awake()
    {
        CheckPublicVariablesForValues();
        GetReferenceToCanvas();
        CreateLifeHUD();
        CreateEnergyHUD();
        CreateItemsCollectedHUD();
    }

    private void CreateItemsCollectedHUD()
    {
        Image img = Instantiate<Image>(itemsCollectedImage);
        img.rectTransform.SetParent(HUDCanvas.transform, false);
        Text txt = Instantiate<Text>(itemsCollectedText);
        txt.rectTransform.SetParent(HUDCanvas.transform, false);
    }

    private void CreateEnergyHUD()
    {
        Image img = Instantiate<Image>(energyBarImage);
        img.rectTransform.SetParent(HUDCanvas.transform, false);
    }

    private void CreateLifeHUD()
    {
        for (int x = 0; x < totalLives; x++)
        {
            Image img = Instantiate<Image>(livesRemainingImage);
            img.rectTransform.anchoredPosition = new Vector2(img.rectTransform.sizeDelta.x * x, 0.0f);
            img.rectTransform.SetParent(HUDCanvas.transform, false);
        }
    }

    private void GetReferenceToCanvas()
    {
        HUDCanvas = GameObject.FindObjectOfType<Canvas>();
        if (HUDCanvas == null)
            print(gameObject.name + " cannot locate Canvas object in scene.");
    }

    private void CheckPublicVariablesForValues()
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

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
