using UnityEngine;

/// <summary>Rotational functionality for 2d objects. Parent object must have a rigidbody.</summary>
public class Rotator : MonoBehaviour
{
    [Tooltip("Speed at which object should rotate.")][SerializeField]
    private float rotationSpeed;

    [Tooltip("Direction of spin. Unselected will turn counter-clockwise.")][SerializeField]
    private bool clockwiseRotation;

    [Tooltip("Is rotation enabled?")][SerializeField]
    private bool rotationEnabled = true;

    /// <summary>Reference to object's Rigidbody2D component.</summary>
    private Rigidbody2D rb;





    /// <summary>Public function that allows rotation to be enabled.</summary>
    public void EnableRotation()
    {
        rotationEnabled = true;
    }
    
    
    
    
    
    /// <summary>Set-up.  Gets reference to object's Rigidbody2D component.</summary>
	void Start () {
        rb = GetComponent<Rigidbody2D>();
	}
	
	/// <summary>Determines rotation direction based off of (clockwiseRotation) boolean, then rotates at (rotationSpeed).</summary>
	void Update () {
        if (rotationEnabled)
        {
            if (clockwiseRotation)
            {
                rotationSpeed = -Mathf.Abs(rotationSpeed);
            }
            else
            {
                rotationSpeed = -Mathf.Abs(rotationSpeed) * -1;
            }

            rb.angularVelocity = rotationSpeed;
        }
	}
}
