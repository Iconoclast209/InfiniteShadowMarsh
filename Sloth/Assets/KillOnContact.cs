using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillOnContact : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //SLOPPY:  Does the job for now.  Take the hard code number out.
            PlayerManager.Singleton.DamagePlayer(1000000);
        }
    }
}
