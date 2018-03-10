using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrollingBiter : EnemyBiter, ICanPatrol {
    [Space(10)][Header("Patrolling stats.")]
    [Tooltip("Patrol radius")][SerializeField][Range(0.0f, 10.0f)]
    private float patrolRadius = 5.0f;
    [Tooltip("Should start patrolling left?")][SerializeField]
    private bool startMovingLeft;
    private float startingPosition;

    /// <summary>Accessor for patrol radius.</summary>
    public float PatrolRadius
    {
        get
        {
            return patrolRadius;
        }
        set
        {
            patrolRadius = value;
        }
    }
    /// <summary>Accessor for movement direction.</return>
    public bool IsMovingLeft
    {
        get
        {
            return startMovingLeft;
        }

        set
        {
            startMovingLeft = value;
        }
    }

    public float StartingPosition
    {
        get
        {
            return startingPosition;
        }

        set
        {
            startingPosition = value;
        }
    }





    /// <summary>Set starting position.</summary>
    public override void Awake()
    {
        base.Awake();
        StartingPosition = transform.position.x;
    }
    /// <summary>Added editor functionality.</summary>
    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        ShowPatrolInEditor();
    }
    /// <summary>Draw patrol distance in editor.</summary>
    private void ShowPatrolInEditor()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(new Vector3(transform.position.x - PatrolRadius, transform.position.y), new Vector3(transform.position.x + PatrolRadius, transform.position.y));
    }
}
