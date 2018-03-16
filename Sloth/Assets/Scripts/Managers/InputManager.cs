using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour {
    public delegate void NoArguments();

    public static event NoArguments OnWalkStart;
    public static event NoArguments OnWalkStop;
    public static event NoArguments OnClimbStart;
    public static event NoArguments OnClimbStop;
    public static event NoArguments OnAttack;
    public static event NoArguments OnJump;
    public static event NoArguments OnInteract;

    private bool tryingToJump = false;
    private bool tryingToAttack = false;
    private bool tryingToInteract = false;

    public bool TryingToJump
    {
        get
        {
            return tryingToJump;
        }

        set
        {
            tryingToJump = value;
        }
    }
    public bool TryingToAttack
    {
        get
        {
            return tryingToAttack;
        }

        set
        {
            tryingToAttack = value;
        }
    }
    public bool TryingToInteract
    {
        get
        {
            return tryingToInteract;
        }

        set
        {
            tryingToInteract = value;
        }
    }

    private void FixedUpdate()
    {
        if ((Input.GetAxis("Horizontal") != 0) && (OnWalkStart != null))
        {
            OnWalkStart();
        }
        if ((Input.GetAxis("Horizontal") == 0) && (OnWalkStop != null))
        {
            OnWalkStop();
        }
        if ((Input.GetAxis("Vertical") != 0) && (OnClimbStart != null))
        {
            OnClimbStart();
        }
        if ((Input.GetAxis("Vertical") == 0) && (OnClimbStop != null))
        {
            OnClimbStop();
        }
        if (TryingToJump && (OnJump != null))
        {
            TryingToJump = false;
            OnJump();
        }
        if (TryingToAttack && (OnAttack != null))
        {
            TryingToAttack = false;
            OnAttack();
        }
        if (TryingToInteract && (OnInteract != null))
        {
            TryingToInteract = false;
            OnInteract();
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            TryingToJump = true;

        if (Input.GetKeyDown(KeyCode.E))
            TryingToAttack = true;

        if (Input.GetKeyDown(KeyCode.F))
            TryingToInteract = true;
    }
}