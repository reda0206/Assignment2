using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{
    public void StartButton()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void InstructionsButton()
    {
        SceneManager.LoadScene("InstructionsScene");
    }

    public void QuitGameButton()
    {
        Application.Quit();
    }
}
