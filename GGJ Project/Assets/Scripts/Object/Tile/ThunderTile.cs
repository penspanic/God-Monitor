using UnityEngine;
using System.Collections;

public class ThunderTile : Tile
{
    public GameObject thunder;
    EventBase targetEvent;
    public override void UseTile(EventBase targetEvent)
    {
        this.targetEvent = targetEvent;
        StartCoroutine(ThunderProcess());
    }

    IEnumerator ThunderProcess()
    {
        GameObject thunderObj = Instantiate(thunder);
        thunderObj.transform.SetParent(targetEvent.town.transform);
        thunderObj.transform.localPosition = new Vector2(0, 2.25f);

        yield return new WaitForSeconds(5f);
        Destroy(thunderObj);
    }
}
