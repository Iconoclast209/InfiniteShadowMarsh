using UnityEngine;

/// <summary>
/// <para>Structure used to contain information defining how the pick up should affect player</para>
/// <para>This is separate from the PickUpController to allow the "closing" of the information in the Unity Editor, and permit other classes use of the structure if necessary in the future</para>
/// </summary>
[System.Serializable]
public struct PickUpBoost {
    //TODO: Add PickUpBoost information - Used to give boosts to player.
	[Tooltip("Duration (in seconds) to apply temporary bonuses")]
	public float durationInSeconds;	
}

/// <summary>
/// This is the controller class for pick-up behavior.
/// </summary>
public class PickUpManager : MonoBehaviour {
	[Tooltip("Pick-Up Boost Information Structure")]
	public PickUpBoost pickUpBoost;

    /// <summary>
    /// Applies (pickUpBoost) bonuses to player.
    /// </summary>
	public void ExecutePickUp() {
		PlayerManager.Singleton.ApplyPickUpBoost (pickUpBoost);
	}
}
