using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pausePanel;
    private bool paused = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(paused == true)
            {
                unpauseGame();
            }
            else
            {
                pauseGame();
            }
        }
    }

    void pauseGame()
    {
        paused = true;
        pausePanel.SetActive(true);
        Time.timeScale = 0;
    }
    void unpauseGame()
    {
        paused = false;
        pausePanel.SetActive(false);
        Time.timeScale = 1;
    }
}
