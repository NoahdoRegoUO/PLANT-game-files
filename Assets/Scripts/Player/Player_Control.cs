using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class Player_Control : MonoBehaviour
{
	public CharacterController controller;

	public static string status;

	public float height;

	//Camera variables
	public Transform cam; 

	//General Movement variables
	public float speed = 10f;
	public float turnSmoothTime = 0.1f;

	public UnityEngine.Vector3 lastPlayerSighting;
	public UnityEngine.Vector3 resetPosition = new UnityEngine.Vector3(1000f, 1000f, 1000f);
	public UnityEngine.Vector3 position = new UnityEngine.Vector3(1000f, 1000f, 1000f);

	private Rigidbody Rb;

	float turnSmoothVelocity;

	void Start()
	{
		controller = gameObject.GetComponent<CharacterController>();
		Rb = GetComponent<Rigidbody>();
		resetPosition = transform.position;
		status = "Hidden";
	}

	// Update is called once per frame
	void Update()
	{
		float horizontal = Input.GetAxisRaw("Horizontal");
		float vertical = Input.GetAxisRaw("Vertical");
		lastPlayerSighting = transform.position;

		if (transform.position.y != height)
        {
            transform.position = new UnityEngine.Vector3(transform.position.x, height, transform.position.z);
        }

		UnityEngine.Vector3 direction = new UnityEngine.Vector3(horizontal, 0f, vertical).normalized; 
		if (direction.magnitude >= 0.1f)
		{
			float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
			float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
			transform.rotation = UnityEngine.Quaternion.Euler(0f, angle, 0f);
			UnityEngine.Vector3 moveDir = UnityEngine.Quaternion.Euler(0f, targetAngle, 0f) * UnityEngine.Vector3.forward;

			controller.Move(moveDir.normalized * speed * Time.deltaTime);
		}

		if (Input.GetKey(KeyCode.LeftShift))
		{
			controller.height = 8.0f;
			speed = 6f;
		}
		else
		{
			controller.height = 15.4f;
			speed = 12f;
        }

		if (Camera_Player_Detection.hidden == false || EnemySight.hidden == false)
        {
			status = "Spotted";
        }
		else
        {
			status = "Hidden";
        }

    }
		
}