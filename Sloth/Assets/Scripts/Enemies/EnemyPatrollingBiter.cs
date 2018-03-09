using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrollingBiter : EnemyBiter, ICanPatrol {
    [Space(10)][Header("Patrolling stats.")]
    [Tooltip("Patrol radius")][SerializeField][Range(0.0f, 10.0f)]
    private float patrolRadius = 5.0f;
    [Tooltip("Should start patrolling left?")][SerializeField]
    private bool startMovingLeft;
    [Tooltip("Movement speed")][SerializeField]
    private float movementSpeed;
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
    /// <summary>Accessor for movement speed.</summary>
    public float MovementSpeed
    {
        get
        {
            return movementSpeed;
        }

        set
        {
            movementSpeed = value;
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

    private void Awake()
    {
        StartingPosition = transform.position.x;
    }
}
