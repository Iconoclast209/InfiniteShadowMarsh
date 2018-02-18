using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTransitioner : MonoBehaviour
{

    [Tooltip("Transitioner to send to.  Generally that transitioner will reference this one.")]
    [SerializeField]
    private PlayerTransitioner receiver;

    [Tooltip("Location to place player when transitioning to this object.")]
    [SerializeField]
    private Vector3 transitionToLocation;

    public PlayerTransitioner Receiver
    {
        get
        {
            return receiver;
        }

        private set
        {
            receiver = value;
        }
    }

    public Vector3 TransitionToLocation
    {
        get
        {
            return transitionToLocation;
        }

        private set
        {
            transitionToLocation = value;
        }
    }


    // Use this for initialization
    void Start () {
        VerifyVariables();
	}

    private void VerifyVariables()
    {
        if (Receiver == null)
            print(gameObject.name + " does not have a receiver set for transitioning.");
        if (TransitionToLocation == Vector3.zero)
            print(gameObject.name + " transitions to (0,0,0).  Are you sure it's set?");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerManager.Singleton.gameObject.transform.position = Receiver.TransitionToLocation;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(TransitionToLocation - new Vector3(.5f, 0, 0), TransitionToLocation + new Vector3(.5f, 0, 0));
    }

    public void ResetTransitionLocation()
    {
        TransitionToLocation = gameObject.transform.position;
    }
}
