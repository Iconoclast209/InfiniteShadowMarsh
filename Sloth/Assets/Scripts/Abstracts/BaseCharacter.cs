using UnityEngine;

///<summary>Base class for all characters  in the game.  This will imbue them with the common characteristics of all characters.</summary>
public abstract class BaseCharacter : MonoBehaviour {
    [Space(10)]
    [Header("Base Character Stats")]
    [Tooltip("Character's name.")][SerializeField]
    string characterName;
    [Tooltip("Character's description.")][SerializeField]
    string characterDescription;
    
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
}
