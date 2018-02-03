using UnityEngine;

/// <summary>
/// Rotational functionality for 2d objects. Parent object must have a rigidbody.
/// <para>Will affect other objects so long as everything has a rigidbody/2d collider not set to trigger.</para>
/// </summary>
public class Rotator : MonoBehaviour {

    [Tooltip("Speed at which object should rotate.")]
    public float rotationSpeed;
    [Tooltip("Direction of spin. Unselected will turn counter-clockwise.")]
    public bool clockwiseRotation;

    /// <summary>
    /// Reference to object's Rigidbody2D component.
    /// </summary>
    private Rigidbody2D rb;
    
    /// <summary>
    /// Set-up.  Gets reference to object's Rigidbody2D component.
    /// </summary>
	void Start () {
        rb = GetComponent<Rigidbody2D>();
	}
	
	/// <summary>
    /// Determines rotation direction based off of (clockwiseRotation) boolean, then rotates at (rotationSpeed).
    /// </summary>
	void Update () {
        if (clockwiseRotation)
        {
            rotationSpeed = -Mathf.Abs(rotationSpeed);
        } else
        {
            rotationSpeed = -Mathf.Abs(rotationSpeed) * -1;
        }

        rb.angularVelocity = rotationSpeed;
		
	}
}
