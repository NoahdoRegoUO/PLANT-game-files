using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{

    public static bool GameIsPaused = false, loadMenu = false, restart = false, loseMenuOpen = false;

    public GameObject pauseMenuUI;
    public GameObject controlsMenuUI;
    public GameObject loseMenuUI;
    public GameObject playerUI;

    public Text seedDisplay;
    public Text playerStatus;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                CloseControls();
                Resume();
            }
            else
            {
                Pause();
            }
        }

        if (EnemySight.lose == true)
        {
            if (loseMenuOpen == false)
            {
                GameOver();
                loseMenuOpen = true;
            }
        }

        seedDisplay.text = "" + SeedThrower.seedCount + "/10";
        playerStatus.text = "Status: " + Player_Control.status;
    }

    public void Resume()
    {

        //Deactivate pause menu
        pauseMenuUI.SetActive(false);

        //Activate player UI
        playerUI.SetActive(true);

        //Unfreeze game
        Time.timeScale = 1f;

        GameIsPaused = false;
    }

    void Pause()
    {
        //Activate pause menu
        pauseMenuUI.SetActive(true);

        //deactivate player UI
        playerUI.SetActive(false);

        //Freeze game
        Time.timeScale = 0f;

        GameIsPaused = true;


    }

    public void GameOver()
    {
        loseMenuUI.SetActive(true);

        pauseMenuUI.SetActive(false);

        //deactivate player UI
        playerUI.SetActive(false);

        //Freeze game
        Time.timeScale = 0f;

        GameIsPaused = true;
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SeedThrower.seedCount = 10;
        restart = true;
        GameIsPaused = false;
        Debug.Log("ooga?");
    }

    public void OpenControls()
    {
        controlsMenuUI.SetActive(true);

        pauseMenuUI.SetActive(false);

        loseMenuUI.SetActive(false);
    }

    public void CloseControls()
    {
        controlsMenuUI.SetActive(false);

        if (EnemySight.lose == true)
        {
            loseMenuUI.SetActive(true);
        }
        else
        {
            pauseMenuUI.SetActive(true);
        }
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
        loadMenu = true;
        GameIsPaused = false;
    }

    public void QuitGame()
    {
        Time.timeScale = 1f;
        GameIsPaused = false;
        Debug.Log("Quit");
        Application.Quit();
    }
}
