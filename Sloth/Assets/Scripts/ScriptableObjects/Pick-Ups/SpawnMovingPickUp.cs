using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Pick-Ups/Spawn Editing Pick-Up")]
public class SpawnMovingPickUp : PickUp
{

    public override void Action()
    {
        Player.Singleton.SpawnPoint = Player.Singleton.RB.transform.position;
    }

}
