using UnityEngine;


/// <summary>
/// Controller class for basic enemy behavior such as movement, pick-up spawning.
/// <para>All enemies should have this attached!</para>
/// <para>Only standard enemy behavior goes here!  All enemy-specific functionality should be placed in its own script!</para>
/// </summary>
public class EnemyManager : MonoBehaviour
{
    [Tooltip("Set enemy Health. Mobs: 5-50; Bosses: >50")]
    public int startingHealth;
    //UNDONE: Removed until we review enemy pick-ups.
    //[Tooltip("List of possible Game-Objects to spawn when this object is eliminated")]
    //public GameObject[] possiblePickUps;
    //[Tooltip("Likelihood to spawn a pick-up.  0.0 to 1.0")]
    //public float pickUpChance;

    /// <summary>
    /// Current health of enemy.
    /// </summary>
    private int currentHealth;

    /// <summary>
    /// Damage enemy.  Kills enemy when appropriate.
    /// </summary>
    /// <param name="amountOfDamage">Amount of damage</param>
    public void DamageEnemy(int amountOfDamage)
    {
        currentHealth -= amountOfDamage;
        if (currentHealth <= 0)
        {
            //UNDONE: Removed until we review enemy pick-ups.
            //RandomlyDropPickUp();
            KillEnemy();
        }
    }

    /// <summary>
    /// Destroy this object.
    /// </summary>
    public void KillEnemy()
    {
        Destroy(gameObject);
    }

    /// <summary>
    /// Early set-up.  Internal.
    /// </summary>
    private void Awake()
    {
        currentHealth = startingHealth;
    }

    //UNDONE:  Removed until we review enemy pick-ups.
    ///// <summary>
    ///// Randomly drop a pick-up. Likelihood based on (pickUpChance) -- higher pickUpChance is more likely to spawn pick-up.
    ///// </summary>
    //void RandomlyDropPickUp()
    //{
    //    if (pickUpChance > 0.0f)
    //    {
    //        if (Random.value < pickUpChance)
    //        {
    //            GameObject pickUp = Instantiate(possiblePickUps[Random.Range(0, possiblePickUps.Length)], transform.position, transform.rotation);
    //        }
    //    }
    //} 
}
