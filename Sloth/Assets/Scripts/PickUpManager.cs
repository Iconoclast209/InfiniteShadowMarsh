using System;
using UnityEngine;

/// <summary>
/// <para>Structure used to contain information defining how the pick up should affect player</para>
/// <para>This is separate from the PickUpController to allow the "closing" of the information in the Unity Editor, and permit other classes use of the structure if necessary in the future</para>
/// </summary>
[Serializable]
public class PickUpBoost {
    //TODO: Add PickUpBoost information - Used to give boosts to player.
	[Tooltip("Energy boost given.")]
	public float energyFromBoost;
    public float gravityReductionDivisor = 2.0f;
    public float movementSpeedMultiplier = 1.5f;
    public float climbSpeedMultiplier = 1.5f;
    public float jumpStrengthMultiplier = 1.5f;
    [Range(.1f, 2.0f)]
    public float timeScalar = .5f;
}

/// <summary>
/// This is the controller class for pick-up behavior.
/// </summary>
public class PickUpManager : MonoBehaviour {
	[Tooltip("Pick-Up Boost Information Structure")]
	public PickUpBoost pickUpBoost;

    /// <summary>
    /// If player triggers this pick-up, execute the pick-up and get rid of this object.
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            ExecutePickUp();

    }

    /// <summary>
    /// Applies (pickUpBoost) bonuses to player.
    /// </summary>
	public void ExecutePickUp() {
		PlayerManager.Singleton.ApplyEnergyBoost (pickUpBoost);
        Destroy(gameObject);
	}
}
