using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuHandler : MonoBehaviour
{
    public static void StartGame()
    {
        //Loads the second scene in the hierarchy
        SceneManager.LoadScene(1);
    }

    public static void ExitGame()
    {
        Application.Quit(0);
    }
}
