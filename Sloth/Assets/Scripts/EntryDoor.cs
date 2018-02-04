using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Displays messages when the player tries to exit out of the level entry door.
/// </summary>
public class EntryDoor : MonoBehaviour
{

    [Tooltip("UI element in which to display the message.")]
    public Text textBoxToUse;
    [Tooltip("Text message to display when player tries to leave.")]
    public string textToDisplay;

    /// <summary>
    /// Early set-up.  Internal.
    /// </summary>
    private void Awake()
    {
        //Make sure variables are set.
        if (textBoxToUse == null)
            print("No UI Text Element set in the EntryDoor script!");
        if (textToDisplay == null)
            print("No warning text set in the EntryDoor script!");
    }

    /// <summary>
    /// Set-up.  External.
    /// </summary>
    private void Start()
    {
        //Set up textBoxToUse for future use.
        textBoxToUse.text = null;
        textBoxToUse.enabled = false;
    }

    /// <summary>
    /// Function called when this object is involved in an OnTriggerEnter2D event.
    /// </summary>
    /// <param name="collision">Collider of other object.</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //If triggered by player, display entry-door warning text.
        if (collision.gameObject.CompareTag("Player"))
            DisplayWarningText();
    }

    /// <summary>
    /// Function called when this object is involved in an OnTriggerExit2D event.
    /// </summary>
    /// <param name="collision">Collider of other object.</param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        //If triggered by player, remove entry-door warning text.
        if (collision.gameObject.CompareTag("Player"))
            RemoveWarningText();
    }

    /// <summary>
    /// Displays entry-door warning text on screen.
    /// </summary>
    private void DisplayWarningText()
    {
        textBoxToUse.enabled = true;
        textBoxToUse.text = textToDisplay;
    }

    /// <summary>
    /// Removes entry-door warning text from screen.
    /// </summary>
    private void RemoveWarningText()
    {
        textBoxToUse.text = null;
        textBoxToUse.enabled = false;
    }
}

