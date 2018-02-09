using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>Allows object to activate a referenced Rotator script on other game objects.</summary>
public class RotatorEnabler : MonoBehaviour {

    [Tooltip("Rotator to enable.")]
    [SerializeField]
    private Rotator rotatorToEnable;

	/// <summary>Early set-up.  Internal.</summary>
	private void Awake () {
        if (rotatorToEnable == null)
            print(gameObject.name + " does not have a rotatorToEnable set on the RotatorEnabler script!");
	}

    /// <summary>Activate referenced rotator when triggered.</summary>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            rotatorToEnable.EnableRotation();
            Destroy(gameObject);
        }
    }
}
