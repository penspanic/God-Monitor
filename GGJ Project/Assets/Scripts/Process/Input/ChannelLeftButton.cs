using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class ChannelLeftButton : MonoBehaviour,IPointerDownHandler, IPointerUpHandler
{
    WorldManager worldMgr;
    void Awake()
    {
        worldMgr = GameObject.FindObjectOfType<WorldManager>();
    }

    bool canApplyButtonDown = true;
    float elapsedTime = 0f;
    void Update()
    {
        if (canApplyButtonDown)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime > 0.2f)
            {
                canApplyButtonDown = true;
                elapsedTime = 0f;
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        worldMgr.OnLeftButtonDown();
        canApplyButtonDown = false;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        worldMgr.OnLeftButtonUp();
    }
}
