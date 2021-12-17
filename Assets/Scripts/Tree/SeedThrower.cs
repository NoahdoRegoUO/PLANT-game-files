using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedThrower : MonoBehaviour
{

    public float throwForce = 10f;
    public static int seedCount = 10;
    public GameObject seedPrefab;

    public AudioClip clip;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        seedCount = 10;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0) && PauseMenu.GameIsPaused == false)
        {
            if (seedCount > 0)
            {
                audioSource.PlayOneShot(clip);
                ThrowSeed();
                seedCount -= 1;
            }
            else
            {
                //out of seeds
            }
        }
    }

    void ThrowSeed()
    {
        GameObject seed = Instantiate(seedPrefab, transform.position, transform.rotation);
        Rigidbody rb = seed.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * throwForce, ForceMode.VelocityChange);
    }
}
