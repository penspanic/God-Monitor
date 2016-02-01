using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Title : MonoBehaviour
{
    public static bool isFirstGame = true;

    public GameObject[] goats;

    public GameObject titleObj;
    public GameObject titleBGM;

    public SpriteRenderer startRenderer;

    AudioSource audioSource;
    public AudioClip startSound;

    public bool isChanging; 
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        foreach (GameObject eachGoat in goats)
            eachGoat.transform.position = new Vector2(Random.Range(-5, 5), Random.Range(5, 7));

        GameObject[] titleBGMs = GameObject.FindGameObjectsWithTag("Title BGM");

        if (titleBGMs.Length == 2)
        {
            foreach (GameObject eachBGM in titleBGMs)
                if (eachBGM.transform.position != Vector3.one)
                    Destroy(eachBGM.gameObject);
        }
        DontDestroyOnLoad(titleBGM);
        titleBGM.transform.position = Vector3.one;
        StartCoroutine(TitleMove());
        StartCoroutine(StartFlicker());

        SceneFader.Instance.FillScreenBlack();
        StartCoroutine(SceneFader.Instance.FadeIn(1f));

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
        if( isFirstGame )
        {
            isFirstGame = false;
            StartCoroutine( SceneFader.Instance.FadeOut( 1f, "Intro" ) );
        }
        else
        {
            StartCoroutine( SceneFader.Instance.FadeOut( 1f, "InGame" ) );
        }
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

    IEnumerator StartFlicker()
    {
        float nextFlickerTime = 0f;
        float disabledTime = 0f;
        while(true)
        {
            nextFlickerTime = Random.Range(0.1f, 7);
            yield return new WaitForSeconds(nextFlickerTime);

            disabledTime = Random.Range(0.05f, 0.3f);
            startRenderer.enabled = false;
            yield return new WaitForSeconds(disabledTime);
            startRenderer.enabled = true;
        }
    }
}