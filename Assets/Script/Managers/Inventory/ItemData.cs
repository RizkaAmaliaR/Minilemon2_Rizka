using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "Item Data")]
public class ItemData : ScriptableObject
{
    [field: SerializeField, Tooltip("Nama item")]
    public string itemName { get; private set; }

    [field: SerializeField, Tooltip("Icon item untuk ditampilkan di inventory")]
    public Sprite icon { get; private set; }

    [field: SerializeField, Tooltip("Banyaknya maksimal item yang bisa distack dalam 1 slot"), Min(1)]
    public int maxStack { get; private set; } = 1;

    [field: SerializeField, Tooltip("Prefab untuk item")]
    public GameObject prefab { get; private set; }

}
