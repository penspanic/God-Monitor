using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Title : MonoBehaviour
{
    public Text titleText;
    void Awake()
    {
        StartCoroutine(MoveTitle());
    }

    public void OnStartButtonDown()
    {

    }

    IEnumerator MoveTitle()
    {
        while(true)
        {
            titleText.transform.position = new Vector2(0, Mathf.Sin(5f));
            yield return null;
        }
    }
}
