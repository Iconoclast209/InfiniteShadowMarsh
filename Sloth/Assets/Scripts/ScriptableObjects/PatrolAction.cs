using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Actions/Patrol")]
public class PatrolAction : Action
{
    public override void Act(EnemyManager enemy)
    {
        Patrol(enemy);
    }

    private void Patrol(EnemyManager enemy)
    {
        enemy.Animator.SetBool("isWalking", true);
        if (enemy.IsMovingLeft)
            if (enemy.transform.position.x > enemy.StartingPosition - enemy.PatrolRadius)
                enemy.RB.velocity = new Vector2(Mathf.Lerp(enemy.RB.velocity.x, -enemy.MovementSpeed, 0.5f), 0f);
            else
            {
                enemy.IsMovingLeft = false;
                enemy.RB.velocity = Vector2.zero;
            }
        else
            if (enemy.transform.position.x < enemy.StartingPosition + enemy.PatrolRadius)
                enemy.RB.velocity = new Vector2(Mathf.Lerp(enemy.RB.velocity.x, enemy.MovementSpeed, 0.5f), 0);
            else
            {
                enemy.IsMovingLeft = true;
                enemy.RB.velocity = Vector2.zero;
            }
    }
}
