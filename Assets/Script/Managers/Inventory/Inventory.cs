using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    // Daftar item utama
    public List<Item> mainItems { get; private set; }
    public int keys { get; private set; }

    public int selectedIndex;

    public Inventory()
    {
        mainItems = new List<Item>(25);
    }

    public void AddItem(Item item)
    {
        ItemData data = item.data;
        int amount = item.amount;
        foreach (Item i in mainItems)
        {
            if (i.data == data && i.amount < data.maxStack)
            {
                i.amount += amount;
                if (i.amount > data.maxStack)
                {
                    amount = i.amount - data.maxStack;
                    i.amount = data.maxStack;
                }
                else
                {
                    return;
                }
            }
        }
        while (amount > 0 && mainItems.Count < mainItems.Capacity)
        {
            int toAdd = Mathf.Min(amount, data.maxStack);
            mainItems.Add(new Item { data = data, amount = toAdd });
            amount -= toAdd;
        }

        InventoryUI.instance.Refresh();
    }

    public void RemoveItemAt(int index)
    {
        mainItems.RemoveAt(index);

        InventoryUI.instance.Refresh();
    }

    public void AddKey() => keys++;
    public bool UseKey()
    {
        if (keys > 0) 
        {
            keys--;
            return true;
        }
        return false;
    }
}
