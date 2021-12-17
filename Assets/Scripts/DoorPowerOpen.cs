using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorPowerOpen : MonoBehaviour
{
    //gameobjects
    public GameObject door;
    public GameObject[] power;

    //variable for detecting which powerbox is broken
    public float num;
    public int boxnum;

    //variables for opening door
    public float height = 8f;
    public float timer = 0f;
    public float growTime = 1f;
    bool isOpened = false;

    private void Start()
    {
        power = GameObject.FindGameObjectsWithTag("Power");
        isOpened = false;
    }

    private void Update()
    {
        power = GameObject.FindGameObjectsWithTag("Power");

        //Set num to number of objects in array
        num = GameObject.FindGameObjectsWithTag("Power").Length;

        if (isOpened == false)
        {
            if (PowerBox.disable == true && power.Length == 0)
            {
                Debug.Log("OK");
                StartCoroutine(DoorOpen());
                isOpened = true;
            }

            if (power.Length == 0)
            {
                Debug.Log("Okayyyyy");
                StartCoroutine(DoorOpen());
                isOpened = true;
            }
        }

    }

    private IEnumerator DoorOpen()
    {
        //start position
        Vector3 startPos = new Vector3(door.transform.position.x, door.transform.position.y, door.transform.position.z);

        //end position
        Vector3 endPos = new Vector3(door.transform.position.x, door.transform.position.y + height, door.transform.position.z);

        do
        {
            door.transform.position = Vector3.Lerp(startPos, endPos, timer / growTime);

            timer += Time.deltaTime;

            yield return null;

        } while (timer < growTime);

        isOpened = true;

    }
}
