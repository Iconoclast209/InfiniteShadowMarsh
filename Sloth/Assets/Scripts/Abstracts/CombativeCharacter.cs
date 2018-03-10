using UnityEngine;

///<summary>Base class for all "combative" characters.  This will imbue them with the common combat characteristics.</summary>
public abstract class CombativeCharacter : BaseCharacter, IDamageable, IDamaging
{
    [Space(10)][Header("Basic Combat Stats")]
    [Tooltip("Starting health.")][SerializeField]
    protected int startingHealth;
    [Tooltip("Base damage dealt. Attacks can be scaled individually.")][SerializeField]
    protected int baseDamage;
    /// <summary>Current health value.</summary>
    protected int currentHealth;
    /// <summary>Is Character alive?</summary>
    protected bool isAlive = true;





    /// <summary>Accessor for starting health.</summary>
    public int StartingHealth
    {
        get
        {
            return startingHealth;
        }

        set
        {
            startingHealth = value;
        }
    }
    /// <summary>Accessor for base damage.</summary>
    public int BaseDamage
    {
        get
        {
            return baseDamage;
        }

        set
        {
            baseDamage = value;
        }
    }
    /// <summary>Accessor for current health.</summary>
    public int CurrentHealth
    {
        get
        {
            return currentHealth;
        }

        set
        {
            currentHealth = value;
        }
    }
    /// <summary>Accessor for alive status.</summary>
    public bool IsAlive
    {
        get
        {
            return isAlive;
        }

        set
        {
            isAlive = value;
        }
    }





    /// <summary>Initialize current health.</summary>
    public virtual void Awake()
    {
        CurrentHealth = StartingHealth;
    }
    /// <summary><para>Overrideable function for taking damage.  Reduces current health & calls Death() when 0.</para>
    /// <para>Should be overridden for anything beyond that basic functionality (i.e. audio, animation, etc.)</para></summary>
    /// <param name="damageToTake">Amount of damage to take.</param>
    public virtual void ReceiveDamage(int damageToTake)
    {
        CurrentHealth -= damageToTake;
        if (CurrentHealth <= 0)
            Death();
    }
    ///<summary>Overrideable function for dying.  Removes alive status and sets character on dead character layer.</summary>
    public virtual void Death()
    {
        if (IsAlive)
        {
            IsAlive = false;
            gameObject.layer = LayerMask.NameToLayer("Dead Characters");
        }
    }
    public virtual void GiveDamage(int damageToGive, GameObject target)
    {
    }
    public virtual void Kill(GameObject target)
    {

    }
}
