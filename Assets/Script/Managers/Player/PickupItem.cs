using UnityEngine;
using UnityEngine.UI;
using MalbersAnimations.Controller;

public class PickupItem : MonoBehaviour
{
    MAnimal playerController;
    Animator animator;  // Assuming you have an Animator for handling animations

    [Header("Player Item Pickup")]
    public Transform handTransform;
    public GameObject currentItem;
    public LayerMask itemLayer;

    [Min(0)] public float pickupRange;
    [Min(0)] public float maxAngle = 60f;

    GameObject nearbyItem;

    void Awake()
    {
        playerController = GetComponent<MAnimal>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        CheckForNearbyItems();
    }

    // Detect nearby gameobjects with layer "Pickable"
    void CheckForNearbyItems()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, pickupRange, itemLayer);
        nearbyItem = null;

        float minDistance = float.MaxValue;
        float maxDotProduct = -1.0f; // Ubah menjadi -1 untuk menjamin prioritas pada item yang lebih lurus

        foreach (Collider collider in hitColliders)
        {
            float distance = Vector3.Distance(transform.position, collider.transform.position);
            if (distance < minDistance)
            {
                float dotProduct = Vector3.Dot(transform.forward, collider.transform.position - transform.position);
                if (dotProduct > maxDotProduct)
                {
                    nearbyItem = collider.gameObject;
                    minDistance = distance;
                    maxDotProduct = dotProduct;
                }
            }
        }
    }

    // Picking up item
    public void OnGrab()
    {
        if (nearbyItem != null && currentItem == null)
        {
            currentItem = nearbyItem;
            nearbyItem = null;
            currentItem.transform.position = handTransform.position;
            currentItem.transform.parent = handTransform;

            animator.SetBool("IsHoldingItem", true);
        }

        else if (currentItem != null)
        {
            currentItem.transform.parent = null;
            currentItem = null;

            animator.SetBool("IsHoldingItem", false);
        }
    }
}
