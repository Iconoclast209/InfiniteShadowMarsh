using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Gear : MonoBehaviour {
    [Tooltip("Number of teeth on this gear")][SerializeField]
    private int teethOnGear;

    private float speed = 0.0f;
    private bool clockwiseRotation;

    public int TeethOnGear
    {
        get
        {
            return teethOnGear;
        }

        set
        {
            teethOnGear = value;
        }
    }

    public float Speed
    {
        get
        {
            return speed;
        }

        set
        {
            speed = value;
        }
    }

    public bool ClockwiseRotation
    {
        get
        {
            return clockwiseRotation;
        }

        set
        {
            clockwiseRotation = value;
        }
    }

    private void Update()
    {
        if (Speed >= 0.0f)
        {
            float newRotation = gameObject.transform.rotation.z + (Speed * Time.deltaTime);


            gameObject.transform.Rotate(0f, 0f, newRotation);
        }
    }
}
