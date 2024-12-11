using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public Item item;

    public virtual void Pick()
    {
        EventBus.InvokeItemCollected(item);
        
        Player.instance.inventory.AddItem(item);
        Destroy(gameObject);
    }
}
