using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Column_Rise : MonoBehaviour
{
    public float timer = 0f;
    public float riseTime = 5f;
    public float height = 15f;

    private Vector3 endPos;

    public bool isMaxSize = false;
    private bool doman = false;

    // Start is called before the first frame update
    void Start()
    {
        doman = false;
        isMaxSize = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (BossAI.cutscene == true && doman == false)
        {
            StartCoroutine(Rise());
            doman = true;
        }
    }

    private IEnumerator Rise()
    {

        //Set start position
        Vector3 startPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);

        //Define position change
        endPos = new Vector3(transform.position.x, height, transform.position.z);


        do
        {

            //Move up
            transform.localPosition = Vector3.Lerp(startPos, endPos, timer / riseTime);

            //Increment timer
            timer += Time.deltaTime;

            //Yield
            yield return null;
        }
        while (timer < riseTime);

        isMaxSize = true;
    }
}
