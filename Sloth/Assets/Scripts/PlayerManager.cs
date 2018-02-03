using UnityEngine;

/// <summary>
/// Manager class for player behavior in response to player and game interaction.
/// </summary>
public class PlayerManager : MonoBehaviour {

	[Tooltip("Maximum Player Health")]
	public int maximumHealth;
    [Tooltip("Player Speed")]
	public float movementSpeed;

    /// <summary>
    /// Pick Up Bonuses assigned to player.
    /// </summary>
	private PickUpBoost pickUpBonus;
    /// <summary>
    /// Reference to health bar HUD component.
    /// </summary>
    private HealthBar healthBar;
    /// <summary>
    /// Reference to player's Rigidbody2D component.
    /// </summary>
    private Rigidbody2D rb;
    /// <summary>
    /// Player's current health.
    /// </summary>
    private int currentHealth;
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

    /// <summary>
    /// Reference to singleton instance of PlayerManager class.
    /// </summary>
	private static PlayerManager singleton;
    /// <summary>
    /// Method to retrieve reference to singleton instance of PlayerManager class.
    /// </summary>
	public static PlayerManager Singleton {
		get {
			if (singleton == null) {
				singleton = FindObjectOfType<PlayerManager> ();
			}
			return singleton;
		}
	}



    /// <summary>
    /// Early set-up. Establish singleton, initialize health and get Rigidbody2D reference.
    /// </summary>
    private void Awake() {
        //Establish singleton
		if (singleton == null) {
			singleton = this;
		} else if (singleton != this) {
			Destroy (gameObject);
		}

        //Set current health w/ error check
		CurrentHealth = maximumHealth;
        if (CurrentHealth == 0)
            print("Player's maximum health has not been set!");

        //Get Rigidbody2D w/ error check
        rb = GetComponent<Rigidbody2D> ();
        if (rb == null)
            print("Can't find player's Rigidbody2D component!");
	}

    /// <summary>
    /// Set-up involving third-parties.
    /// <para>Must go here to ensure other objects have been created in the scene as well.</para>
    /// </summary>
	private void Start() {
        //TODO: Set up reference to health bar.
	} 

    /// <summary>
    /// Will hold any necessary physics related functionality.
    /// </summary>
	void FixedUpdate () {
		
	} 

    /// <summary>
    /// Per-frame update information. 
    /// </summary>
	void Update()
    {
        PickUpBoostMonitor();
    } 

    /// <summary>
    /// If player is boosted, reduce counter, removing boost when appropriate.
    /// </summary>
    private void PickUpBoostMonitor()
    {
        if (pickUpBonus.durationInSeconds > 0.0f)
        {
            pickUpBonus.durationInSeconds -= Time.deltaTime;
            if (pickUpBonus.durationInSeconds <= 0.0f)
            {
                RemovePickUpBoost();
            }
        }
    }

    /// <summary>
    /// Applies pickup boost to player, and sets duration.
    /// </summary>
    /// <param name="modsFromPickUp"></param>
    public void ApplyPickUpBoost(PickUpBoost modsFromPickUp)
	{
		if (pickUpBonus.durationInSeconds == 0.0f && modsFromPickUp.durationInSeconds > 0.0f) {
			//TODO: Add ApplyPickUpBoost functionality based on what pickupBoost will contain
		}
	}

    /// <summary>
    /// Removes pickup boost modifications to player.
    /// </summary>
	public void RemovePickUpBoost()
	{
        //TODO: Add RemovePickUpBoost functionality based on what pickupBoost will contain
		print ("Powering Down...");
	}

    /// <summary>
    /// Applies damage to player.  
    /// </summary>
    /// <param name="amountOfDamage">Amount of damage to give.</param>
	public void DamagePlayer(int amountOfDamage){
		CurrentHealth -= amountOfDamage;
        //UNDONE: Don't replace until health bar reference is set up.
        //healthBar.ResizeHealthBar();
		if (CurrentHealth <= 0) {
            //TODO: Add player died functionality
			Destroy (gameObject);
		}
	}
}