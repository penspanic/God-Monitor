using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public static GameObject selectedObject;
    Vector3 startPos;
    Transform startParent;

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (GetComponent<Animator>() != null)
        {
            GetComponent<Animator>().enabled = false;
            GetComponent<Rigidbody2D>().gravityScale = 0;
        }

        selectedObject = gameObject;
        startParent = transform.parent;
        gameObject.layer = 2;
        startPos = transform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 worldPos = Input.mousePosition;
        worldPos.z = Camera.main.nearClipPlane;
        worldPos = Camera.main.ScreenToWorldPoint(worldPos);
        transform.position = worldPos;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (GetComponent<Animator>() != null)
        {
            GetComponent<Animator>().enabled = true;
            GetComponent<Rigidbody2D>().gravityScale = 2;
        }

        selectedObject = null;
        gameObject.layer = 0;
        if (transform.parent == startParent)
            transform.position = startPos;
    }

    public void ResetPosition()
    {
        transform.SetParent(startParent);
        transform.position = startPos;
        transform.localScale = Vector3.one;
    }
}
