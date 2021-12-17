using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public GameObject barrier;

    public static bool radioGrab = false;
    public static bool endDialogue = false;

    private bool firstTime = false;

    public int numOfSentences = 2;

    private void Start()
    {
        radioGrab = false;
        firstTime = false;
        endDialogue = false;
    }

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
    

    private void OnTriggerStay(Collider other)
    {

        if (other.tag == "Player")
        {
            if (!firstTime)
            {
                FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
                radioGrab = true;
                firstTime = true;
                barrier.SetActive(false);
                StartCoroutine(MessageContinue(1, numOfSentences));
            }
        }
        
    }

    IEnumerator MessageContinue(int counter, int sentenceCount)
    {
        Debug.Log("Started Coroutine at timestamp : " + Time.time);

        if (firstTime)
        {
            while (counter <= sentenceCount)
            {
                yield return new WaitForSeconds(3f);
                FindObjectOfType<DialogueManager>().DisplayNextSentence();
                counter += 1;
            }
        }
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);
        endDialogue = true;
    }
}