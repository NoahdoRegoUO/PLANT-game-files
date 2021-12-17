using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float xpos, ypos, zpos;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(xpos, 1 * Mathf.Cos(Time.time) + ypos, zpos);

        if (DialogueTrigger.radioGrab == true)
        {
            gameObject.SetActive(false);
        }
    }
}
