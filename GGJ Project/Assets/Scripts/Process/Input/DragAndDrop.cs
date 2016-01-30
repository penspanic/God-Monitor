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
        gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
        startPos = transform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 worldPos = Input.mousePosition;
        worldPos.z = Camera.main.nearClipPlane;
        worldPos = Camera.main.ScreenToWorldPoint(worldPos);
        transform.position = worldPos;
        var goat = GetComponent<Goat>();
        if(goat != null)
            goat.beingDragged = true;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (GetComponent<Animator>() != null)
        {
            GetComponent<Animator>().enabled = true;
            GetComponent<Rigidbody2D>().gravityScale = 2;
        }

        var goat = GetComponent<Goat>();
        if( goat != null )
        {
            goat.beingDragged = false;
            gameObject.layer = LayerMask.NameToLayer("Goat");
        }
        else
        {
            if( transform.parent == startParent )
                transform.position = startPos;

            gameObject.layer = 0;
        }

        selectedObject = null;
    }

    public void ResetPosition()
    {
        transform.SetParent(startParent);
        transform.position = startPos;
        transform.localScale = Vector3.one;
    }
}
