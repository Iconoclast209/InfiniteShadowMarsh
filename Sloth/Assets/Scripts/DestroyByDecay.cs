using System.Collections;
using UnityEngine;

///<summary>Destroy object over time.</summary>
public class DestroyByDecay : MonoBehaviour {

    [Tooltip("Amount of time before decaying.")]
	[SerializeField]
    private float lifeTimeInSeconds;

	/// <summary>Early set-up.  Internal.</summary>
	void Awake () {
		StartCoroutine (DestroyInSeconds (lifeTimeInSeconds));
	}
	
    /// <summary>Destroys self after countdown.</summary>
    /// <param name="seconds">Length of time before destroying self.</param>
	IEnumerator DestroyInSeconds (float seconds){
		yield return new WaitForSeconds (seconds);
		Destroy (gameObject);
	}
}
