using UnityEngine;
using System.Collections;

public class HealTile : Tile
{
    public GameObject heal;

    EventBase targetEvent;
    public override void UseTile(EventBase targetEvent)
    {
        this.targetEvent = targetEvent;
        StartCoroutine(HealParticleProcess());
    }
    
    IEnumerator HealParticleProcess()
    {
        GameObject healObj = Instantiate(heal);
        healObj.transform.SetParent(targetEvent.town.transform);
        healObj.transform.localPosition = Vector2.zero;

        yield return new WaitForSeconds(5f);
        Destroy(healObj);
    }
}
