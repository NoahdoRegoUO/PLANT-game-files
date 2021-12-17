using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{

    public Text dialogueText;
    public Text nameText;

    public static bool endMessage = false;

    public Animator animator;

    public AudioClip radioClip;
    
    private Queue<string> sentences;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue (Dialogue dialogue)
    {

        animator.SetBool("IsOpen", true);

        nameText.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence ()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            endMessage = false;
            return;
        }

        string sentence = sentences.Dequeue();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence (string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            AudioSource audio = GetComponent<AudioSource>();
            audio.clip = radioClip;
            audio.pitch = Random.Range(0.3f, 0.6f);
            audio.Play();
            yield return null;
            audio.Stop();
        }
        
    }

    void EndDialogue()
    {
        
        animator.SetBool("IsOpen", false);

        endMessage = true;
    }
}
