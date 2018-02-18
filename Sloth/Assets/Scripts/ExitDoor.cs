using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Only allows player to escape once an "unlocked" criteria is met.  Otherwise, display warning.
/// </summary>
public class ExitDoor : MonoBehaviour
{
    [Tooltip("Text message to display when player tries to leave before the door is unlocked..")][SerializeField]
    private string textToDisplayWhenLocked;

    [Tooltip("Text message to display when player tries to leave after it's unlocked..")][SerializeField]
    private string textToDisplayWhenUnlocked;

    /// <summary>Is the door locked?</summary>
    private bool isLocked = true;





    /// <summary>Unlocks the door.</summary>
    public void UnlockDoor()
    {
        isLocked = false;
    }





    /// <summary>Early set-up.  Internal.</summary>
    private void Awake()
    {
        //Make sure variables are set.
        if (textToDisplayWhenLocked == null)
            print("No locked-door text set in the ExitDoor script!");
        if (textToDisplayWhenUnlocked == null)
            print("No unlocked-door text set in the ExitDoor script!");
    }

    /// <summary>Function called when this object is involved in an OnTriggerEnter2D event.</summary>
    /// <param name="collision">Collider of other object.</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //If triggered by player when locked, display exit-door warning text.
        if (collision.gameObject.CompareTag("Player"))
        {
            if (LevelManager.Singleton.CollectiblesCollected < LevelManager.Singleton.NumOfCollectibles)
                UIManager.Singleton.CreateMessage(textToDisplayWhenLocked);
            else
                UIManager.Singleton.CreateMessage(textToDisplayWhenUnlocked);
        }
    }

    /// <summary>Function called when this object is involved in an OnTriggerExit2D event.</summary>
    /// <param name="collision">Collider of other object.</param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        //If triggered by player when locked, remove exit-door warning text.
        if (collision.gameObject.CompareTag("Player") && isLocked)
            UIManager.Singleton.RemoveMessage();
    }
}

