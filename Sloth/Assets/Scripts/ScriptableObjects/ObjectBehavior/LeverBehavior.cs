using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Object Behaviors/Lever Behavior")]
public class LeverBehavior : ObjectBehavior
{
    [Space(10)]
    [Header("Lever Behaviour Settings")]
    [Tooltip("Is the lever tilted left?")][SerializeField]
    private bool facingLeft;
    [Tooltip("Can lever be used in current state?")][SerializeField]
    private bool isLeverFunctional;

    public override void Execute()
    {
        Debug.Log("Executed the lever action!");
    }
}
