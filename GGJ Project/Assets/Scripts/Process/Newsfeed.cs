using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MessageInfo
{
    public MessageInfo(Text text, float time)
    {
        this.messageText = text;
        this.elapsedTime = time;
    }
    public Text messageText;
    public float elapsedTime;
}
public class Newsfeed : MonoBehaviour
{
    public GameObject newsfeedTextPrefab;

    WorldManager worldMgr;
    Queue<MessageInfo> messageQueue = new Queue<MessageInfo>();

    AudioSource audioSource;

    void Awake()
    {
        worldMgr = GameObject.FindObjectOfType<WorldManager>();
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(MessageDelete());
    }

    public void PushEvent(EventBase targetEvent)
    {
        audioSource.Play();

        World targetWorld = worldMgr.GetWorld(targetEvent.town);

        string eventName = targetEvent.GetComponent<SpriteRenderer>().sprite.name;
        string message = "@god" + "  #" + eventName + "  #" + GetWorldName(targetWorld);

        Text newText = Instantiate(newsfeedTextPrefab).GetComponent<Text>();
        newText.transform.SetParent(this.transform, false);
        newText.transform.localPosition = new Vector2(200, -75);
        newText.text = message;
        messageQueue.Enqueue(new MessageInfo(newText.GetComponent<Text>(), 0));

        foreach (MessageInfo eachInfo in messageQueue)
        {
            StartCoroutine(MoveUpward(eachInfo.messageText));
        }
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

    IEnumerator MessageDelete()
    {
        while(true)
        {
            if(messageQueue.Count>0)
            {
                foreach(MessageInfo eachInfo in messageQueue)
                {
                    eachInfo.elapsedTime += Time.deltaTime;
                }
                if(messageQueue.Peek().elapsedTime > 3)
                {
                    MessageInfo destroyInfo = messageQueue.Dequeue();
                    float elapsedTime = 0f;
                    while(elapsedTime < 1)
                    {
                        elapsedTime += Time.deltaTime;
                        destroyInfo.messageText.color = Color.Lerp(new Color(1, 1, 1, 1), new Color(1, 1, 1, 0), elapsedTime);
                        yield return null;
                    }
                    Destroy(destroyInfo.messageText.gameObject);
                }
            }
            yield return null;
        }
    }

    IEnumerator MoveUpward(Text text)
    {
        Debug.Log("Move Upward");
        float elapsedTime = 0f;
        Vector2 startPos = text.transform.position;
        Vector2 endPos = new Vector2(startPos.x, startPos.y + 0.5f);
        while(elapsedTime < 0.2f)
        {
            if (text == null || text.IsDestroyed())
                break;
            elapsedTime += Time.deltaTime;
            text.transform.position = EasingUtil.EaseVector2(EasingUtil.easeOutQuad, startPos, endPos, elapsedTime / 0.2f);
            yield return null;
        }
    }
}
