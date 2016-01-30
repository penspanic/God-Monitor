using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Title : MonoBehaviour
{
    void Awake()
    {

    }

    public void OnStartButtonDown()
    {
        StartCoroutine(SceneFader.Instance.FadeOut(1f, "InGame"));
        StartCoroutine(SceneFader.Instance.SoundFadeOut(1f, GameObject.FindObjectsOfType<AudioSource>()));
    }
}