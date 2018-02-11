using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>Displays messages when the player tries to exit out of the level entry door.</summary>
public class EntryDoor : MonoBehaviour
{
    [Tooltip("Text message to display when player tries to leave.")][SerializeField]
    private string textToDisplay;

    /// <summary>Early set-up.  Internal.</summary>
    private void Awake()
    {
        if (textToDisplay == null)
            print("No warning text set in the EntryDoor script!");
    }

    /// <summary>Function called when this object is involved in an OnTriggerEnter2D event.</summary>
    /// <param name="collision">Collider of other object.</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //If triggered by player, display entry-door warning text.
        if (collision.gameObject.CompareTag("Player"))
            UIManager.Singleton.CreateMessage(textToDisplay);
    }

    /// <summary>Function called when this object is involved in an OnTriggerExit2D event.</summary>
    /// <param name="collision">Collider of other object.</param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        //If triggered by player, remove entry-door warning text.
        if (collision.gameObject.CompareTag("Player"))
            UIManager.Singleton.RemoveMessage();
    }

}

