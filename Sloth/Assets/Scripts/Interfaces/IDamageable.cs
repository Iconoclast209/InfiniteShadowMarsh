public interface IDamageable
{
    ///<summary>Receive damage.</summary>
    ///<param name="damageToTake">Amount of damage to receive.</param>
    void ReceiveDamage(int damageToTake);

    ///<summary>Destroy damageable object.</summary>
    void Death();
}
