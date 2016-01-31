using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Title : MonoBehaviour
{
    public GameObject titleObj;

    AudioSource audioSource;
    public AudioClip startSound;

    public bool isChanging; 
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(TitleMove());
    }

    void Start()
    {
        StartCoroutine(SceneFader.Instance.FadeIn(1f));
    }

    int escapeCount = 0;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            escapeCount++;

            if(escapeCount >=3)
            {
                Application.Quit();
            }
        }
    }
    public void OnStartButtonDown()
    {
        if (isChanging)
            return;
        audioSource.volume = 2;
        audioSource.PlayOneShot(startSound);
        isChanging = true;
        StartCoroutine(SceneFader.Instance.FadeOut(1f, "Intro"));
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