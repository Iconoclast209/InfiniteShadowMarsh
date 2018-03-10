using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Pick-Ups/Boost Pick-Up")]
public class BoostPickUp : PickUp {

    [Tooltip("Health boost given.")]
    [SerializeField]
    private int healthFromBoost;
    [Tooltip("Energy boost given.")]
    [SerializeField]
    private float energyFromBoost;
    [Tooltip("Energy boost given.")]
    [SerializeField]
    private float gravityReductionDivisor = 2.0f;
    [Tooltip("Energy boost given.")]
    [SerializeField]
    private float movementSpeedMultiplier = 1.5f;
    [Tooltip("Energy boost given.")]
    [SerializeField]
    private float climbSpeedMultiplier = 1.5f;
    [Tooltip("Energy boost given.")]
    [SerializeField]
    private float jumpStrengthMultiplier = 1.5f;
    [Tooltip("Energy boost given.")]
    [SerializeField]
    [Range(.1f, 2.0f)]
    private float timeScalar = .5f;





    ///<summary>Get amount of energy given.</summary>
    public float EnergyFromBoost
    {
        get
        {
            return energyFromBoost;
        }
        private set
        {
            energyFromBoost = value;
        }
    }

    ///<summary>Get Gravity Reduction Divisor.</summary>
    public float GravityReductionDivisor
    {
        get
        {
            return gravityReductionDivisor;
        }
        private set
        {
            gravityReductionDivisor = value;
        }
    }

    ///<summary>Get Movement Speed Multiplier</summary>
    public float MovementSpeedMultiplier
    {
        get
        {
            return movementSpeedMultiplier;
        }
        private set
        {
            movementSpeedMultiplier = value;
        }
    }

    ///<summary>Get Climb Speed Multiplier</summary>
    public float ClimbSpeedMultiplier
    {
        get
        {
            return climbSpeedMultiplier;
        }
        private set
        {
            climbSpeedMultiplier = value;
        }
    }

    ///<summary>Get Jump Strength Multiplier</summary>
    public float JumpStrengthMultiplier
    {
        get
        {
            return jumpStrengthMultiplier;
        }
        private set
        {
            jumpStrengthMultiplier = value;
        }
    }

    ///<summary>Get time scalar.</summary>
    public float TimeScalar
    {
        get
        {
            return timeScalar;
        }
        private set
        {
            timeScalar = value;
        }
    }

    ///<summary>Accessor for Health Boost value</summary>
    public int HealthFromBoost
    {
        get
        {
            return healthFromBoost;
        }

        private set
        {
            healthFromBoost = value;
        }
    }

    public override void Action()
    {
        Player.Singleton.ApplyPickUpBoost(this);
        AudioManager.Singleton.PlayerPickUp();
    }
}
