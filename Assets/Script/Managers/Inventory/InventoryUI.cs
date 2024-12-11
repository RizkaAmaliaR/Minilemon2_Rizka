using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class InventoryUI : MonoBehaviour
{
    public static InventoryUI instance { get; private set; }

    [field: SerializeField, Tooltip("Prefab slot inventory, terletak di Assets/Prefabs/UI/Inventory")]
    public GameObject inventorySlotPrefab { get; private set; }

    [field: SerializeField, Tooltip("Aksi input untuk inventory")]
    public InputActionMap actions { get; private set; } = new InputActionMap();

    public static bool visible { get; private set; } = false;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }

        // Buat scale jadi 0 karena jika diset inactive maka script tidak jalan
        transform.localScale = visible ? Vector3.one : Vector3.zero;

        // Input action
        actions.Enable();
        actions.FindAction("Toggle").performed += _ => Toggle();
    }

    // Perbarui inventory
    public void Refresh()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < Player.instance.inventory.mainItems.Count; i++)
        {
            InventorySlot newSlot = Instantiate(inventorySlotPrefab, transform).GetComponent<InventorySlot>();
            newSlot.item = Player.instance.inventory.mainItems[i];
            newSlot.index = i;
        }
    }

    public void Toggle()
    {
        visible = !visible;

        Cursor.visible = visible;
        Cursor.lockState = visible ? CursorLockMode.None : CursorLockMode.Locked;

        transform.localScale = visible ? Vector3.one : Vector3.zero;

        Refresh();
    }

    
}
