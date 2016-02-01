using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
public class CreditGoat : Goat, IPointerClickHandler
{
    Title title;
    void Awake()
    {
        title = GameObject.FindObjectOfType<Title>();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (title.isChanging)
            return;
        title.isChanging = true;
        StartCoroutine(SceneFader.Instance.FadeOut(1f, "Credit"));
    }
}
