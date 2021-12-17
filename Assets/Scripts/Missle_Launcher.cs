using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missle_Launcher : MonoBehaviour
{
    public GameObject rocketPrefab;

    // Update is called once per frame
    void Update()
    {
        if (BossAI.missle == true)
        {
            LaunchMissle();
            BossAI.missle = false;
        }
    }

    void LaunchMissle()
    {
        GameObject rocket = Instantiate(rocketPrefab, transform.position, transform.rotation);
    }
}
