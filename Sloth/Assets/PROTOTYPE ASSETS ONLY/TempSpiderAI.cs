using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * 
 * ATTN:  I haven't commented this because it's extremely basic, and nothing more than a temporary place holder script.
 *        As of right now, the only thing it does is allow the spider to die when touched by a player.
 * 
 * */
public class TempSpiderAI : MonoBehaviour {

    private Rigidbody2D rb;
    private BoxCollider2D bc;

    // Use this for initialization
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();

        if (rb == null)
            print("Spider does not have a Rigidbody2D component.");
        if (bc == null)
            print("Spider does not have a BoxCollider2D component.");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            bc.enabled = false;
            rb.velocity = new Vector2(0.0f, 5.0f);
            rb.angularVelocity = 50.0f;
            StartCoroutine(DieAfterSeconds(2.0f));
        } 
    }

    IEnumerator DieAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }


}
