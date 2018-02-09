using UnityEngine;

// LATER:  Need to correct functionality after determining level background design. WILL NOT WORK YET.

/// <summary>This class causes the parent object to spawn within a specified global Z-axis range, and "scroll" along the global Z-axis.</summary>
public class BackgroundScroller : MonoBehaviour {
    [Tooltip("Speed at which the object should scroll")]
	[SerializeField]
    private float scrollSpeed;

    [Tooltip("Spawn location range along the Z-axis")]
	[SerializeField]
    private Vector2 startingPositionMinMax;

    /// <summary>Set-up.  Parent object will be placed at a random location along the global Z-axis.</summary>
	void Start () {
		transform.position = new Vector3 (
			transform.position.x,
			transform.position.y,
			Random.Range (startingPositionMinMax.x, startingPositionMinMax.y));
	}
	
	/// <summary>Parent object will scroll "downward" past player at a rate of scrollSpeed multiplied by deltaTime</summary>
	void Update () {
		transform.position = new Vector3 (transform.position.x, transform.position.y, (transform.position.z - (scrollSpeed * Time.deltaTime)));
	}
}
