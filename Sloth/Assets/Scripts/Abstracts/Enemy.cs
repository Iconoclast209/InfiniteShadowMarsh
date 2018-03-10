using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary><para>Base class for all combative enemies.  All enemies should have this attached!</para>
/// <para>Inherit from this class and add behaviours.</para>
/// </summary>
public abstract class Enemy : CombativeCharacter
{
    [Space(10)][Header("Basic Enemy Settings")]
    [Tooltip("Default State of Enemy")][SerializeField]
    private State currentState;
    [Tooltip("State enemy should remain in -- DO NOT USE")][SerializeField]
    private State remainInState;
    /// <summary>Reference to enemy's Rigidbody2D component.</summary>
    private Rigidbody2D rb;
    /// <summary>Reference to enemy's BoxCollider2D component.</summary>
    private CapsuleCollider2D cc;
    /// <summary>Reference to enemy's animator.</summary>
    private Animator animator;
    /// <summary>Reference to enemy's sprite renderer.</summary>
    private SpriteRenderer spriteRenderer;
    /// <summary>Reference to time (in seconds) of enemy's previous attack.</summary>
    protected float lastAttackTime;





    /// <summary>Accessor for Rigidbody2D component</summary>
    public Rigidbody2D RB
    {
        get
        {
            return rb;
        }
        private set
        {
            rb = value;
        }
    }
    /// <summary>Accessor for CapsuleCollider2D component</summary>
    public CapsuleCollider2D CC
    {
        get
        {
            return cc;
        }

        private set
        {
            cc = value;
        }
    }
    /// <summary>Accessor for Animator component</summary>
    public Animator Animator
    {
        get
        {
            return animator;
        }

        private set
        {
            animator = value;
        }
    }
    /// <summary>Accessor for SpriteRenderer component</summary>
    public SpriteRenderer SpriteRenderer
    {
        get
        {
            return spriteRenderer;
        }

        private set
        {
            spriteRenderer = value;
        }
    }
    /// <summary>Accessor for time of enemy's last attack.</summary>
    public float LastAttackTime
    {
        get
        {
            return lastAttackTime;
        }

        set
        {
            lastAttackTime = value;
        }
    }


    /// <summary>Early set-up.  Internal.</summary>
   public override void Awake()
    {
        // TODO: Add name HUD display on enemies
        //GameObject demoText = new GameObject("Enemy Name");
        //Text textComp = demoText.AddComponent<Text>();
        //textComp.text = "DEMO!";
        //demoText.transform.SetParent(FindObjectOfType<Canvas>().transform, false);
        base.Awake();
        ValidateVariables();
    }
    /// <summary>Frame-independent update. Handles AI state updates for enemy.</summary>
    private void FixedUpdate()
    {
        if (IsAlive == true)
        {
            currentState.UpdateState(this);
        }
    }
    /// <summary>Validates all base enemy variable settings.</summary>
    private void ValidateVariables()
    {

        RB = GetComponent<Rigidbody2D>();
        CC = GetComponent<CapsuleCollider2D>();
        Animator = GetComponent<Animator>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
        
        if (RB == null)
            print(gameObject.name + " does not have a Rigidbody2D component.");
        if (CC == null)
            print(gameObject.name + " does not have a BoxCollider2D component.");
        if (Animator == null)
            print(gameObject.name + " does not have an Animator component.");
        if (SpriteRenderer == null)
            print(gameObject.name + " does not have a SpriteRenderer component.");
    }
    /// <summary>Destroys enemy object after seconds.</summary>
    /// <param name="seconds">Seconds to wait before destroying enemy object.</param>
    private IEnumerator DieAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }
    /// <summary>Transition to new AI state</summary>
    /// <param name="nextState">State to transition into.</param>
    public void TransitionToState(State nextState)
    {
        if (nextState != remainInState)
        {
            currentState = nextState;
        }
    }





    /// <summary>Make enemy receive damage.</summary>
    /// <param name="damageToTake">Damage to give to enemy.</param>
    override public void ReceiveDamage(int damageToTake)
    {
        base.ReceiveDamage(damageToTake);
    }
    /// <summary>Make enemy die.</summary>
    override public void Death()
    {
        base.Death();
        Animator.SetTrigger("triggerDeath");
        AudioManager.Singleton.EnemyDeath();
        StartCoroutine(DieAfterSeconds(2.0f));
    }
}
