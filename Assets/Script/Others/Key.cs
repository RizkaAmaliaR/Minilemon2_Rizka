using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : Collectible
{
    public override void Pick()
    {
        Player.instance.inventory.AddKey();
        Destroy(gameObject);
    }
}
