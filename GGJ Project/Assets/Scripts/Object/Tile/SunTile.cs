using UnityEngine;
using System.Collections;

public class SunTile : Tile
{
    public GameObject light;
    EventBase targetEvent;
    public override void UseTile(EventBase targetEvent)
    {
        this.targetEvent = targetEvent;
        StartCoroutine(LightProcess());
    }

    IEnumerator LightProcess()
    {
        GameObject lightObj = Instantiate(light);
        lightObj.transform.SetParent(targetEvent.town.transform);
        lightObj.transform.localPosition = new Vector2(0, 2.5f);

        float elapsedTime = 0f;
        while(true)
        {
            elapsedTime += Time.deltaTime;
            lightObj.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, Mathf.PingPong(elapsedTime, 1));
            if (elapsedTime >= 5)
                break;
            yield return null;
        }

        Destroy(lightObj);
    }
}
