using UnityEngine;

public class HealthBar : MonoBehaviour
{
     /// <summary>
    /// Constant defined as 100.
    /// </summary>
    const int HUNDRED_PERCENT = 100;
    /// <summary>
    /// Transform component for this object's parent -- should be set to the health bar UI object
    /// </summary>
    private RectTransform healthBarTransform;
    /// <summary>
    /// Starting width of the health bar UI object's transform -- needed for calculating health bar size
    /// </summary>
    private float healthBarStartingWidth;
    /// <summary>
    /// Maximum possible health count for player -- needed for calculating health bar size
    /// </summary>
    private int playerMaximumHealth;
    /// <summary>
    /// Current health count for player -- needed for calculating health bar size
    /// </summary>
    private int playerCurrentHealth;
    /// <summary>
    /// Percentage of health remaining in player -- needed for calculating health bar size
    /// </summary>
    private int playerCurrentHealthPercentage;

    /// <summary>
    /// Early set-up.  Initialize values and get internal references.
    /// </summary>
    private void Awake()
    {
        healthBarTransform = gameObject.GetComponent<RectTransform>();
        healthBarStartingWidth = healthBarTransform.sizeDelta.x;
    }

    /// <summary>
    /// Set-up involving third-parties.  
    /// </summary>
    private void Start()
    {
        playerMaximumHealth = PlayerManager.Singleton.maximumHealth;
        ResizeHealthBar();
    }

    /// <summary>
    /// Calculate perentage of health remaining and use that to resize the health bar UI
    /// </summary>
    public void ResizeHealthBar()
    {
        CalculateHealthPercentage();
        ResizeHealthBarToPercentage();
    }

    /// <summary>
    /// Calculate percentage of health remaining getting the current health and calculating it with the max health determined in Start() and the HUNDRED_PERCENT constant.
    /// </summary>
    private void CalculateHealthPercentage()
    {
        playerCurrentHealth = PlayerManager.Singleton.CurrentHealth;
        playerCurrentHealthPercentage = (playerCurrentHealth * HUNDRED_PERCENT / playerMaximumHealth);
    }

    /// <summary>
    /// Resizes health bar UI based on the percentage of health remaining and starting width of the health bar UI
    /// </summary>
    private void ResizeHealthBarToPercentage()
    {
        healthBarTransform.sizeDelta = new Vector2((healthBarStartingWidth * playerCurrentHealthPercentage / HUNDRED_PERCENT), healthBarTransform.sizeDelta.y);
    }
}
