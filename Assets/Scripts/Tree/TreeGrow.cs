using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeGrow : MonoBehaviour
{
    public float timer = 0f;
    public float growTime = 1f;
    public float maxSize = 6f;
    public float height = 2.9f;

    private Vector3 endPos;

    public bool isMaxSize = false;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.tag = "Tree";
        if (isMaxSize == false)
        {
            StartCoroutine(Grow());
        }
    }

    private IEnumerator Grow()
    {
        //Set start scale to local scale in inspector
        Vector3 startScale = transform.localScale;

        //Set start position
        Vector3 startPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);

        //Define rotation
        Vector3 to = new Vector3(0, Random.Range(-30.0f, 30.0f), 0);

        //Define maxScale
        Vector3 maxScale = new Vector3(maxSize, maxSize, maxSize);

        //Define position change
        endPos = new Vector3(transform.position.x, height, transform.position.z);


        do
        {
            //Grow
            transform.localScale = Vector3.Lerp(startScale, maxScale, timer/growTime);

            //Move up
            transform.localPosition = Vector3.Lerp(startPos, endPos, timer/growTime);

            //Rotate
            transform.eulerAngles = Vector3.Lerp(transform.rotation.eulerAngles, to, Time.deltaTime);

            //Increment timer
            timer += Time.deltaTime;

            //Yield
            yield return null;
        }
        while (timer < growTime);

        isMaxSize = true;
    }
}
