using UnityEngine;
using System.Collections;

public class RainTile : Tile
{
    public GameObject rain;

    EventBase targetEvent;
    public override void UseTile(EventBase targetEvent)
    {
        this.targetEvent = targetEvent;
        StartCoroutine(RainProcess());
    }

    IEnumerator RainProcess()
    {
        GameObject rainObj = Instantiate(rain);
        rainObj.transform.SetParent(targetEvent.town.transform);
        rainObj.transform.localPosition = new Vector2(0, 3f);

        yield return new WaitForSeconds(5f);
        Destroy(rainObj);
    }
}
