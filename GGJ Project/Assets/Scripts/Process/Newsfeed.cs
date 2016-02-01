using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Newsfeed : MonoBehaviour
{
    public GameObject newsfeedTextPrefab;

    WorldManager worldMgr;
    Queue<Text> messageQueue = new Queue<Text>();

    void Awake()
    {
        worldMgr = GameObject.FindObjectOfType<WorldManager>();
    }

    public void PushEvent(EventBase targetEvent)
    {
        World targetWorld = worldMgr.GetWorld(targetEvent.town);

        string eventName = targetEvent.GetComponent<SpriteRenderer>().sprite.name;
        string message = "@god" + " #" + eventName + " #" + GetWorldName(targetWorld);

        if(messageQueue.Count!= 0)
        {
            foreach(Text eachText in messageQueue)
            {
                StartCoroutine(MoveUpward(eachText));
            }
        }
        Text newText = Instantiate(newsfeedTextPrefab).GetComponent<Text>();
        newText.transform.SetParent(this.transform, false);
        newText.transform.localPosition = new Vector2(200, 0);
        newText.text = message;
        messageQueue.Enqueue(newText.GetComponent<Text>());
    }

    string GetWorldName(World world)
    {
       switch(world.name)
       {
           case "World1":
               return "Mountain";
           case "World2":
               return "Pole";
           case "World3":
               return "Desert";
           case "World4":
               return "Canyon";
           case "World5":
               return "Sea";
           default:
               throw new System.ArgumentException();
       }
    }

    IEnumerator MoveUpward(Text text)
    {
        float elapsedTime = 0f;
        Vector2 startPos = text.transform.position;
        Vector2 endPos = new Vector2(startPos.x,startPos.y + 0.2f);
        while(elapsedTime> 0.2f)
        {
            elapsedTime += Time.deltaTime;
            EasingUtil.EaseVector2(EasingUtil.easeOutQuad, startPos, endPos, elapsedTime / 0.2f);
            yield return null;
        }
    }
}
