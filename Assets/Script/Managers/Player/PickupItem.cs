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

    // Detect nearby gameobjects with correct layer
    void CheckForNearbyItems()
    {
        Collider[] hitColliders = Physics.OverlapCapsule(
            transform.position + Vector3.up * 1.0f,
            transform.position + Vector3.down * 1.0f,
            pickupRange, itemLayer
        );
        nearbyItem = null;
        foreach (Collider collider in hitColliders)
        {
            Vector3 direction = collider.transform.position - transform.position;
            float angle = Vector3.Angle(transform.forward, direction);

            if (angle < maxAngle)
            {
                nearbyItem = collider.gameObject;
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
            if (Physics.Raycast(currentItem.transform.position, Vector3.down, out RaycastHit hit, 2f, LayerMask.GetMask("Ground")))
            {
                currentItem.transform.position = hit.point;
                currentItem.transform.rotation = Quaternion.identity;
            }
            currentItem = null;

            animator.SetBool("IsHoldingItem", false);
        }
    }
}
