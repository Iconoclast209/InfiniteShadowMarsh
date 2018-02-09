
using UnityEngine;
using UnityEngine.UI;

/// <summary>Class that gives a hint to the player when passing through object's trigger collider</summary>
public class Hint : MonoBehaviour
{
    [Tooltip("Text message to display when player enters hint trigger.")]
    [SerializeField]
    private string textToDisplay;

    [Tooltip("Number of times to present message. 0 = infinite!")]
    [SerializeField]
    private int timesToDisplay;

    /// <summary>Early set-up.  Internal.</summary>
	private void Awake()
    {
        if (textToDisplay == null)
            print("No himt text set in the Hint script!");
    }

    /// <summary>Function called when this object is involved in an OnTriggerEnter2D event.</summary>
    /// <param name="collision">Collider of other object.</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //If triggered by player, display entry-door warning text.
        if (collision.gameObject.CompareTag("Player")) { }
        UIManager.Singleton.CreateMessage(textToDisplay);
    }

    /// <summary>Function called when this object is involved in an OnTriggerExit2D event.</summary>
    /// <param name="collision">Collider of other object.</param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        //If triggered by player, remove entry-door warning text.
        if (collision.gameObject.CompareTag("Player"))
        {
            UIManager.Singleton.RemoveMessage();
            DecreaseCounter();
        }
    }

    /// <summary>Decrease counter. If 0, destroy object.</summary>
    private void DecreaseCounter()
    {
        timesToDisplay--;
        if (timesToDisplay == 0)
        {
            Destroy(gameObject);
        }
    }
}
