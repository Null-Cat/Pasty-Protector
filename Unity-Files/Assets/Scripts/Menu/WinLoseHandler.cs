using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinLoseHandler : MonoBehaviour
{
    public static void ReturnToMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
