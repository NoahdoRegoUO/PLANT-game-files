using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Homing_Missile : MonoBehaviour
{
    /// The base movement speed of the missile, in units per second. 
    [SerializeField]
    private float speed = 15;

    /// The base rotation speed of the missile, in radians per second. 
    [SerializeField]
    private float rotationSpeed = 1000;

    /// The distance at which this object stops following its target and continues on its last known trajectory. 
    [SerializeField]
    private float focusDistance = 5;

    /// The transform of the target object.
    private Transform target;

    /// Returns true if the object should be looking at the target. 
    private bool isLookingAtObject = true;

    /// The tag of the target object.
    [SerializeField]
    private string targetTag = "Player";

    /// Error message.
    private string enterTagPls = "Please enter the tag of the object you'd like to target, in the field 'Target Tag' in the Inspector.";

    //Audio variables
    [SerializeField]
    private AudioClip[] clips;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        EnemySight.lose = false;

        if (targetTag == "")
        {
            Debug.LogError(enterTagPls);
            return;
        }

        target = GameObject.FindGameObjectWithTag(targetTag).transform;

        AudioClip clip = clips[0];
        audioSource.PlayOneShot(clip);
    }

    private void Update()
    {
        Vector3 targetDirection = target.position - transform.position;

        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, rotationSpeed * Time.deltaTime, 0.0F);

        transform.Translate(Vector3.forward * Time.deltaTime * speed, Space.Self);

        if (Vector3.Distance(transform.position, target.position) < focusDistance)
        {
            isLookingAtObject = false;
        }

        if (isLookingAtObject)
        {
            transform.rotation = Quaternion.LookRotation(newDirection);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        AudioClip clip = clips[0];
        audioSource.PlayOneShot(clip);

        if (other.tag == "Player")
        {
            EnemySight.lose = true;
        }
        else if (other.tag == "Object")
        {
            //explode
            Destroy(this.gameObject);
            BossAI.missleOut = false;
        }
        else if (other.tag == "Enemy")
        {
            //explode
            Destroy(this.gameObject);
            BossAI.bossHealth -= 1;
            BossAI.missleOut = false;
        }
    }
}
