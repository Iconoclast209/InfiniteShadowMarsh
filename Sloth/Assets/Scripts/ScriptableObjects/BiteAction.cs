using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Actions/Bite")]
public class BiteAction : Action
{
    public override void Act(EnemyManager enemy)
    {
        TryToBitePlayer(enemy);
    }

    private static void TryToBitePlayer(EnemyManager enemy)
    {
        enemy.Animator.SetBool("isWalking", false);

        if (Time.realtimeSinceStartup >= (enemy.LastAttackTime + enemy.BiteDelay))
        {
            enemy.RB.velocity = new Vector2(0.0f, 1.5f);

            enemy.LastAttackTime = Time.realtimeSinceStartup;
            int LayerToHit = 1 << 8;
            if (Physics2D.OverlapCircle(enemy.transform.position, enemy.BiteRadius, LayerToHit) != null)
            {
                enemy.Animator.SetTrigger("bitesPlayer");
                AudioManager.Singleton.EnemyAttack();
                PlayerManager.Singleton.DamagePlayer(enemy.BiteDamage);
            }
        }
    }
}
