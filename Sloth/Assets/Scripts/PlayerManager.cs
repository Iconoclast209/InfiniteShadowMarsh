using UnityEngine;

/// <summary>Manager class for player behavior in response to player and game interaction.</summary>
public class PlayerManager : MonoBehaviour {

	[Tooltip("Maximum Health")][SerializeField]
    private int maximumHealth;

    [Tooltip("Maximum Energy")][SerializeField]
    private int maximumEnergy;

    [Tooltip("How fast player runs.")][SerializeField]
    private float movementSpeed;

    [Tooltip("How fast player climbs.")][SerializeField]
    private float climbSpeed;

    [Tooltip("Power of player jump.")][SerializeField]
    private float jumpStrength;

    /// <summary>Reference to player's Rigidbody2D component.</summary>
    private Rigidbody2D rb;
    /// <summary>Reference to players animator.</summary>
    private Animator animator;
    /// <summary>Reference to player's Sprite Renderer component.</summary>
    private SpriteRenderer sprite;
    /// <summary>Any boosts from a pick-up will be stored here.</summary>
    private PickUpBoost boostFromPickUp;
    /// <summary>Flag - Is Player In Front of object with Ladder Tag?</summary>
    private bool inFrontOfLadder = false;
    /// <summary>Player's current health.</summary>
    private int currentHealth;
    /// <summary>Player's current energy</summary>
    private float currentEnergy;
    /// <summary> Reference to singleton instance of PlayerManager class.</summary>
	private static PlayerManager singleton;





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
    public int CurrentHealth
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
    
    /// <summary>Applies pickup boost to player, and sets duration.</summary>
    /// <param name="modsFromPickUp"></param>
    public void ApplyEnergyBoost(PickUpBoost PickUp)
    {
        //Save pick-up data to the player's boost info.
        boostFromPickUp = PickUp;

        //Calculate the compensation needed on certain player boosts to compensate for the time scalar. Then scale time.
        float compensatorForTimeScale = 1.0f / boostFromPickUp.TimeScalar;
        Time.timeScale = boostFromPickUp.TimeScalar;

        //Apply all the boosts from the pick-up based off the stored boost info and the compensator.
        rb.gravityScale /= boostFromPickUp.GravityReductionDivisor;
        movementSpeed *= boostFromPickUp.MovementSpeedMultiplier * compensatorForTimeScale;
        climbSpeed *= boostFromPickUp.ClimbSpeedMultiplier * compensatorForTimeScale;
        jumpStrength *= boostFromPickUp.JumpStrengthMultiplier;

        //Add boost energy to player up to maximum energy levels, and resize the energy bar accordingly.
        currentEnergy += boostFromPickUp.EnergyFromBoost;
        if (currentEnergy > MaximumEnergy)
            currentEnergy = MaximumEnergy;
    }

    /// <summary>Removes pickup boost modifications to player.</summary>
	public void RemoveEnergyBoost()
    {
        //Set energy to 0, in case less than.
        currentEnergy = 0;

        //Calculate the compensation needed based on modified time scalar..  Then set scalar to default.
        float compensatorForTimeScale = boostFromPickUp.TimeScalar / 1.0f;
        Time.timeScale = 1.0f;

        //Remove all boosts from the pick-up, based off the stored boost info and the compensator.
        rb.gravityScale *= boostFromPickUp.GravityReductionDivisor;
        movementSpeed /= boostFromPickUp.MovementSpeedMultiplier / compensatorForTimeScale;
        climbSpeed /= boostFromPickUp.ClimbSpeedMultiplier / compensatorForTimeScale;
        jumpStrength /= boostFromPickUp.JumpStrengthMultiplier;
    }

    /// <summary>Applies damage to player.  </summary>
    /// <param name="amountOfDamage">Amount of damage to give.</param>
	public void DamagePlayer(int amountOfDamage)
    {
        CurrentHealth -= amountOfDamage;
        HUDManager.Singleton.ResizeHealthBar();
        if (CurrentHealth <= 0)
        {
            //TODO: Add player died functionality
            Destroy(gameObject);
        }
    }





    /// <summary>Early set-up. Establish singleton, initialize health and get Rigidbody2D reference.</summary>
    private void Awake() {
        //Establish singleton
		if (singleton == null) {
			singleton = this;
		} else if (singleton != this) {
			Destroy (gameObject);
		}

        //Set current health w/ error check
		CurrentHealth = MaximumHealth;
        if (CurrentHealth == 0)
            print("Player's maximum health has not been set!");
        CurrentEnergy = 0;

        //Get Rigidbody2D w/ error check
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
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

    /// <summary>Performs movements and animations.</summary>
	private void FixedUpdate () {
        //TODO: Add falling functionality
        //PRONTO: Get animations to test these features!

        //Applies movement based on input, makes sprite face direction of movement, and sends animator data to use movement animation.
        float playerMovement = Input.GetAxis("Horizontal");
        if (playerMovement > 0.0f && sprite.flipX == false)
            sprite.flipX = true;
        if (playerMovement < 0.0f && sprite.flipX == true)
            sprite.flipX = false;
        animator.SetFloat("playerSpeed", Mathf.Abs(playerMovement));
        rb.velocity = new Vector2(playerMovement * movementSpeed, rb.velocity.y);

        //BUGGED: Can climb up even when not properly aligned.  Consider a "vertical alignment" check.
        //BUGGED: Tries to climb even at the top of the ladder.  Maybe add edge colliders?
        //If in front of a ladder, gets vertical movement input and allows for climbing w/ animation.  If not climbing, fall and don't use climb animation.
        if (inFrontOfLadder)
        {
            float playerClimbSpeed = Input.GetAxis("Vertical");
            if (playerClimbSpeed > 0.0f)
            {
                animator.SetFloat("playerClimbSpeed", Mathf.Abs(playerClimbSpeed));
            }
            else
            {
                animator.SetFloat("playerClimbSpeed", 0.0f);
            }
            rb.velocity = new Vector2(rb.velocity.x, climbSpeed * playerClimbSpeed);  
        } 

        //BUGGED: Can jump multiple times in a row, while in air.  Need to prevent this.
        //TODO: Tie in jumping w/ falling, etc.
        if(Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpStrength);
            animator.SetTrigger("jumped");
            animator.ResetTrigger("jumped");
        }
	}

    /// <summary>Per-frame update information. </summary>
	private void Update()
    {
        DrainEnergy();
        HUDManager.Singleton.ResizeEnergyBar();
    } 

    /// <summary>Detects if player entered "Ladder" trigger, and let them climb.</summary>
    /// <param name="collision">object collided with.</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ladder"))
        {
            inFrontOfLadder = true;
        }
    }

    /// <summary>Detects if player left "Ladder" trigger, and prevent climbing.</summary>
    /// <param name="collision">object collided with.</param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ladder"))
        {
            inFrontOfLadder = false;
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
                RemoveEnergyBoost();
            }
        }
    }
}