using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ExitApp : MonoBehaviour
{
    public GameObject exitVerficationPanel;

    public void CheckExitStatus()
    {
        if(gameObject.activeSelf)
            CloseDialog();
        else
            OpenDialog();
    }

    public void CloseDialog()
    {
        exitVerficationPanel.SetActive(false);
    }

    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void OpenDialog()
    {
        exitVerficationPanel.SetActive(true);
    }
}
