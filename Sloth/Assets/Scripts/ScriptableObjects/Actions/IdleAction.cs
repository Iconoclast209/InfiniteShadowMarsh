using System;
using UnityEngine;

[CreateAssetMenu(menuName ="AI/Actions/Idle")]
public class IdleAction : Action
{

    public override void Act(Enemy enemy)
    {
        RemainIdle();
    }

    private void RemainIdle()
    {
    }
}
