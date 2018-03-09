using UnityEngine;

public interface IDamaging
{
    ///<summary>Issue damage to target.</summary>
    ///<param name="damageToGive">Amount of damage to givet to target.</param>
    ///<param name="target">Target which should receive damage.</param>
    void GiveDamage(int damageToGive, GameObject target);

    ///<summary>Destroy damageable object.</summary>
    ///<param name="target">Target which should be destroyed.</param>
    void Kill(GameObject target);
}
