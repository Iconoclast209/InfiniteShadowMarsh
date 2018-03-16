using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : InteractiveObject
{
    private SpriteRenderer sprite;



    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        if (sprite == null)
            print(gameObject.name + " does not have an attached sprite renderer.");
    }

    public override void OnInteraction()
    {
        sprite.flipX = !(sprite.flipX);
    }
}
