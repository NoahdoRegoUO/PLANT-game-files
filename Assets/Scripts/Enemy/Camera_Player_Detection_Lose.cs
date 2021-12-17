using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Player_Detection_Lose : MonoBehaviour
{
	public GameObject player;

	public LayerMask mask;
	public LayerMask maskPlayer;

	private float distance;

	public bool hidden = true;

    private void Start()
    {
		hidden = true;
    }

    void Update()
	{

		Ray ray = new Ray(transform.position, transform.forward);
		RaycastHit hitInfo;

		if (Physics.Raycast(ray, out hitInfo, 50, mask))
		{
			distance = Vector3.Distance(transform.position, hitInfo.point);
			Debug.DrawLine(ray.origin, hitInfo.point, Color.green);
			hidden = true;
		}
		if (Physics.Raycast(ray, out hitInfo, distance, maskPlayer))
		{
			Debug.DrawLine(ray.origin, hitInfo.point, Color.red);
			hidden = false;
			Debug.Log("Bababooey");
		}
		else
		{
			hidden = true;
		}

	}

	private void OnTriggerStay(Collider other)
	{
		if (other.gameObject == player) //&& hidden == false)
		{
			EnemySight.lose = true;
		}
	}
}
