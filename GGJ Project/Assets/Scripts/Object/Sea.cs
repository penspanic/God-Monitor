using UnityEngine;
using System.Collections;

public class Sea : MonoBehaviour
{

    void Awake()
    {
        StartCoroutine(SeaMoveProcess());
    }

    IEnumerator SeaMoveProcess()
    {
        while(true)
        {
            transform.localPosition = new Vector3(Mathf.Sin(Time.time / 2) * 2, -1, -1);
            yield return null;
        }
    }
}
