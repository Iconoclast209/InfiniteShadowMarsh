using System.Collections;
using UnityEngine;


/// <summary>
/// Manager class for enemy behavior.  Controls AI.
/// <para>All enemies should have this attached!</para>
/// <para>Do not code specific enemy behaviors (AI) here!  They belong in scriptable objects for modular design!</para>
/// </summary>
public class EnemyManager : MonoBehaviour
{
    [Header("Basic Enemy Stats")]
    [Tooltip("Enemy starting health.")]
    [SerializeField]
    private int startingHealth;

    [Tooltip("Patrol speed")]
    [SerializeField]
    private float movementSpeed = 2.0f;

    [Tooltip("Enemy patrol radius")]
    [SerializeField]
    [Range(0.0f, 10.0f)]
    private float patrolRadius = 5.0f;

    [Tooltip("Should enemy start moving left?")]
    [SerializeField]
    private bool startMovingLeft;

    [Tooltip("Enemy web-attack radius")]
    [SerializeField]
    [Range(0.0f, 10.0f)]
    private float webRadius = 5.0f;

    [Tooltip("Enemy web-attack delay (in seconds)")]
    [SerializeField]
    private float webDelay = 4.0f;

    [Tooltip("Enemy bite-attack radius")]
    [SerializeField]
    [Range(0.0f, 1.0f)]
    private float biteRadius = 1.0f;

    [Tooltip("Enemy bite-attack delay (in seconds)")]
    [SerializeField]
    private float biteDelay = 2.0f;

    [Tooltip("Enemy bite-attack damage")]
    [SerializeField]
    private int biteDamage = 10;
    
    [Tooltip("Default State of Enemy")]
    [SerializeField]
    private State currentState;
    public State remainInState;

    /// <summary>Current health of enemy.</summary>
    private int currentHealth;

    /// <summary>Reference to enemy's Rigidbody2D component.</summary>
    private Rigidbody2D rb;
    
    /// <summary>Reference to enemy's BoxCollider2D component.</summary>
    private BoxCollider2D bc;
    
    /// <summary>Reference to enemy's starting position.</summary>
    private float startingPosition;  
    
    /// <summary>Reference to time (in seconds) of enemy's previous attack.</summary>
    private float lastAttackTime;

    /// <summary>Reference to enemy's animator.</summary>
    private Animator animator;

    private bool isEnemyAlive = true;

    private SpriteRenderer spriteRenderer;

    ///<summary>Is enemy moving left?</summary><return>Returns true if yes.</return>
    public bool IsMovingLeft
    {
        get
        {
            return startMovingLeft;
        }

        set
        {
            startMovingLeft = value;
        }
    }

    public float PatrolRadius
    {
        get
        {
            return patrolRadius;
        }
    }

    public float StartingPosition
    {
        get
        {
            return startingPosition;
        }
        private set
        {
            startingPosition = value;
        }
    }

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

    public float MovementSpeed
    {
        get
        {
            return movementSpeed;
        }
    }

    public float BiteRadius
    {
        get
        {
            return biteRadius;
        }

        private set
        {
            biteRadius = value;
        }
    }

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

    public float BiteDelay
    {
        get
        {
            return biteDelay;
        }

        private set
        {
            biteDelay = value;
        }
    }

    public int BiteDamage
    {
        get
        {
            return biteDamage;
        }

        private set
        {
            biteDamage = value;
        }
    }

    public BoxCollider2D BC
    {
        get
        {
            return bc;
        }

        private set
        {
            bc = value;
        }
    }

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

    public bool IsEnemyAlive
    {
        get
        {
            return isEnemyAlive;
        }

        private set
        {
            isEnemyAlive = value;
        }
    }

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






    /// <summary>Damage enemy.  Kills enemy when appropriate.</summary>
    /// <param name="amountOfDamage">Amount of damage</param>
    public void DamageEnemy(int amountOfDamage)
    {
        currentHealth -= amountOfDamage;
        if (currentHealth <= 0)
        {
            KillEnemy();
        }
    }





    /// <summary>Kills enemy.</summary>
    private void KillEnemy()
    {

        IsEnemyAlive = false;
        Animator.SetTrigger("triggerDeath");
        StartCoroutine(DieAfterSeconds(2.0f));
    }

    /// <summary>Early set-up.  Internal.</summary>
    private void Awake()
    {
        ValidateVariables();
    }

    private void ValidateVariables()
    {
        currentHealth = startingHealth;
        StartingPosition = transform.position.x;
        RB = GetComponent<Rigidbody2D>();
        BC = GetComponent<BoxCollider2D>();
        Animator = GetComponent<Animator>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
        
        if (RB == null)
            print("Spider does not have a Rigidbody2D component.");
        if (BC == null)
            print("Spider does not have a BoxCollider2D component.");
        if (Animator == null)
            print("Spider does not have an Animator component.");
        if (SpriteRenderer == null)
            print("Spider does not have a SpriteRenderer component.");
    }

    /// <summary>Kill enemy on collision with enemy</summary>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && (IsEnemyAlive == true))
            KillEnemy();
    }

    /// <summary>Destroys enemy object after seconds.</summary>
    private IEnumerator DieAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        if (IsEnemyAlive == true)
        {
            currentState.UpdateState(this);
            if (startMovingLeft == false && RB.velocity.x > 0)
                SpriteRenderer.flipX = true;
            else if (startMovingLeft == true && RB.velocity.x < 0)
                SpriteRenderer.flipX = false;     
        }
    }


    private void OnDrawGizmos()
    {
        ShowPatrolInEditor();
        ShowWebInEditor();
        ShowBiteInEditor();
    }

    private void ShowPatrolInEditor()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(gameObject.transform.position + new Vector3(0.0f, 0.1f, 0.0f), gameObject.transform.position + new Vector3(PatrolRadius, 0.1f, 0f));
        Gizmos.DrawLine(gameObject.transform.position + new Vector3(0.0f, 0.1f, 0.0f), gameObject.transform.position + new Vector3(-PatrolRadius, 0.1f, 0f));
    }

    private void ShowWebInEditor()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(gameObject.transform.position, webRadius);
    }

    private void ShowBiteInEditor()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(gameObject.transform.position, BiteRadius);
    }

    public void TransitionToState(State nextState)
    {
        if (nextState != remainInState)
        {
            currentState = nextState;
        }
    }
}
