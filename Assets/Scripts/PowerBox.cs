using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerBox : MonoBehaviour
{
    public static bool disable;
    public GameObject[] tree;
    public float range = 4;

    //Detect distance
    public float distance;
    public float num;

    // Start is called before the first frame update
    void Start()
    {
        disable = false;
        tree = GameObject.FindGameObjectsWithTag("Tree");
        distance = Vector3.Distance(transform.position, tree[0].transform.position);
    }

    private void Update()
    {
        tree = GameObject.FindGameObjectsWithTag("Tree");

        //Set num to number of objects in array
        num = GameObject.FindGameObjectsWithTag("Tree").Length;

        for (int i = 0; i < num; i++)
        {
                distance = Vector3.Distance(transform.position, tree[i].transform.position);
                if (distance < range)
                {
                    Debug.Log("oooo yeah");
                    StartCoroutine(Destroy());
                    disable = true;
                }

        }
    }

    private IEnumerator Destroy()
    {
        disable = true;
        gameObject.SetActive(false);
        yield return null;
    }

}
