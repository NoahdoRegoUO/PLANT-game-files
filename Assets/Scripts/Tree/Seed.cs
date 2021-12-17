using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed : MonoBehaviour
{
    //Timer for tree grow
    public float timer = 3f;

    //Radius for checking nearby objects
    public float radius = 3f;

    //Storage for tree prefab
    public GameObject treeModelPrefab;

    float countdown;
    bool hasSpawnedTree = false;

    void Start()
    {
        countdown = timer;
    }

    // Update is called once per frame
    void Update()
    {
        countdown -= Time.deltaTime;
        if (countdown <= 0f && !hasSpawnedTree)
        {
            GrowTree();
            hasSpawnedTree = true;
        }
    }

    void GrowTree()
    {
        Debug.Log("Tree");

        
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider nearbyObject in colliders)
        {
            //if enemy or power source, disable them
            //else cover raycasts from camera
        }

        Destroy(gameObject);

        GameObject tree = Instantiate(treeModelPrefab, transform.position, Quaternion.identity);
        Rigidbody rb = tree.GetComponent<Rigidbody>();

        hasSpawnedTree = false;
    }
}
