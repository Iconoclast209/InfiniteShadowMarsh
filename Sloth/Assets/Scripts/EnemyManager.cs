using UnityEngine;


/// <summary>
/// Controller class for basic enemy behavior such as movement, pick-up spawning.
/// <para>All enemies should have this attached!</para>
/// <para>Only standard enemy behavior goes here!  All enemy-specific functionality should be placed in its own script!</para>
/// </summary>
public class EnemyManager : MonoBehaviour
{
    [Tooltip("Set enemy Health. Mobs: 5-50; Bosses: >50")]
    [SerializeField]
    private int startingHealth;

    /// <summary>Current health of enemy.</summary>
    private int currentHealth;

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

    /// <summary>Destroy this object.</summary>
    public void KillEnemy()
    {
        Destroy(gameObject);
    }

    /// <summary>Early set-up.  Internal.</summary>
    private void Awake()
    {
        currentHealth = startingHealth;
    }
}
