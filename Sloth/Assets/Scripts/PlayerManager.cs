using UnityEngine;
using System;
#if UNITY_EDITOR
using UnityEditor;
#endif


/// <summary>Manager class for player behavior in response to player and game interaction.</summary>
public class PlayerManager : MonoBehaviour {
    [Header("Player Stats")]

    [Tooltip("Maximum Health")][SerializeField]
    private int totalLives;

    [Tooltip("Maximum Health")][SerializeField]
    private int maximumHealth;

    [Tooltip("Health Drain per seconds")][SerializeField]
    private float healthDrain;

    [Tooltip("Maximum Energy")][SerializeField]
    private int maximumEnergy;

    [Tooltip("How fast player runs.")][SerializeField]
    private float movementSpeed;

    [Tooltip("How fast player climbs.")][SerializeField]
    private float climbSpeed;

    [Tooltip("Power of player jump.")][SerializeField]
    private float jumpStrength;

    [Tooltip("Attack radius--Generally should be larger than enemy bite-radius.")]
    [SerializeField]
    [Range(0.0f, 2.0f)]
    private float attackRadius = 1.5f;

    [Tooltip("Attack delay in seconds--Generally should be larger than enemy bite-delay.")]
    [SerializeField]
    private float attackDelay = 1.0f;

    [Space(10.0f)]
    [Header("Logic Settings")]

    [Tooltip("Size of Raycast to use during 'ground check' for jumping, etc.")]
    [SerializeField]
    private float distanceOfGroundCheck = 1.0f;

    /// <summary>Reference to player's Rigidbody2D component.</summary>
    private Rigidbody2D rb;
   
    /// <summary>Reference to players animator.</summary>
    private Animator animator;

    /// <summary>Reference to player's Sprite Renderer component.</summary>
    private SpriteRenderer sprite;
    
    /// <summary>Any boosts from a pick-up will be stored here.</summary>
    private BoostPickUp boostFromPickUp;
    
    /// <summary>Flag - Is Player In Front of object with Ladder Tag?</summary>
    private bool inFrontOfLadder = false;
    
    /// <summary>Player's current health.</summary>
    private float currentHealth;
    
    /// <summary>Player's current energy</summary>
    private float currentEnergy;

    /// <summary>Players remaining lives.</summary>
    private int remainingLives;

    /// <summary> Reference to singleton instance of PlayerManager class.</summary>
	private static PlayerManager singleton;
    
    ///<summary>Reference to current gravity scale.</summary>
    private float currentGravityScale;
    
    ///<summary>Reference to walking gravity scale.</summary>
    private float walkingGravityScale;
    
    /// <summary>Flag for player jumping behavior. </summary>
    private bool tryingToJump = false;

    /// <summary>Is player falling?</summary>
    private bool isFalling = false;

    /// <summary>Is player attacking?</summary>
    private bool isAttacking = false;

    /// <summary>Reference to the starting point for the level.</summary>
    private Vector3 spawnPoint;

    /// <summary>Reference to the time in seconds when player last attacked</summary>
    private float lastAttackTime;


    /// <summary>Get PlayerManager singelton.</summary>
	public static PlayerManager Singleton {
		get {
			if (singleton == null) {
				singleton = FindObjectOfType<PlayerManager> ();
			}
			return singleton;
		}
	}
    
    /// <summary>Get player's maximum health.</summary>
    public int MaximumHealth
    {
        get
        {
            return maximumHealth;
        }

        private set
        {
            maximumHealth = value;
        }
    }
    
    /// <summary>Get player's maximum energy.</summary>
    public int MaximumEnergy
    {
        get
        {
            return maximumEnergy;
        }

        set
        {
            maximumEnergy = value;
        }
    }
    
    /// <summary>Get player's current health.</summary>
    public float CurrentHealth
    {
        get
        {
            return currentHealth;
        }

        private set
        {
            currentHealth = value;
        }
    }
    
    /// <summary>Get player's current energy</summary>
    public float CurrentEnergy
    {
        get
        {
            return currentEnergy;
        }

        private set
        {
            currentEnergy = value;
        }
    }

    /// <summary>Accessor for object's Rigidbody2D reference.</summary>
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

    /// <summary>Accessor for object's "current" gravity scale.</summary>
    public float CurrentGravityScale
    {
        get
        {
            return currentGravityScale;
        }

        private set
        {
            currentGravityScale = value;
        }
    }

    /// <summary>Accessor for object's "walking" gravity scale.</summary>
    public float WalkingGravityScale
    {
        get
        {
            return walkingGravityScale;
        }

        private set
        {
            walkingGravityScale = value;
        }
    }

    /// <summary>Accessor to determine player jump state.</summary>
    public bool TryingToJump
    {
        get
        {
            return tryingToJump;
        }

        private set
        {
            tryingToJump = value;
        }
    }

    /// <summary>Accessor to player's total lives.</summary>
    public int TotalLives
    {
        get
        {
            return totalLives;
        }

        private set
        {
            totalLives = value;
        }
    }

    /// <summary>Accessor to player's remaining lives.</summary>
    public int RemainingLives
    {
        get
        {
            return remainingLives;
        }

        private set
        {
            remainingLives = value;
        }
    }

    /// <summary>Accessor of the spawn point used by the player this level.</summary>
    public Vector3 SpawnPoint
    {
        get
        {
            return spawnPoint;
        }

        private set
        {
            spawnPoint = value;
        }
    }

    /// <summary>Accessor to determine if player is falling.</summary>
    public bool IsFalling
    {
        get
        {
            return isFalling;
        }

        private set
        {
            isFalling = value;
        }
    }

     /// <summary>Accessor to determine if player is in front of a ladder</summary>
    public bool InFrontOfLadder
    {
        get
        {
            return inFrontOfLadder;
        }

        private set
        {
            inFrontOfLadder = value;
        }
    }

    public bool IsAttacking
    {
        get
        {
            return isAttacking;
        }

        private set
        {
            isAttacking = value;
        }
    }

    public float AttackRadius
    {
        get
        {
            return attackRadius;
        }

        private set
        {
            attackRadius = value;
        }
    }

    public float AttackDelay
    {
        get
        {
            return attackDelay;
        }

        private set
        {
            attackDelay = value;
        }
    }

    public float LastAttackTime
    {
        get
        {
            return lastAttackTime;
        }

        private set
        {
            lastAttackTime = value;
        }
    }





    /// <summary>Applies pickup boost to player, and sets duration.</summary>
    /// <param name="modsFromPickUp"></param>
    public void ApplyPickUpBoost(BoostPickUp pickUp)
    {
        //Save pick-up data to the player's boost info.
        boostFromPickUp = pickUp;

        //Calculate the compensation needed on certain player boosts to compensate for the time scalar. Then scale time.
        float compensatorForTimeScale = 1.0f / boostFromPickUp.TimeScalar;
        Time.timeScale = boostFromPickUp.TimeScalar;

        //Apply all the boosts from the pick-up based off the stored boost info and the compensator.
        CurrentGravityScale = RB.gravityScale /= boostFromPickUp.GravityReductionDivisor;       //Need to store CurrentGravity as well so we can freely manipulate gravity during ladder climbing without interfering with the boost mechanic.
        movementSpeed *= boostFromPickUp.MovementSpeedMultiplier * compensatorForTimeScale;
        climbSpeed *= boostFromPickUp.ClimbSpeedMultiplier * compensatorForTimeScale;
        jumpStrength *= boostFromPickUp.JumpStrengthMultiplier;

        //Add boost energy to player up to maximum energy levels.
        CurrentEnergy += boostFromPickUp.EnergyFromBoost;
        if (CurrentEnergy > MaximumEnergy)
            CurrentEnergy = MaximumEnergy;

        //Add health boost to player up to maximum health level, and resize the health bar accordingly.
        CurrentHealth += boostFromPickUp.HealthFromBoost;
        if (CurrentHealth > MaximumHealth)
            CurrentHealth = MaximumHealth;
        HUDManager.Singleton.ResizeHealthBar();
        
        //Adjust animation speed to compensate for the time scaling operations.
        animator.speed *= boostFromPickUp.MovementSpeedMultiplier * compensatorForTimeScale;
    }

    /// <summary>Removes pickup boost modifications to player.</summary>
	public void RemovePickUpBoost()
    {
        //Set energy to 0, in case less than.
        currentEnergy = 0;

        //Calculate the compensation needed based on modified time scalar..  Then set scalar to default.
        float compensatorForTimeScale = boostFromPickUp.TimeScalar / 1.0f;
        Time.timeScale = 1.0f;

        //Remove all boosts from the pick-up, based off the stored boost info and the compensator.
        CurrentGravityScale = WalkingGravityScale;                                //REVIEW: This may cause issues when running out of Slothbux on ladders?
        if (RB.gravityScale != 0.0f)                                              //Check later to see if this cures the issue.      
            RB.gravityScale = WalkingGravityScale;
        movementSpeed /= boostFromPickUp.MovementSpeedMultiplier / compensatorForTimeScale;
        climbSpeed /= boostFromPickUp.ClimbSpeedMultiplier / compensatorForTimeScale;
        jumpStrength /= boostFromPickUp.JumpStrengthMultiplier;

        // TEST
        animator.speed /= boostFromPickUp.MovementSpeedMultiplier / compensatorForTimeScale;
    }

    /// <summary>Applies damage to player.  </summary>
    /// <param name="amountOfDamage">Amount of damage to give.</param>
	public void DamagePlayer(int amountOfDamage)
    {
        CurrentHealth -= amountOfDamage;
        AudioManager.Singleton.PlayerHurt();
        HUDManager.Singleton.ResizeHealthBar();
        if (CurrentHealth <= 0)
        {
            PlayerDies();
        }
    }

    // TODO: Fix this.  It's very sloppy execution.
    /// <summary>When player dies, remove a life and restart.  If no lives left, end game!</summary>
    private void PlayerDies()
    {
        if (RemainingLives > 0)
        {
            RemainingLives--;
            RespawnPlayer();
        }
        else
        {
            LevelManager.Singleton.GameOverWrapper();
            Destroy(this.gameObject);
        }
    }

    /// <summary>Respawn player at start point with max health.</summary>
    private void RespawnPlayer()
    {
        HUDManager.Singleton.UpdateLivesLeftHUD();
        CurrentHealth = MaximumHealth;
        HUDManager.Singleton.ResizeHealthBar();
        CurrentEnergy = 0;
        HUDManager.Singleton.ResizeEnergyBar();
        gameObject.transform.position = SpawnPoint;
    }

    /// <summary>Early set-up. Establish singleton, initialize health and get Rigidbody2D reference.</summary>
    private void Awake()
    {
        InitializeSingleton();
        SetPlayerComponentReferences();
        InitializePlayerStats();
    }

    /// <summary>Set references to necessary components on player object.</summary>
    private void SetPlayerComponentReferences()
    {
        //Get Rigidbody2D w/ error check
        RB = GetComponent<Rigidbody2D>();
        if (RB == null)
            print("Can't find player's Rigidbody2D component!");

        //Get Animator w/ error check
        animator = GetComponent<Animator>();
        if (animator == null)
            print("Can't find player's Animator component!");

        //Get SpriteRenderer w/ error check
        sprite = GetComponent<SpriteRenderer>();
        if (animator == null)
            print("Can't find player's SpriteRenderer component!");
    }

    /// <summary>Initialize all values needed for proper player function.</summary>
    private void InitializePlayerStats()
    {
        //Set current health w/ error check
        RemainingLives = TotalLives;
        CurrentHealth = MaximumHealth;
        if (CurrentHealth == 0)
            print("Player's maximum health has not been set!");
        CurrentEnergy = 0;
        WalkingGravityScale = RB.gravityScale;
        CurrentGravityScale = WalkingGravityScale;
        SpawnPoint = gameObject.transform.position;
    }

    /// <summary>Initialize the Singleton object.</summary>
    private void InitializeSingleton()
    {
        //Establish singleton
        if (singleton == null)
        {
            singleton = this;
        }
        else if (singleton != this)
        {
            Destroy(gameObject);
        }
    }

    /// <summary>Performs movements and animations.</summary>
    private void FixedUpdate()
    {
        //Applies movement based on input, makes sprite face direction of movement, and sends animator data to use movement animation.
        WalkingMovement();

        //BUGGED: Can climb up even when not properly aligned.  Consider a "vertical alignment" check.
        //BUGGED: Tries to climb even at the top of the ladder.  Maybe add edge colliders?
        //If in front of a ladder, gets vertical movement input and allows for climbing w/ animation.  If not climbing, fall and don't use climb animation.
        if (InFrontOfLadder)
        {
            if (InFrontOfLadder)
            {
                ClimbingMovement();
            }
            else
            {
                RB.velocity = new Vector2(RB.velocity.x, 0.0f);
            }
        }

        if (TryingToJump)
        {
            Jump();
        }
        if ((IsPlayerOnGround() == false) && (RB.velocity.y < 0.0f) && (IsFalling == false))
        {
            IsFalling = true;
            animator.SetBool("isFalling", true);
        }

        if ((IsPlayerOnGround() == true) && (IsFalling == true))
        {
            IsFalling = false;
            animator.SetBool("isFalling", false);
        }

        if ((RB.velocity.y == 0f) && IsAttacking == true)
        {
            Attack();
        }
        else
        {
            IsAttacking = false;
        }
    }

    private void Attack()
    {
        IsAttacking = false;
        animator.SetTrigger("attacked");
        if (Time.realtimeSinceStartup >= (LastAttackTime + AttackDelay))
        {
            LastAttackTime = Time.realtimeSinceStartup;
            int LayerToHit = 1 << 11;
            Collider2D target = new Collider2D();
            if ((target = Physics2D.OverlapCircle(gameObject.transform.position, AttackRadius, LayerToHit)) != null)
            {
                AudioManager.Singleton.PlayerAttack();
                target.gameObject.GetComponent<EnemyManager>().DamageEnemy(0);
            }
        }
    }

    private void ClimbingMovement()
    {
        float playerClimbSpeed = Input.GetAxis("Vertical");
        if (playerClimbSpeed != 0.0f)
        {
            animator.SetFloat("playerClimbSpeed", playerClimbSpeed);
        }
        else
        {
            animator.SetFloat("playerClimbSpeed", 0.0f);
            RB.velocity = new Vector2(RB.velocity.x, 0.0f);
        }
        RB.velocity = new Vector2(RB.velocity.x, climbSpeed * playerClimbSpeed);
    }

    private void WalkingMovement()
    {
        float playerMovement = Input.GetAxis("Horizontal");
        if (playerMovement > 0.0f && sprite.flipX == false)
            sprite.flipX = true;
        if (playerMovement < 0.0f && sprite.flipX == true)
            sprite.flipX = false;
        animator.SetFloat("playerSpeed", Mathf.Abs(playerMovement));
        RB.velocity = new Vector2(playerMovement * movementSpeed, RB.velocity.y);

		if (playerMovement != 0.0f) 
			AudioManager.Singleton.PlayerWalking (true);
		if (playerMovement == 0.0f)
			AudioManager.Singleton.PlayerWalking (false);

    }

    private void Jump()
    {
        TryingToJump = false;
        if (IsPlayerOnGround())
        {
            RB.velocity = new Vector2(RB.velocity.x, jumpStrength);
            animator.SetTrigger("jumped");
            AudioManager.Singleton.PlayerJump();
        }
    }

    /// <summary>Per-frame update information. </summary>
	private void Update()
    {
        DrainHealth();
        HUDManager.Singleton.ResizeHealthBar();
        DrainEnergy();
        HUDManager.Singleton.ResizeEnergyBar();

        if (Input.GetKeyDown(KeyCode.Space))
            TryingToJump = true;

        if (Input.GetKeyDown(KeyCode.E))
            IsAttacking = true;
    }

    /// <summary>Drain health over time. If 0, player dies.</summary>
    private void DrainHealth()
    {
        CurrentHealth -= Time.deltaTime * healthDrain;
        if (CurrentHealth <= 0.0f)
        {
            PlayerDies();
        }
    }

    /// <summary>Detects if player entered "Ladder" trigger, and let them climb.</summary>
    /// <param name="collision">object collided with.</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ladder"))
        {
            RB.gravityScale = 0.0f;
            
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ladder"))
        {
            if (collision.OverlapPoint(RB.position))
            {
                InFrontOfLadder = true;
                print("In front of ladder");
            }
            else
            {
                InFrontOfLadder = false;
                print("Not in front of ladder");
            }
        }
    }
    /// <summary>Detects if player left "Ladder" trigger, and prevent climbing.</summary>
    /// <param name="collision">object collided with.</param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ladder"))
        {
            RB.gravityScale = CurrentGravityScale;
            animator.SetFloat("playerClimbSpeed", 0.0f);
        }
    }
    
    /// <summary>If player is boosted, reduce counter, removing boost when appropriate.</summary>
    private void DrainEnergy()
    {
        if (currentEnergy > 0.0f)
        {
            currentEnergy -= Time.deltaTime;
            if (currentEnergy <= 0.0f)
            {
                RemovePickUpBoost();
            }
        }
    }

    /// <summary>Perform a ray-cast to determine if player is on the ground.</summary>
    private bool IsPlayerOnGround()
    {
        
        if (Physics2D.Raycast(transform.position, Vector2.down, distanceOfGroundCheck, ~(1 << 8)))
            return true;
        else
            return false;
    }

#if UNITY_EDITOR 
    /// <summary>Draw "level-editor" gizmo settings in editor scene view.</summary>
    private void OnDrawGizmos()
    {
        Handles.color = Color.blue;
        Handles.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y - distanceOfGroundCheck, transform.position.z));
        Handles.Label(new Vector3(transform.position.x + 0.1f, transform.position.y - (distanceOfGroundCheck / 2.0f), transform.position.z), new GUIContent("Distance of Ground-check Ray", "Distance of Ground-check Ray"));
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(gameObject.transform.position, AttackRadius);
    }
#endif
}