using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuHandler : MonoBehaviour
{
    public static void StartGame()
    {
        SceneManager.LoadScene("Defender");
    }

    public static void ExitGame()
    {
        Application.Quit(0);
    }
}
