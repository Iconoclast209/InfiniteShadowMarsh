using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="AI/Decisions/Bite Decision")]
public class BiteDecision : Decision {

    public override bool Decide(EnemyManager enemy)
    {
        bool targetVisible = IsPlayerInBiteRange(enemy);
        return targetVisible;
    }

    private bool IsPlayerInBiteRange(EnemyManager enemy)
    {
        int LayerToHit = 1 << 8;
        if (Physics2D.OverlapCircle(enemy.transform.position, enemy.BiteRadius, LayerToHit) != null)
            return true;
        else
            return false;
    }
}
