using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour
{
    [HideInInspector] public int index;
    [HideInInspector] public Item item;

    [SerializeField] Image icon;
    [SerializeField] TextMeshProUGUI amount;

    void Start()
    {
        icon.sprite = item.data.icon;
        amount.text = item.amount == 1 ? "" : item.amount.ToString();
    }

    void Update()
    {

    }

    public void Selected()
    {
        InventoryUI.instance.Toggle();

        Player.instance.SetHand(item);
        Player.instance.inventory.selectedIndex = index;
    }
}
