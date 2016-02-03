using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Intro : MonoBehaviour
{
    public Text firstLineText;
    public Text secondLineText;
    public Text firstLineText2;
    public Text secondLineText2;
    public Text secondLineText3;

    public GameObject[] enterDots;

    public AudioClip typeSound;

    AudioSource audioSource;
    const string firstLineString = "@god#help";
    const string secondLineString = "@mankindsure. #followme #stop_sending_me_goats";
    void Start()
    {
        StartCoroutine(SceneFader.Instance.FadeIn(1f));
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(IntroProcess());
    }

    int inputCount = 0;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            inputCount++;
        if (inputCount >= 2)
            SceneChange();

    }
    IEnumerator IntroProcess()
    {
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < 4; i++)
        {
            firstLineText.text = firstLineString.Substring(0, i + 1);
            PlaySound();
            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(0.2f);

        for (int i = 0; i < 6; i++)
        {
            firstLineText2.text = firstLineString.Substring(4, i);
            PlaySound();
            yield return new WaitForSeconds(0.1f);
        }

        for(int i = 0;i<3;i++)
        {
            enterDots[i].SetActive(true);
            yield return new WaitForSeconds(0.5f);
        }

        for(int i = 0;i<8;i++)
        {
            secondLineText.text = secondLineString.Substring(0, i + 1);
            PlaySound();
            yield return new WaitForSeconds(0.2f);
        }

        yield return new WaitForSeconds(0.5f);

        for(int i = 0;i<5;i++)
        {
            secondLineText2.text = secondLineString.Substring(8, i + 1);
            PlaySound();
            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(0.5f);

        for(int i = 0;i<10;i++)
        {
            secondLineText3.text = secondLineString.Substring(13,i+1);
            PlaySound();
            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(0.5f);

        for(int i = 10;i<16;i++)
        {
            secondLineText3.text = secondLineString.Substring(13, i + 1);
            PlaySound();
            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(0.2f);

        for(int i =16;i<24;i++)
        {
            secondLineText3.text = secondLineString.Substring(13, i + 1);
            PlaySound();
            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(0.2f);

        for(int i = 24;i<27;i++)
        {
            secondLineText3.text = secondLineString.Substring(13, i + 1);
            PlaySound();
            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(0.2f);

        for(int i = 27;i<33;i++)
        {
            secondLineText3.text = secondLineString.Substring(13, i + 1);
            PlaySound();
            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(1f);

        SceneChange();
    }
    bool isChanging = false;
    void SceneChange()
    {
        if (isChanging)
            return;
        isChanging = true;
        StartCoroutine(SceneFader.Instance.FadeOut(1f, "InGame"));
    }
    void PlaySound()
    {
        audioSource.PlayOneShot(typeSound);
    }
}
