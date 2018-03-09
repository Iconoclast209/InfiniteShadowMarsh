using UnityEngine;

public class InteractableObject : MonoBehaviour {
    [Space(10)]
    [Header("Base Interactable Object Options")]
    [Tooltip("Object's Name")][SerializeField]
    string objectName;
    [Tooltip("Object's Description")][SerializeField]
    string objectDescription;
    [Tooltip("Object's Behavior on interaction")][SerializeField]
    ObjectBehavior objectEffect;





    ///<summary>Accessor for the object's name</summary>
    virtual public string ObjectName
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
    ///<summary>Accessor for the object's description</summary>
    virtual public string ObjectDescription
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
    ///<summary>Accessor for the object's behavior</summary>
    virtual public ObjectBehavior ObjectEffect
    {
        get
        {
            return objectEffect;
        }

        set
        {
            objectEffect = value;
        }
    }





    ///<summary><para>This is the function to be called when the player interacts with the InteractableObject</para>
    ///<para>If not overridden by sub-class, will call object behaviour's primary funcitonality</para>
    ///<para>HINT:  Should be overridden for sub-class objects using IPickUp or IEquipment interface</para></summary>
    virtual public void OnInteractedWith()
    {
        ObjectEffect.Execute();
    }
}
