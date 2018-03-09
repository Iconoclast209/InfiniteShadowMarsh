using UnityEngine;



/// <summary>Manager class for pick-up behavior.</summary>
public class PickUpManager : MonoBehaviour
{
    [Tooltip("Pick-Up Boost Information Structure")][SerializeField]
    private PickUp[] pickUps;

    /// <summary>Accessor to pick-up objects.</summary>
    public PickUp[] PickUps
    {
        get
        {
            return pickUps;
        }

        private set
        {
            pickUps = value;
        }
    }





    /// <summary>Set-up.  Calls pick-up's set-up functionality.</summary>
    private void Start()
    {
        foreach (PickUp pickUp in PickUps)
        {
            pickUp.SetUp();
        }
    }

    /// <summary>If pickUp uses trigger collider, execute on trigger.</summary><param name="collision">Collider2D of other object.</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            ExecutePickUp();
    }

    /// <summary>If pickUp uses collider, execute on trigger.</summary><param name="collision">Collision data.</param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            ExecutePickUp();
    }

    /// <summary>Applies pick-up behavior.</summary>
	private void ExecutePickUp() {
        foreach (PickUp pickUp in PickUps)
        {
            pickUp.Action();
        }
        Destroy(gameObject);
	}
}
