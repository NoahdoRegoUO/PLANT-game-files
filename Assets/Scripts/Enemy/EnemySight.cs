using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySight : MonoBehaviour
{

	public Transform player;
    public float playerDistance, awareAI, AIMoveSpeed, damping = 6.0f;

    //Raycast
    public static bool hidden, chase;
	public LayerMask mask, maskPlayer;

	public Transform[] navPoint;
	public UnityEngine.AI.NavMeshAgent agent;
	public int destPoint = 0;
	public Transform goal;
	public static float enemyHealth, chaseDelay = 0;
	public static bool lose = false;

	private float distance = 50;
	private float hitObj;
	private Animator anim;

	void Start()
	{
		anim = GetComponent<Animator>();
		enemyHealth = 100;
		hidden = true;
        chase = false;
		chaseDelay = 0;
		UnityEngine.AI.NavMeshAgent agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
		agent.destination = goal.position;

		agent.autoBraking = false;

		GotoNextPoint();

	}

	void Update()
	{
		Ray ray = new Ray(transform.position + new Vector3(0.0f, 2.0f, 0.0f), transform.forward);
		RaycastHit hitInfo;

		if (Physics.Raycast(ray, out hitInfo, distance, maskPlayer))
		{
			Debug.DrawLine(ray.origin, hitInfo.point, Color.red);
			hidden = false;
		}
		else if (Physics.Raycast(ray, out hitInfo, distance, mask))
        {
			//hitPlayer = Vector3.Distance(transform.position, hitInfo);
			Debug.DrawLine(ray.origin, hitInfo.point, Color.green);
			hitObj = hitInfo.distance;
			hidden = true;

			if (Physics.Raycast(ray, hitObj, maskPlayer))
			{
				Debug.DrawLine(ray.origin, hitInfo.point, Color.red);
				hidden = false;
			}
		}
		else
		{
			Vector3 end = ray.origin + ray.direction * distance;
			Debug.DrawLine(ray.origin, end);
			hidden = true;
		}

		if (hidden == true)
        {
			chaseDelay = 0;
        }

		if (enemyHealth <= 0)
			Destroy(gameObject);

		if (Input.GetKey(KeyCode.LeftShift))
        {
			awareAI = 10f;
        }
        else
        {
			awareAI = 20f;
        }


		playerDistance = Vector3.Distance(player.position, transform.position);

		if (playerDistance < awareAI)
		{
			RaycastHit hit;

			Vector3 fromPosition = transform.position;
			Vector3 toPosition = player.transform.position;
			Vector3 direction = toPosition - fromPosition;

			if (Physics.Raycast(transform.position, direction, out hit, awareAI, mask))
			{
				Debug.DrawLine(ray.origin, hit.point);
				hidden = true;
			}
			else if (Physics.Raycast(transform.position, direction, out hit, awareAI, maskPlayer))
            {
				Debug.DrawLine(ray.origin, hit.point);
				hidden = false;
				LookAtPlayer();
			}
		}

		if (hidden == false)
        {
			LookAtPlayer();
        }

		if (agent.remainingDistance < 0.5f)
        {
			GotoNextPoint();
		}
		
		if (playerDistance < 4 && hidden == false)
        {
			lose = true;
        }
		
	}

	void LookAtPlayer()
	{
        transform.LookAt(player);
        Chase();
    }


	void GotoNextPoint()
	{
		anim.CrossFade("Patrol", 0.1f);

		anim.SetBool("isPatrolling", true);
		anim.SetBool("isIdle", false);
		anim.SetBool("isChasing", false);

		if (navPoint.Length == 0)
        {
			anim.CrossFade("Idle", 0.1f);

			anim.SetBool("isIdle", true);
			anim.SetBool("isChasing", false);
			anim.SetBool("isPatrolling", false);
			return;
		}
		agent.destination = navPoint[destPoint].position;
		destPoint = (destPoint + 1) % navPoint.Length;
	}


	void Chase()
	{
		chase = true;
		chaseDelay += 0.1f;
		transform.Translate(Vector3.forward * AIMoveSpeed * Time.deltaTime);

		if (playerDistance < 6 && hidden == false)
		{
			lose = true;
		}

		anim.CrossFade("Chase", 0.1f);

		anim.SetBool("isPatrolling", false);
		anim.SetBool("isIdle", false);
		anim.SetBool("isChasing", true);
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && hidden == false)
        {
			lose = true;
		}
    }
}
