using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{

    //Reference to animator
    public Animator transition;

    //float for controlling time of transition
    public float transitionTime = 1f;

    public static bool next = false, loadcredits = false;
    private bool ended = false;

    private void Start()
    {
        ended = false;
    }

    //Restart in Update
    void Update()
    {
        if (PauseMenu.restart == true)
        {
            StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex));
            PauseMenu.restart = false;
        }

        if (next == true)
        {
            next = false;
            EndLevel();
        }

        if (BossAI.bossHealth == 0 && ended == false)
        {
            StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
            ended = false;
            BossAI.bossHealth = 3;
        }

        if (loadcredits == true)
        {
            loadcredits = false;
            Credits();
        }
    }

    public void EndLevel()
    {
        LoadNextLevel();
    }

    public void LoadNextLevel()
    {
        //Loads the next scene in build index
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void Credits()
    {
        //Loads the next scene in build index
        StartCoroutine(LoadLevel(8));
    }

    public void LoadMenu()
    {
        StartCoroutine(LoadLevel(0));
    }

    //Coroutine for transition initiation and level load
    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);
    }

    //Coroutine for transition initiation and level load
    IEnumerator LoadEnd(int levelIndex)
    {
        transition.SetTrigger("BossDead");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            EndLevel();
        }
    }
}
