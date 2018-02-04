using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatorEnabler : MonoBehaviour {

    public Rotator rotatorToEnable;

	/// <summary>
    /// Early set-up.  Internal.
    /// </summary>
	private void Awake () {
        if (rotatorToEnable == null)
            print(gameObject.name + " does not have a rotatorToEnable set on the RotatorEnabler script!");
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            rotatorToEnable.EnableRotation();
            Destroy(gameObject);
        }
    }
}
