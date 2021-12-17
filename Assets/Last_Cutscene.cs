using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Last_Cutscene : MonoBehaviour
{
    public Animator transition;

    //float for controlling time of transition
    public float transitionTime = 1f;

    public GameObject[] image;

    public GameObject[] text;

    public int counter;

    private bool start;

    public float timer;
    public float time;

    private void Awake()
    {
        start = false;

        counter = 0;

        for (int i = 0; i < image.Length; i++)
        {
            image[i].SetActive(false);
            text[i].SetActive(false);
        }

        image[0].SetActive(true);
        text[0].SetActive(true);
        timer = 0;
        time = 15;
    }

    public void Update()
    {
        timer += Time.deltaTime;

        if (timer >= time)
        {
            StartCoroutine(FadeToBlack());
            StartCoroutine(Next());
            timer = 0;
        }

        if (counter == image.Length - 1)
        {
            LevelLoader.next = true;
        }
    }

    // Update is called once per frame
    public void Control()
    {
        if (start == true)
        {
            start = false;
            StartCoroutine(FadeToBlack());

            StartCoroutine(Next());
        }
    }

    IEnumerator Next()
    {
        counter += 1;
        time = 7;

        for (int i = 0; i < image.Length; i++)
        {
            image[i].SetActive(false);
            text[i].SetActive(false);
        }

        image[counter].SetActive(true);

        text[counter].SetActive(true);

        yield return new WaitForSeconds(5);
        start = true;
    }

    IEnumerator FadeToBlack()
    {
        transition.SetTrigger("NextImage");

        yield return new WaitForSeconds(transitionTime);
    }
}