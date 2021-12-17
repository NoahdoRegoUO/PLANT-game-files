using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{

    public float minWaitTime;
    public float maxWaitTime;
    public Material[] material;
    Renderer rend;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        rend.sharedMaterial = material[0];
        StartCoroutine(Flashing());
    }

    IEnumerator Flashing ()
    {
        while (true)
        {
            rend.sharedMaterial = material[0];
            yield return new WaitForSeconds(Random.Range(minWaitTime, maxWaitTime));
            rend.sharedMaterial = material[1];
            yield return new WaitForSeconds(Random.Range(minWaitTime, maxWaitTime));
        }
    }

}
