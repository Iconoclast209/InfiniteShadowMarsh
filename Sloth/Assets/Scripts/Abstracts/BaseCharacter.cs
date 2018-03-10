using UnityEngine;

///<summary>Base class for all characters  in the game.  This will imbue them with the common characteristics of all characters.</summary>
public abstract class BaseCharacter : MonoBehaviour {
    [Space(10)]
    [Header("Base Character Stats")]
    [Tooltip("Character's name.")][SerializeField]
    protected string characterName;
    [Tooltip("Character's description.")][SerializeField]
    protected string characterDescription;
    [Tooltip("Movement speed.")][SerializeField]
    protected float movementSpeed;
    




    /// <summary>Accessor for characterName</summary>
    public string CharacterName
    {
        get
        {
            return characterName;
        }

        private set
        {
            characterName = value;
        }
    }
    /// <summary>Accessor for characterDescription</summary>
    public string CharacterDescription
    {
        get
        {
            return characterDescription;
        }

        set
        {
            characterDescription = value;
        }
    }
    /// <summary>Accessor for movementSpeed</summary>
    public float MovementSpeed
    {
        get
        {
            return movementSpeed;
        }

        set
        {
            movementSpeed = value;
        }
    }





    /// <summary>Editor-specific behaviour for character.</summary>
    virtual public void OnDrawGizmos() { }
}
