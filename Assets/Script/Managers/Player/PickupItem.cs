using UnityEngine;
using UnityEngine.UI;
using MalbersAnimations.Controller;

public class PickupItem : MonoBehaviour
{
    MAnimal playerController;
    Animator animator;  // Assuming you have an Animator for handling animations

    [Header("Player Item Pickup")]
    [SerializeField] Transform handTransform;
    [SerializeField] Transform headTransform;
    [SerializeField] LayerMask itemLayer;

    [SerializeField, Min(0)] float pickupRange = 5f;

    GameObject currentItem;
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
        RaycastHit hit;
        Physics.Raycast(
            new Ray(headTransform.position, Camera.main.transform.forward),
            out hit,
            pickupRange,
            itemLayer,
            QueryTriggerInteraction.Collide
        );

        if (hit.collider != null)
        {
            nearbyItem = hit.collider.gameObject;
        }
        else
        {
            nearbyItem = null;
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
