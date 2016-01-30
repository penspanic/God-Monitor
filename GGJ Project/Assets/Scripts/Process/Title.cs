using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Title : MonoBehaviour
{

    AudioSource audioSource;
    public AudioClip startSound;
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void OnStartButtonDown()
    {
        audioSource.volume = 2;
        audioSource.PlayOneShot(startSound);
        StartCoroutine(SceneFader.Instance.FadeOut(1f, "InGame"));
    }
}