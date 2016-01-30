using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class ZeroGravityGoat : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public float initialForce = 100;
    public float drag = .1f;
    private Rigidbody2D thisRigidBody;
    private Vector2 startTouchPos;
    private float startTouchTime;


    void Start()
    {
        thisRigidBody = GetComponent<Rigidbody2D>();
        thisRigidBody.AddForce(new Vector2(-1, -1) * initialForce);
    }

    public void OnDrag( PointerEventData eventData )
    {
        //transform.position = Input.mousePosition;
    }

    public void OnBeginDrag( PointerEventData eventData )
    {
        startTouchPos = eventData.position;
        startTouchTime = Time.time;
        thisRigidBody.velocity = Vector2.zero;
    }

    public void OnEndDrag( PointerEventData eventData )
    {
        Vector2 deltaPos = eventData.position - startTouchPos;
        float deltaTime = Time.time - startTouchTime;
        Vector2 movement = deltaPos / deltaTime;
        thisRigidBody.velocity = Vector2.zero;
        thisRigidBody.AddForce(movement * drag);
    }
}
