using UnityEngine;

public class EnergyBar : MonoBehaviour
{
    /// <summary>
    /// Constant defined as 100.
    /// </summary>
    const int HUNDRED_PERCENT = 100;
    /// <summary>
    /// Transform component for this object's parent -- should be set to the energy bar UI object
    /// </summary>
    private RectTransform energyBarTransform;
    /// <summary>
    /// Starting width of the energy bar UI object's transform -- needed for calculating energy bar size
    /// </summary>
    private float energyBarStartingWidth;
    /// <summary>
    /// Maximum possible energy count for player -- needed for calculating energy bar size
    /// </summary>
    private int maximumEnergy;
    /// <summary>
    /// Current energy count for player -- needed for calculating energy bar size
    /// </summary>
    private int currentEnergy;
    /// <summary>
    /// Percentage of energy remaining in player -- needed for calculating energy bar size
    /// </summary>
    private int currentEnergyPercentage;

    /// <summary>
    /// Early set-up.  Initialize values and get internal references.
    /// </summary>
    private void Awake()
    {
        energyBarTransform = gameObject.GetComponent<RectTransform>();
        energyBarStartingWidth = energyBarTransform.sizeDelta.x;
    }

    /// <summary>
    /// Set-up involving third-parties.  
    /// </summary>
    private void Start()
    {
        maximumEnergy = PlayerManager.Singleton.maximumEnergy;
        ResizeEnergyBar();
    }

    /// <summary>
    /// Calculate perentage of energy remaining and use that to resize the energy bar UI
    /// </summary>
    public void ResizeEnergyBar()
    {
        CalculateEnergyPercentage();
        ResizeEnergyBarToPercentage();
    }

    /// <summary>
    /// Calculate percentage of energy remaining getting the current energy and calculating it with the max energy determined in Start() and the HUNDRED_PERCENT constant.
    /// </summary>
    private void CalculateEnergyPercentage()
    {
        currentEnergy = (int)PlayerManager.Singleton.CurrentEnergy;
        if (currentEnergy > 0)
            currentEnergyPercentage = (currentEnergy * HUNDRED_PERCENT / maximumEnergy);
        else
            currentEnergyPercentage = 0;
    }

    /// <summary>
    /// Resizes energy bar UI based on the percentage of energy remaining and starting width of the energy bar UI
    /// </summary>
    private void ResizeEnergyBarToPercentage()
    {
        energyBarTransform.sizeDelta = new Vector2((energyBarStartingWidth * currentEnergyPercentage / HUNDRED_PERCENT), energyBarTransform.sizeDelta.y);
    }
}
