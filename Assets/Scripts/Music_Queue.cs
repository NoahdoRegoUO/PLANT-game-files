using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Music_Queue : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] clips;

    private AudioSource audioSource;

    public bool chase;

    public float wait;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        MainMusic();
        chase = false;
        wait = 3f;
    }

    // Update is called once per frame
    void Update()
    {
        if (EnemySight.hidden == false && chase == false)
        {
            if (EnemySight.chaseDelay >= 1f)
            {
                ChaseMusic();
                chase = true;
                EnemySight.chaseDelay = 0;
            }
        }

        if (EnemySight.hidden == true && chase == true)
        {
            wait -= Time.deltaTime;
            if (wait <= 0)
            {
                wait = 3;
                MainMusic();
                chase = false;
            }
        }
    }

    void MainMusic()
    {
        audioSource.Stop();
        audioSource.loop = true;
        audioSource.clip = clips[0];
        audioSource.Play();
    }

    void ChaseMusic()
    {
        audioSource.Stop();
        audioSource.loop = true;
        audioSource.clip = clips[1];
        audioSource.Play();
    }

}
