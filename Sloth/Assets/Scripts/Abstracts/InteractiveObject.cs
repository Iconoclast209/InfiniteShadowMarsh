using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>Base abstract class for all interactive objects.  Utilizes IInteractable.</summary>
public abstract class InteractiveObject : MonoBehaviour, IInteractable {
    [Space(10)]
    [Header("Basic Object Information")]
    [Tooltip("Object's Name")][SerializeField]
    private string objectName;
    [Tooltip("Object's Description")][SerializeField]
    private string objectDescription;

    



    /// <summary>Accessor for objectName</summary>
    public string ObjectName
    {
        get
        {
            return objectName;
        }

        set
        {
            objectName = value;
        }
    }
    /// <summary>Accessor for objectDescription</summary>
    public string ObjectDescription
    {
        get
        {
            return objectDescription;
        }

        set
        {
            objectDescription = value;
        }
    }





    /// <summary>Declares functionality to be taken on interaction.</summary>
    abstract public void OnInteraction();
}
