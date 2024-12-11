using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    [Tooltip("Data dari item, letaknya di Assets/Resources/Items")]
    public ItemData data;
    
    [Tooltip("Jumlah item"), Min(1)]
    public int amount;
}
