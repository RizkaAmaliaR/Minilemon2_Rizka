using UnityEngine;
using UnityEngine.UIElements;

public class PickupItem : MonoBehaviour
{
    [Header("Player Item Pickup")]
    [SerializeField] Transform handTransform;
    [SerializeField] Transform headTransform;
    [SerializeField] LayerMask itemLayer;

    [SerializeField, Min(0)] float pickupRange = 5f;
    [SerializeField, Range(0, 45)] float pickupAngle = 30f;

    GameObject currentItem;
    GameObject nearbyItem;

    void Awake()
    {

    }

    void Update()
    {
        CheckForNearbyItems();
    }

    // Detect nearby gameobjects with correct layer
    void CheckForNearbyItems()
    {
        Vector3 playerForward = transform.forward;
        
        // Ignore Y Axis
        Vector3 cameraForward = new(
            Camera.main.transform.forward.x,
            0,
            Camera.main.transform.forward.z
        );

        // Cast Ray
        Physics.Raycast(
            new Ray(headTransform.position, Camera.main.transform.forward),
            out RaycastHit hit,
            pickupRange,
            itemLayer,
            QueryTriggerInteraction.Collide
        );

        // Angle between player's direction and camera's direction
        float angle = Vector3.Angle(playerForward, cameraForward);
        Debug.Log(angle);

        if (hit.collider != null && angle <= pickupAngle)
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
        }
    }

    // Dropping item
    public void OnDrop()
    {
        if (currentItem != null)
        {
            currentItem.transform.parent = null;
            if (Physics.Raycast(currentItem.transform.position, Vector3.down, out RaycastHit hit, 2f, LayerMask.GetMask("Ground")))
            {
                currentItem.transform.position = hit.point;
                currentItem.transform.rotation = Quaternion.identity;
            }
            currentItem = null;
        }
    }
}
