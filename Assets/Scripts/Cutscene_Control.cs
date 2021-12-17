using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene_Control : MonoBehaviour
{
    public Animator transition;

    //float for controlling time of transition
    public float transitionTime = 1f;

    public GameObject[] image;

    public GameObject[] text;

    public int counter = 0;

    private bool loaded = false;

    // Start is called before the first frame update
    void Start()
    {
        counter = 0;

        for (int i = 0; i < image.Length; i++)
        {
            image[i].SetActive(false);
            text[i].SetActive(false);
        }

        image[0].SetActive(true);
        text[0].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0) && counter < image.Length - 1)
        {
            StartCoroutine(FadeToBlack());
            Next();
        }
        else if (Input.GetMouseButtonUp(0) && counter == image.Length - 1)
        {
            LevelLoader.next = true;
            Debug.Log("nextlvl");
        }
    }

    public void Next()
    {
        counter += 1;

        for (int i = 0; i < image.Length; i++)
        {
            image[i].SetActive(false);
            text[i].SetActive(false);
        }

        image[counter].SetActive(true);

        text[counter].SetActive(true);
    }

    IEnumerator FadeToNorm()
    {
        transition.SetTrigger("NextImg");

        yield return new WaitForSeconds(transitionTime);
    }

    IEnumerator FadeToBlack()
    {
        transition.SetTrigger("NextImage");

        yield return new WaitForSeconds(transitionTime);
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(3);
    }
}
