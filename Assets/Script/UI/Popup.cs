using UnityEngine;

public class Popup : MonoBehaviour
{
    void Awake()
    {
        if (GameObject.FindGameObjectsWithTag("Popup").Length > 0)
        {
            Open();
        }
        else
        {
            Close();
        }
    }

    public static void Open()
    {
        if (isOpened) return;

        isOpened = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public static void Close()
    {
        if (!isOpened) return;

        isOpened = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public static bool isOpened = false;
}
