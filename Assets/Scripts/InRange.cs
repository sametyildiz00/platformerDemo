using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InRange : MonoBehaviour
{
    [SerializeField]
    float range;

    [SerializeField]
    GameObject rangePoint;
    [SerializeField]
    LayerMask targetLayer;

    public bool isInRange = false;

    public void inRange()
    {

        Collider2D[] hitResult = Physics2D.OverlapCircleAll(rangePoint.transform.position, range, targetLayer);

        if (hitResult == null)
            return;

        foreach (Collider2D hit in hitResult)
        {
            if(hit.GetComponent<IInteractable>() != null)
            {
                if (Input.GetKey(KeyCode.E))
                {
                    hit.GetComponent<IInteractable>().Interact();
                }
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        if (rangePoint == null)
            return;
        Gizmos.DrawWireSphere(rangePoint.transform.position, range);
    }

}
