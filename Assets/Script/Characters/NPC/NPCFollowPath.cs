using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCFollowPath : MonoBehaviour
{
    [SerializeField] Transform[] path;
    [SerializeField] Animator animator;

    int pathIndex;

    void OnAnimatorMove()
    {
        if (!animator.GetBool("ReachedByPlayer"))
        {
            transform.position = animator.rootPosition;
            transform.LookAt(path[pathIndex].transform);
            transform.eulerAngles.Set(0, transform.eulerAngles.y, 0);

            if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 100f))
            {
                transform.position = new Vector3(transform.position.x, hit.point.y, transform.position.z);
            }

            if (Vector3.Distance(transform.position, path[pathIndex].transform.position) < 0.25f)
            {
                pathIndex = (pathIndex + 1) % path.Length;
            }
        }
        else
        {
            transform.LookAt(Player.instance.transform.position);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            animator.SetBool("ReachedByPlayer", true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            animator.SetBool("ReachedByPlayer", false);
        }
    }
}
