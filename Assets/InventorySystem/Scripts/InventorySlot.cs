using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class InventorySlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        InventoryItem inventoryItem = dropped.GetComponent<InventoryItem>();

        RaycastHit hit;

        if (Physics.Raycast(Input.mousePosition, Vector3.forward, out hit, 1))
        {
            if (hit.collider.CompareTag("Inventory"))
            {

                if (transform.childCount == 0)
                {
                    inventoryItem.parentAfterDrag = transform;
                }

            }
            else
            {
                inventoryItem.parentAfterDrag = null;
            }
        }
    }
}
