using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="AI/Decisions/Bite Decision")]
public class BiteDecision : Decision {

    public override bool Decide(Enemy enemy)
    {
        bool targetVisible = IsPlayerInBiteRange(enemy);
        return targetVisible;
    }

    private bool IsPlayerInBiteRange(Enemy enemy)
    {
        if (enemy.GetComponent<ICanBite>() != null)
        {
            ICanBite enemyBiter = enemy.GetComponent<ICanBite>();
            int LayerToHit = 1 << 8;
            if (Physics2D.OverlapCircle(enemy.transform.position, enemyBiter.BiteRadius, LayerToHit) != null)
                return true;
            else
                return false;
        }
        return false;
    }
}
