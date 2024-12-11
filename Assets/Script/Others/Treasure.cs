using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Treasure : Collectible
{
    [SerializeField, Tooltip("Canvas tulisan tombol")]
    GameObject display;

    [SerializeField, Tooltip("Butuh kunci atau tidak?")]
    bool needKey;

    [SerializeField, Tooltip("Item yang akan diambil")]
    TextMeshProUGUI keyWarning;

    [SerializeField]
    Animator anim;

    bool isOpened = false;

    void OnTriggerEnter(Collider other)
    {
        if (isOpened) return;
        if (other.CompareTag("Player"))
        {
            display.SetActive(true);

            if (Player.instance.inventory.keys > 0 || !needKey)
            {
                keyWarning.text = "Tekan E untuk buka";
            }
            else
            {
                keyWarning.text = "Cari kuncinya dulu ya";
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            display.SetActive(false);
        }
    }

    public override void Pick()
    {
        
    }

    public void Interact()
    {
        if (isOpened) return;

        if (!needKey) OpenChest();
        else if (Player.instance.inventory.UseKey()) OpenChest();
    }

    void OpenChest()
    {
        EventBus.InvokeItemCollected(item);
        display.SetActive(false);
        anim.SetBool("OpenChest", true);

        isOpened = true;
    }
}
