using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTreeOpen : MonoBehaviour
{
    //gameobjects
    public GameObject door;
    public GameObject[] tree;

    //variables for detecting tree distance
    public float distance;
    public float num;

    //variables for opening door
    public float height = 8f;
    public float timer = 0f;
    public float growTime = 1f;
    bool isOpened = false;

    private void Start()
    {
        tree = GameObject.FindGameObjectsWithTag("Tree");
        distance = Vector3.Distance(door.transform.position, tree[0].transform.position);
    }

    private void Update()
    {
        tree = GameObject.FindGameObjectsWithTag("Tree");

        //Set num to number of objects in array
        num = GameObject.FindGameObjectsWithTag("Tree").Length;

        for (int i = 0; i < num; i++)
        {
            if (isOpened == false)
            {
                distance = Vector3.Distance(door.transform.position, tree[i].transform.position);
                if (distance < 6)
                {
                    Debug.Log("oooo yeah");
                    StartCoroutine(DoorOpen());
                    isOpened = true;
                }
            }

        }

    }

    private IEnumerator DoorOpen()
    {
        //start position
        Vector3 startPos = new Vector3(door.transform.position.x, door.transform.position.y, door.transform.position.z);

        //end position
        Vector3 endPos = new Vector3(door.transform.position.x, height, door.transform.position.z);

        do
        {
            Debug.Log("Open Sesame");

            door.transform.position = Vector3.Lerp(startPos, endPos, timer / growTime);

            timer += Time.deltaTime;

            yield return null;

        } while (timer < growTime);

        isOpened = true;

    }
}
