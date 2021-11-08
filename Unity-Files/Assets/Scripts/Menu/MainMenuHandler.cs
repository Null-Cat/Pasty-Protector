using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuHandler : MonoBehaviour
{
    public GameObject instructionsPanel;
    public GameObject mainPanel;
    public static void StartGame()
    {
        SceneManager.LoadScene("Defender");
    }

    public static void ExitGame()
    {
        Application.Quit(0);
    }
    public void ShowInstructions()
    {
        mainPanel.SetActive(false);
        instructionsPanel.SetActive(true);
    }

    public void BackToMenu()
    {
        instructionsPanel.SetActive(false);
        mainPanel.SetActive(true);
    }
}
