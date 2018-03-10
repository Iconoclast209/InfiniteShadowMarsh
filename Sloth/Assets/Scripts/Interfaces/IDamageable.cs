public interface IDamageable
{
    int StartingHealth { get; set; }
    int CurrentHealth { get; set; }
    bool IsAlive { get; set; }
    ///<summary>Receive damage.</summary>
    ///<param name="damageToTake">Amount of damage to receive.</param>
    void ReceiveDamage(int damageToTake);

    ///<summary>Destroy damageable object.</summary>
    void Death();
}
