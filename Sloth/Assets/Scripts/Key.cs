using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script that allows an object to unlock a door.
/// </summary>
public class Key : MonoBehaviour {

    [Tooltip("Door that this unlocks.")]
    public ExitDoor doorToOpen;

	/// <summary>
    /// Early set-up.
    /// </summary>
	private void Awake ()
    {
        if (doorToOpen == null)
            print(gameObject.name + " does not have a doorToOpen value set on the Key script.");
	}

    /// <summary>
    /// Function called when this object is involved in an OnTriggerEnter2D event.
    /// </summary>
    /// <param name="collision">Collider of other object.</param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            doorToOpen.UnlockDoor();
            Destroy(gameObject);
        }
    }
}
