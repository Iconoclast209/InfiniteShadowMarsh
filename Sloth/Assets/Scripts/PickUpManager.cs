using UnityEngine;

/// <summary>
/// <para>Structure used to contain information defining how the pick up should affect player</para>
/// <para>This is separate from the PickUpController to allow the "closing" of the information in the Unity Editor, and permit other classes use of the structure if necessary in the future</para>
/// </summary>
[System.Serializable]
public struct PickUpBoost {
    //TODO: Add PickUpBoost information - Used to give boosts to player.
	[Tooltip("Energy boost given.")]
	public float energyFromBoost;	
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
