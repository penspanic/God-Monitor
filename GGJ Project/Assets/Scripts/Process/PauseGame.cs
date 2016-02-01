using UnityEngine;
using System.Collections;

public class PauseGame : MonoBehaviour
{

    public GameObject pauseBlack;
    public GameObject continueButton;
    public GameObject exitButton;
    public void OnPauseButtonDown()
    {
        Time.timeScale = 0;
        pauseBlack.SetActive(true);
        continueButton.SetActive(true);
        exitButton.SetActive(true);
    }

    public void OnContinueButtonDown()
    {
        Time.timeScale = 1;
        pauseBlack.SetActive(false);
        continueButton.SetActive(false);
        exitButton.SetActive(false);
    }
}
