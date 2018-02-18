using UnityEngine;
/// <summary>Class for pick-ups meant to be collected for level progression.</summary>
[CreateAssetMenu(menuName ="Pick-Ups/Collectible Pick Up")]
public class CollectiblePickUp : PickUp {

    ///<summary>Over-ridden abstract.  Executes collectable pick-up action.</summary>
    public override void Action()
    {
        CollectThisPIckUp();
    }

    ///Informs level manager that this item has been picked-up.
    private void CollectThisPIckUp()
    {
        LevelManager.Singleton.CollectiblesCollected++;
        HUDManager.Singleton.UpdateItemsCollectedHUD();
    }

    ///Informs level manager of the existence of this pick-up.
    public override void SetUp()
    {
        LevelManager.Singleton.NumOfCollectibles++;
        HUDManager.Singleton.UpdateItemsCollectedHUD();
        base.SetUp();
    }
    
}
