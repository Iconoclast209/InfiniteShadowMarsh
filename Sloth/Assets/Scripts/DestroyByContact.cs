using UnityEngine;

/// <summary>
/// <para>
/// BUGGED: WILL NOT WORK UNTIL WE CHANGE FUNCTIONALITY.  NEED TO DETERMINE COLLISION HANDLING DESIGN FIRST!!!</para>
/// <para>
/// TODO: Correct contact collision handling functionality after design is prepared.
/// </para>
/// </summary>
public class DestroyByContact : MonoBehaviour {
	[Tooltip("Amount of damage to give when collided with")]
	public int damageOnCollision;

	private EnemyManager enemyController;
	private PickUpManager pickUpController;

	void Awake()
	{
		enemyController = null;
		pickUpController = null;
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.CompareTag ("Player")) {
			if (gameObject.CompareTag ("Pick Up")) {
				PlayerCollidedWithPickUp (other.gameObject);
				return;
			} else {
				PlayerCollidedWithHazard (other.gameObject);
			}
		}
	} // Determine what objects collided, and respond appropriately

	void PlayerCollidedWithPickUp(GameObject otherObject)
	{
		if (!SuccessfullyAccessedPickUpManager ()) {
			return;
		}
		pickUpController.ExecutePickUp ();
		Destroy (gameObject);
	} // Call pick-up function ExecutePickUp, then destroy object

	void PlayerCollidedWithHazard(GameObject otherObject){
		if (!SuccessfullyAccessedEnemyController (gameObject)) {
			return;
		}

		PlayerManager.Singleton.DamagePlayer (damageOnCollision);
		enemyController.DamageEnemy (damageOnCollision);
	} // Damage player, explode, destroy hazard

	bool SuccessfullyAccessedEnemyController(GameObject gameObject){
		if ((enemyController = gameObject.GetComponent<EnemyManager> ()) == null) {
			print (gameObject.name + " cannot locate EnemyController component for DestroyByContact functionality!");
			return false;
		}
		return true;
	} // Returns whether successfully accessed this object's EnemyController component

	bool SuccessfullyAccessedPickUpManager(){
		if ((pickUpController = gameObject.GetComponent<PickUpManager> ()) == null) {
			print (gameObject.name + " cannot locate PickUpController component for DestroyByContact functionality!");
			return false;
		}
		return true;
	} // Returns whether successfully accessed this object's PickUpController component
}