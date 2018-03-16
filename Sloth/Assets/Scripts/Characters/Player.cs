using UnityEngine;
using System;
#if UNITY_EDITOR
using UnityEditor;
#endif


/// <summary>Manager class for player behavior in response to player and game interaction.</summary>
public class Player : CombativeCharacter {
    [Space(10)][Header("Player-Specific Stats")]
    [Tooltip("Maximum Health")][SerializeField]
    private int totalLives;
    [Tooltip("Health Drain per seconds")][SerializeField]
    private float healthDrain;
    [Tooltip("Maximum Energy")][SerializeField]
    private int maximumEnergy;
    [Tooltip("How fast player climbs.")][SerializeField]
    private float climbSpeed;
    [Tooltip("Power of player jump.")][SerializeField]
    private float jumpStrength;
    [Tooltip("Attack radius--Generally should be larger than enemy bite-radius.")][SerializeField][Range(0.0f, 2.0f)]
    private float attackRadius = 1.5f;
    [Tooltip("Attack delay in seconds--Generally should be larger than enemy bite-delay.")][SerializeField]
    private float attackDelay = 1.0f;
    [Tooltip("Interaction delay in seconds.")][SerializeField]
    private float interactionDelay = 1.0f;
    [Space(10.0f)][Header("Logic Settings")]
    [Tooltip("Size of Raycast to use during 'ground check' for jumping, etc.")][SerializeField]
    private float distanceOfGroundCheck = 1.0f;
    /// <summary>Reference to player's Rigidbody2D component.</summary>
    private Rigidbody2D rb;
    /// <summary>Reference to players animator.</summary>
    private Animator animator;
    /// <summary>Reference to player's Sprite Renderer component.</summary>
    private SpriteRenderer sprite;
    /// <summary>Any boosts from a pick-up will be stored here.</summary>
    private BoostPickUp boostFromPickUp;
    /// <summary>Player's current energy</summary>
    private float currentEnergy;
    /// <summary>Players remaining lives.</summary>
    private int remainingLives;
    /// <summary> Reference to singleton instance of PlayerManager class.</summary>
	private static Player singleton;
    ///<summary>Reference to current gravity scale.</summary>
    private float currentGravityScale;
    ///<summary>Reference to walking gravity scale.</summary>
    private float walkingGravityScale;
    /// <summary>Reference to the starting point for the level.</summary>
    private Vector3 spawnPoint;
    /// <summary>Reference to the time in seconds when player last attacked</summary>
    private float lastAttackTime;
    private float lastInteractionTime;






    /// <summary>Get PlayerManager singelton.</summary>
	public static Player Singleton {
		get {
			if (singleton == null) {
				singleton = FindObjectOfType<Player> ();
			}
			return singleton;
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

        set
        {
            spawnPoint = value;
        }
    }
    /// <summary>Accessor for attack radius</summary>
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
    /// <summary>Accessor for delay between attacks</summary>
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
    /// <summary>Accessor for time of players last attack</summary>
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
    public float LastInteractionTime
    {
        get
        {
            return lastInteractionTime;
        }
        private set
        {
            lastInteractionTime = value;
        }
    }
    public float InteractionDelay
    {
        get
        {
            return interactionDelay;
        }
        private set
        {
            interactionDelay = value;
        }
    }





    // Standard Monobehaviour methods--Not commented because standard methods should be understood if modifying code.
    public override void Awake()
    {
        base.Awake();
        InitializeSingleton();
        SetPlayerComponentReferences();
        InitializePlayerStats();
    }
    private void Start()
    {
        // Make horizontal movement available at all times when OnWalkStart is called
        InputManager.OnWalkStart += UpdateHorizontalMovement;

        // Make animator reset at all times when OnWalkStop is called
        InputManager.OnWalkStop += UpdateWalkingAnimation;
        InputManager.OnWalkStop += ResetAnimatorSpeed;
        InputManager.OnClimbStop += UpdateClimbingAnimation;
        InputManager.OnClimbStop += ResetAnimatorSpeed;
    }
    private void OnDestroy()
    {
        // Remove horizontal movement on object destruction.
        InputManager.OnWalkStart -= UpdateHorizontalMovement;

        // Remove animator resetting on object destruction.
        InputManager.OnWalkStop -= UpdateWalkingAnimation;
        InputManager.OnWalkStop -= ResetAnimatorSpeed;
        InputManager.OnClimbStop -= UpdateClimbingAnimation;
        InputManager.OnClimbStop -= ResetAnimatorSpeed;
    }
	private void Update()
    {
        //Update life & energy w/ HUD
        HUDManager.Singleton.ResizeHealthBar();
        DrainEnergy();
        HUDManager.Singleton.ResizeEnergyBar();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //If player is entering a climbable object trigger, remove gravity and add climbing movement to event.
        if (collision.gameObject.CompareTag("Climbable Object"))
        {
            RemoveGravityForce();
            InputManager.OnClimbStart += UpdateVerticalMovement;
            InputManager.OnClimbStart += UpdateClimbingAnimation;
            InputManager.OnClimbStop += UpdateVerticalMovement;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //If player is exiting a climbable object trigger, apply gravity and remove climbing movement from event.
        if (collision.gameObject.CompareTag("Climbable Object"))
        {
            ApplyGravityForce();
            InputManager.OnClimbStart -= UpdateVerticalMovement;
            InputManager.OnClimbStart -= UpdateClimbingAnimation;
            InputManager.OnClimbStop -= UpdateVerticalMovement;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //If contacting the ground, allow jump and attack mechanics and allow walking animation
        if (collision.gameObject.CompareTag("Walkable Ground"))         
        {
            InputManager.OnJump += Jump; 
            InputManager.OnAttack += Attack;
            InputManager.OnInteract += Interact;
            InputManager.OnWalkStart += UpdateWalkingAnimation;
        }
    }



    private void OnCollisionExit2D(Collision2D collision)
    {
        //If not on the ground, disallow jump and attack mechanics and walking animation
        if (collision.gameObject.CompareTag("Walkable Ground"))         
        {
            InputManager.OnJump -= Jump;
            InputManager.OnAttack -= Attack;
            InputManager.OnWalkStart -= UpdateWalkingAnimation;
        }
    }





    /// <summary>Applies pickup boost to player, and sets duration.</summary>
    /// <param name="pickUp">Boost amounts from pick-up.</param>
    public void ApplyPickUpBoost(BoostPickUp pickUp)
    {

        //Only boost stats if not already boosted.
        if (CurrentEnergy == 0)
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

            //Adjust animation speed to compensate for the time scaling operations.
            animator.speed *= boostFromPickUp.MovementSpeedMultiplier * compensatorForTimeScale;
        }

        //Add boost energy to player up to maximum energy levels.
        CurrentEnergy += boostFromPickUp.EnergyFromBoost;
        if (CurrentEnergy > MaximumEnergy)
            CurrentEnergy = MaximumEnergy;

        //Add health boost to player up to maximum health level, and resize the health bar accordingly.
        CurrentHealth += boostFromPickUp.HealthFromBoost;
        if (CurrentHealth > StartingHealth)
            CurrentHealth = StartingHealth;
        HUDManager.Singleton.ResizeHealthBar();
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
    /// <summary>Respawn player at start point with max health.</summary>
    private void RespawnPlayer()
    {
        HUDManager.Singleton.UpdateLivesLeftHUD();
        CurrentHealth = StartingHealth;
        HUDManager.Singleton.ResizeHealthBar();
        CurrentEnergy = 0;
        HUDManager.Singleton.ResizeEnergyBar();
        gameObject.transform.position = SpawnPoint;
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
    /// <summary>Set starting values needed for proper player function.</summary>
    private void InitializePlayerStats()
    {
        //Set current health w/ error check
        RemainingLives = TotalLives;
        CurrentEnergy = 0;
        WalkingGravityScale = RB.gravityScale;
        CurrentGravityScale = WalkingGravityScale;
        SpawnPoint = gameObject.transform.position;
    }
    /// <summary>Start up the Singleton object.</summary>
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

    private void Interact()
    {
        if (Time.realtimeSinceStartup >= (LastInteractionTime + InteractionDelay))
        {
            LastInteractionTime = Time.realtimeSinceStartup;
            int LayerToHit = 1 << 9;
            Collider2D target = new Collider2D();
            if ((target = Physics2D.OverlapCircle(gameObject.transform.position, AttackRadius, LayerToHit)) != null)
            {
                if (target.GetComponent<IInteractable>() != null)
                    target.GetComponent<IInteractable>().OnInteraction();
            }
        }
    }
    /// <summary>Primary attack function.</summary>
    private void Attack()
    {
        animator.SetTrigger("attacked");
        if (Time.realtimeSinceStartup >= (LastAttackTime + AttackDelay))
        {
            LastAttackTime = Time.realtimeSinceStartup;
            int LayerToHit = 1 << 11;
            Collider2D target = new Collider2D();
            if ((target = Physics2D.OverlapCircle(gameObject.transform.position, AttackRadius, LayerToHit)) != null)
            {
                AudioManager.Singleton.PlayerAttack();
                // TODO: Implement the IDamageable and IDamaging methods.  
                //GiveDamage(BaseDamage, target.gameObject); 
                target.gameObject.GetComponent<Enemy>().ReceiveDamage(BaseDamage);
            }
        }
    }
    /// <summary>Primary climbing function.</summary>
    private void UpdateVerticalMovement()
    {
        float playerClimbSpeed = Input.GetAxis("Vertical");
        UpdateClimbingAnimation();
        RB.velocity = new Vector2(RB.velocity.x, climbSpeed * playerClimbSpeed);
    }
    /// <summary>Keep animator updated for climbing transition.</summary>
    private void UpdateClimbingAnimation()
    {
        float playerClimbSpeed = Input.GetAxis("Vertical");
        animator.SetFloat("playerClimbSpeed", playerClimbSpeed);
        animator.speed = Mathf.Abs(playerClimbSpeed);
    }
    /// <summary>Primary movement function</summary>
    private void UpdateHorizontalMovement()
    {
        float playerMovement = Input.GetAxis("Horizontal");
        SetSpriteToFaceDirectionOfMovement(playerMovement);
        RB.velocity = new Vector2(playerMovement * movementSpeed, RB.velocity.y);

        if (playerMovement != 0.0f)
            AudioManager.Singleton.PlayerWalking(true);
        if (playerMovement == 0.0f)
            AudioManager.Singleton.PlayerWalking(false);
    }
    /// <summary>Keep animator updated for walking transitions.</summary>
    private void UpdateWalkingAnimation()
    {
        animator.SetFloat("playerSpeed", Mathf.Abs(Input.GetAxis("Horizontal")));
        animator.speed = Mathf.Abs(Input.GetAxis("Horizontal"));
    }
    /// <summary>Reset animator speed.</summary>
    private void ResetAnimatorSpeed()
    {
        animator.speed = 1.0f;
    }
    /// <summary>Turn sprite in proper direction.</summary>
    private void SetSpriteToFaceDirectionOfMovement(float playerMovement)
    {
        if (playerMovement > 0.0f && sprite.flipX == false)
            sprite.flipX = true;
        if (playerMovement < 0.0f && sprite.flipX == true)
            sprite.flipX = false;
    }
    /// <summary>Execute jump w/ animation & audio cue.</summary>
    private void Jump()
    {
        RB.velocity = new Vector2(RB.velocity.x, jumpStrength);
        animator.SetTrigger("jumped");
        AudioManager.Singleton.PlayerJump();
    }
    /// <summary>If player is boosted, reduce counter, removing boost when appropriate.</summary>
    private void DrainEnergy()
    {
        if (currentEnergy > 0.0f)
        {
            currentEnergy -= (1.0f * Time.deltaTime);
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
    /// <summary>Removes the gravity force from player's rigidbody (Used for climbing)</summary>
    private void RemoveGravityForce()
    {
        RB.gravityScale = 0.0f;
    }
    /// <summary>Restores gravity force to player's rigidbody. (Used for climbing)</summary>
    private void ApplyGravityForce()
    {
        RB.gravityScale = WalkingGravityScale;
    }
#if UNITY_EDITOR 
    /// <summary>Draw "level-editor" gizmo settings in editor scene view.</summary>
    override public void OnDrawGizmos()
    {
        Handles.color = Color.blue;
        Handles.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y - distanceOfGroundCheck, transform.position.z));
        Handles.Label(new Vector3(transform.position.x + 0.1f, transform.position.y - (distanceOfGroundCheck / 2.0f), transform.position.z), new GUIContent("Distance of Ground-check Ray", "Distance of Ground-check Ray"));
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(gameObject.transform.position, AttackRadius);
    }
#endif
    /// <summary>Base function + audio cue and HUD update.</summary>
    override public void ReceiveDamage(int damageToTake)
    {
        base.ReceiveDamage(damageToTake);
        AudioManager.Singleton.PlayerHurt();
        HUDManager.Singleton.ResizeHealthBar();
    }
    /// <summary>Base function + player-specific death mechanics.</summary>
    override public void Death()
    {
        base.Death();
        if (RemainingLives > 0)
        {
            RemainingLives--;
            if (CurrentEnergy > 0)
            {
                RemovePickUpBoost();
            }
            RespawnPlayer();
        }
        else
        {
            LevelManager.Singleton.GameOverWrapper();
            Destroy(this.gameObject);
        }
    }
}