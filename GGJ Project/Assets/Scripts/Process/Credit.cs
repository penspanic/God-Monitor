using UnityEngine;
using System.Collections;

public class Credit : MonoBehaviour
{
    public GameObject texts;

    float speed = 1f;
    void Awake()
    {
        StartCoroutine(SceneFader.Instance.FadeIn(1f));
        Invoke("FadeInEnd", 1f);
    }

    void FadeInEnd()
    {
        isMoving = true;
    }

    bool isMoving = false;
    void Update()
    {
        if (isMoving)
        {
            texts.transform.Translate(Vector2.up * Time.deltaTime * speed);

            if (texts.transform.position.y > 8)
                ReturnToTitle();
        }
        if (Input.GetMouseButtonDown(0) && isMoving)
            ReturnToTitle();

    }

    void ReturnToTitle()
    {
        isMoving = false;
        StartCoroutine(SceneFader.Instance.FadeOut(1f, "Title"));
    }

}
