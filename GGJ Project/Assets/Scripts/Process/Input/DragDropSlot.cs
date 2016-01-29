using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class DragDropSlot : MonoBehaviour, IDropHandler
{
    EventBase targetEvent;
    void Awake()
    {
        targetEvent = GetComponent<EventBase>();
    }

    private GameObject GetItem()
    {
        if (transform.childCount > 0)
            return transform.GetChild(0).gameObject;
        else
            return null;
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("Drop");
        if (GetItem() == null)
        {
            if (Tile.CompareID(DragAndDrop.selectedObject.GetComponent<Tile>(), this.GetComponent<EventBase>()))
            {
                targetEvent.TileAttatched(DragAndDrop.selectedObject.GetComponent<Tile>());
                DragAndDrop.selectedObject.transform.SetParent(transform);
                DragAndDrop.selectedObject.transform.localPosition = new Vector3(0, 0, -1);
            }
        }
    }
}