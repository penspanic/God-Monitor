using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class DragDropSlot : MonoBehaviour, IDropHandler
{
    private GameObject GetItem()
    {
        if(transform.childCount > 0)
            return transform.GetChild(0).gameObject;
        else
            return null;
    }

    public void OnDrop( PointerEventData eventData )
    {
        Debug.Log("Drop");
        if( GetItem() == null )
        {
            DragAndDrop.selectedObject.transform.SetParent( transform );
            DragAndDrop.selectedObject.transform.localPosition = new Vector3(0, 0, -1);
        }
    }
}
