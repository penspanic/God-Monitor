using UnityEngine;
using System.Collections;

public class HarvestTile : Tile
{
    public GameObject bread;

    EventBase targetEvent;
    public override void UseTile(EventBase targetEvent)
    {
        this.targetEvent = targetEvent;
        StartCoroutine(HarvestProcess());
    }

    IEnumerator HarvestProcess()
    {
        GameObject breadObj = Instantiate(bread);
        breadObj.transform.SetParent(targetEvent.town.transform);
        breadObj.transform.localPosition = new Vector2(0, 4f);

        yield return new WaitForSeconds(5f);
        Destroy(breadObj);
    }
}
