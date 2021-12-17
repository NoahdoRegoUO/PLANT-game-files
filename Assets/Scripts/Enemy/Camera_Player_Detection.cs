using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Player_Detection : MonoBehaviour
{

	public GameObject player;

	public LayerMask mask;
	public LayerMask maskPlayer;

    private float distance;

	public static bool hidden = true;

	//Dialogue variables
	private bool firstTime = false;

	public Dialogue dialogue;

	public int numOfSentences = 2;

    private void Start()
    {
		hidden = true;
    }

    void Update ()
	{
		Ray ray = new Ray(transform.position, transform.forward);
		RaycastHit hitInfo;

		if (Physics.Raycast(ray, out hitInfo, 50, mask))
		{
			distance = Vector3.Distance(transform.position, hitInfo.point);
			Debug.DrawLine(ray.origin, hitInfo.point, Color.green);
			hidden = true;
		}
		if (Physics.Raycast(ray, out hitInfo, distance, maskPlayer))
		{
			Debug.DrawLine(ray.origin, hitInfo.point, Color.red);
			hidden = false;
		}
		else
        {
			hidden = true;
        }

	}

    private void OnTriggerStay(Collider other)
    {
		if (other.gameObject == player && hidden == false)
        {
			
			if (!firstTime)
			{
				FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
				firstTime = true;
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
	}
}
