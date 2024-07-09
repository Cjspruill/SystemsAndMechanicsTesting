using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class InventoryItem : MonoBehaviour, IBeginDragHandler,IDragHandler,IEndDragHandler
{
    [SerializeField] public Image image;
    [SerializeField] InventoryController inventoryController;
    [SerializeField] string iconName;

    [SerializeField] Canvas canvas;
    [SerializeField] RectTransform rectTransform;
    [SerializeField]public Transform parentAfterDrag;
    [SerializeField] public Collider dropCollider;
    [SerializeField]public RectTransform inventoryPanel;
    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        parentAfterDrag = transform.parent;
        transform.SetParent(canvas.transform);
        transform.SetAsLastSibling();
        image.raycastTarget = false;
    }

   void IDragHandler.OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }


    public void OnEndDrag(PointerEventData eventData)
    {
        if (parentAfterDrag == null)
        { 
            gameObject.SetActive(false);
            inventoryController.Spawn(iconName);
        }
        else
        {
            transform.SetParent(parentAfterDrag);
            image.raycastTarget = true;
        }
    }
}
