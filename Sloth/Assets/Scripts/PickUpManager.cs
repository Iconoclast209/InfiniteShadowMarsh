using System;
using UnityEngine;

/// <summary>
/// <para>Structure used to contain information defining how the pick up should affect player</para>
/// <para>This is separate from the PickUpController to allow the "closing" of the information in the Unity Editor, and permit other classes use of the structure if necessary in the future</para>
/// </summary>
[Serializable]
public class PickUpBoost {
	[Tooltip("Energy boost given.")][SerializeField]
    private float energyFromBoost;
    [Tooltip("Energy boost given.")] [SerializeField]
    private float gravityReductionDivisor = 2.0f;
    [Tooltip("Energy boost given.")] [SerializeField]
    private float movementSpeedMultiplier = 1.5f;
    [Tooltip("Energy boost given.")] [SerializeField]
    private float climbSpeedMultiplier = 1.5f;
    [Tooltip("Energy boost given.")] [SerializeField]
    private float jumpStrengthMultiplier = 1.5f;
    [Tooltip("Energy boost given.")][SerializeField][Range(.1f, 2.0f)]
    private float timeScalar = .5f;

    ///<summary>Get amount of energy given.</summary>
    public float EnergyFromBoost
    {
        get
        {
            return energyFromBoost;
        }
    }

    ///<summary>Get Gravity Reduction Divisor.</summary>
    public float GravityReductionDivisor
    {
        get
        {
            return gravityReductionDivisor;
        }
    }

    ///<summary>Get Movement Speed Multiplier</summary>
    public float MovementSpeedMultiplier
    {
        get
        {
            return movementSpeedMultiplier;
        }
    }

    ///<summary>Get Climb Speed Multiplier</summary>
    public float ClimbSpeedMultiplier
    {
        get
        {
            return climbSpeedMultiplier;
        }
    }

    ///<summary>Get Jump Strength Multiplier</summary>
    public float JumpStrengthMultiplier
    {
        get
        {
            return jumpStrengthMultiplier;
        }
    }

    ///<summary>Get time scalar.</summary>
    public float TimeScalar
    {
        get
        {
            return timeScalar;
        }
    }
}

/// <summary>Manager class for pick-up behavior.</summary>
public class PickUpManager : MonoBehaviour
{
    [Tooltip("Pick-Up Boost Information Structure")][SerializeField]
    private PickUpBoost pickUpBoost;

    /// <summary>If player triggers this pick-up, execute the pick-up and get rid of this object.</summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            ExecutePickUp();
    }

    /// <summary>Applies (pickUpBoost) bonuses to player.</summary>
	private void ExecutePickUp() {
		PlayerManager.Singleton.ApplyEnergyBoost (pickUpBoost);
        Destroy(gameObject);
	}
}
