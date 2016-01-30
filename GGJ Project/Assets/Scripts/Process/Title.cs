using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Title : MonoBehaviour
{
    public GameObject titleObj;

    AudioSource audioSource;
    public AudioClip startSound;
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(TitleMove());
    }

    public void OnStartButtonDown()
    {
        audioSource.volume = 2;
        audioSource.PlayOneShot(startSound);
        StartCoroutine(SceneFader.Instance.FadeOut(1f, "InGame"));
    }

    IEnumerator TitleMove()
    {
        float elapsedTime = 0f;
        while(true)
        {
            elapsedTime += Time.deltaTime;

            titleObj.transform.position = new Vector2(0, 2.5f + Mathf.Sin(elapsedTime * 3) / 5);
            yield return null;
        }
    }
}