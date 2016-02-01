using UnityEngine;
using System.Collections;

public class Logo : MonoBehaviour
{

    void Awake()
    {
        StartCoroutine(SceneFader.Instance.FadeIn(1f));

        StartCoroutine(LogoProcess());
    }

    IEnumerator LogoProcess()
    {
        yield return new WaitForSeconds(2f);

        StartCoroutine(SceneFader.Instance.FadeOut(1f, "Title"));
    }
}
