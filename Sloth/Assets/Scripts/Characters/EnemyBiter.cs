using System.Collections;
using UnityEngine;


/// <summary>Enemy that remains stationary and bites when player is close enough.</summary>
public class EnemyBiter : Enemy, ICanBite
{
    [Space(10)][Header("Biter Stats")]
    [Tooltip("Enemy bite-attack radius")][SerializeField][Range(0.0f, 1.0f)]
    protected float biteRadius = 1.0f;
    [Tooltip("Enemy bite-attack delay (in seconds)")][SerializeField]
    protected float biteDelay = 2.0f;
    [Tooltip("Enemy bite-attack damage")][SerializeField]
    protected float biteScalar = 1.0f; //LATER:  Implement scaling damage on EnemyBiter





    /// <summary>Accessor for enemy bite radius.</summary>
    public float BiteRadius
    {
        get
        {
            return biteRadius;
        }

        set
        {
            biteRadius = value;
        }
    }
    /// <summary>Accessor for delay (in seconds) of enemy bite.</summary>
    public float BiteDelay
    {
        get
        {
            return biteDelay;
        }

        set
        {
            biteDelay = value;
        }
    }
    /// <summary>Accessor for bite damage scalar.</summary>
    public float ScaledBiteDamage
    {
        get
        {
            return biteScalar * BaseDamage;
        }

        set
        {
            biteScalar = value;
        }
    }




 
    /// <summary>Editor-specific functionality.</summary>
    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        ShowBiteInEditor();
    }
    /// <summary>Receive damage.</summary>
    public override void ReceiveDamage(int damageToTake)
    {
        base.ReceiveDamage(damageToTake);
    }
    /// <summary>Die.</summary>
    public override void Death()
    {
        base.Death();
    }





    /// <summary>Show bite radius in editor.</summary>
    private void ShowBiteInEditor()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(gameObject.transform.position, BiteRadius);
    }
}
