using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Actions/Patrol")]
public class PatrolAction : Action
{
 



    public override void Act(Enemy enemy)
    {
        Patrol(enemy);
    }

    private void Patrol(Enemy enemy)
    {
        if (enemy.GetComponent<ICanPatrol>() != null)
        {
            ICanPatrol enemyPatrol = enemy.GetComponent<ICanPatrol>();
            enemy.Animator.SetBool("isWalking", true);

            // If character is moving left...
            if (enemyPatrol.IsMovingLeft)
            {
                // ...And if character is not reached the left edge of patrol
                if (enemy.transform.position.x > enemyPatrol.StartingPosition - enemyPatrol.PatrolRadius)
                    // ...Keep moving along.
                    enemy.RB.velocity = new Vector2(Mathf.Lerp(enemy.RB.velocity.x, -enemyPatrol.MovementSpeed, 0.5f), 0f);
                else
                {
                    // ...else, stop moving left
                    enemyPatrol.IsMovingLeft = false;
                    enemy.RB.velocity = Vector2.zero;
                }
            }
            // Otherwise character is moving right...
            else
            {
                // ...And if character has not reached the right edge of patrol...
                if (enemy.transform.position.x < enemyPatrol.StartingPosition + enemyPatrol.PatrolRadius)
                    // ...keep moving along.
                    enemy.RB.velocity = new Vector2(Mathf.Lerp(enemy.RB.velocity.x, enemyPatrol.MovementSpeed, 0.5f), 0);
                else
                {
                    // ...else, stop moving right.
                    enemyPatrol.IsMovingLeft = true;
                    enemy.RB.velocity = Vector2.zero;

                }
            }

            //Make enemy face the right direction
            if (enemy.RB.velocity.x != 0)
            {
                if (enemyPatrol.IsMovingLeft == false && enemy.RB.velocity.x > 0)
                    enemy.SpriteRenderer.flipX = true;
                else if (enemyPatrol.IsMovingLeft == true && enemy.RB.velocity.x < 0)
                    enemy.SpriteRenderer.flipX = false;
            }
        }
    }

}
