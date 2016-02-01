using UnityEngine;
using System.Collections;

public class Credit : MonoBehaviour
{
    public GameObject texts;

    float speed = 1f;
    void Awake()
    {
        StartCoroutine(SceneFader.Instance.FadeIn(1f));
    }

    bool isMoving = true;
    void Update()
    {
        if (isMoving)
        {
            texts.transform.Translate(Vector2.up * Time.deltaTime * speed);

            if (texts.transform.position.y > 8)
                ReturnToTitle();
        }
    }

    void ReturnToTitle()
    {
        isMoving = false;
        StartCoroutine(SceneFader.Instance.FadeOut(1f, "Title"));
    }

}
