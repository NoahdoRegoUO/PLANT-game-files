using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAI : MonoBehaviour
{
    public Transform player;

    public float rotateSpeed;
    public static int bossHealth = 3;

    public bool introEnd;
    public static bool cutscene;
    public static bool missle;
    public static bool missleOut;

    // Start is called before the first frame update
    void Start()
    {
        introEnd = false;
        missle = false;
        cutscene = false;
        missleOut = false;
        bossHealth = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if (DialogueTrigger.radioGrab == true && introEnd == false)
        {
            Intro();
            cutscene = true;
        }

        if (introEnd == true)
        {
            RotTowardsPlayer();
        }

        if (DialogueTrigger.endDialogue == true && missleOut == false)
        {
            missle = true;
            missleOut = true;
            Debug.Log("Launch missle");
        }

        if (bossHealth == 2 && missleOut == false)
        {

        }
        else if (bossHealth == 1 && missleOut == false)
        {

        }
        else if (bossHealth == 0 && missleOut == false)
        {
            //animate white fade out

        }
    }

    void Intro()
    {

        Quaternion currentRotation = transform.rotation;
        Quaternion wantedRotation = Quaternion.Euler(0, 180, 0);

        transform.rotation = Quaternion.RotateTowards(currentRotation, wantedRotation, Time.deltaTime * rotateSpeed);

        if (transform.rotation == wantedRotation)
        {
            introEnd = true;
        }
    }

    void RotTowardsPlayer()
    {
        transform.LookAt(player);
    }

}
