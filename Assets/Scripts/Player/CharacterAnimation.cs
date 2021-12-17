using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        //Calls animator component
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Idle if no keys are pressed
        if(!Input.anyKey)
        {
            anim.SetBool("isIdle", true);
            anim.SetBool("isRunning", false);
            anim.SetBool("isCrouching", false);
            anim.SetBool("isCrouchWalking", false);
        }
        else
        {
            anim.SetBool("isIdle", false);
        }

        //if input WASD and Shift is not held down, then crossfade to run animation
        if (((Input.GetKeyDown(KeyCode.W)) || (Input.GetKeyDown(KeyCode.S)) || (Input.GetKeyDown(KeyCode.A)) || (Input.GetKeyDown(KeyCode.D))) && (!Input.GetKey(KeyCode.LeftShift)))
        {
            anim.CrossFade("Running", 0.1f);
        }

        //if input WASD and Shift is not held down, then play run animation
        if (((Input.GetKey(KeyCode.W)) || (Input.GetKey(KeyCode.S)) || (Input.GetKey(KeyCode.A)) || (Input.GetKey(KeyCode.D))) && (!Input.GetKey(KeyCode.LeftShift)))
        {
            anim.SetBool("isRunning", true);
            anim.SetBool("isIdle", false);
            anim.SetBool("isCrouching", false);
            anim.SetBool("isCrouchWalking", false);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }

        //if input is LeftShift, then crossfade to crouch animation
        if (Input.GetKeyDown(KeyCode.LeftShift)) 
        {
            anim.CrossFade("Crouch", 0.1f);

        }

        //if input is LeftShift, then play crouch animation
        if (Input.GetKey(KeyCode.LeftShift))
        {
            anim.SetBool("isCrouching", true);
            anim.SetBool("isIdle", false);
            anim.SetBool("isRunning", false);
        }
        else
        {
            anim.SetBool("isCrouching", false);
        }

        //Check if character is walking and crouching at the same time
        if ((Input.GetKeyDown(KeyCode.LeftShift)) && ((Input.GetKey(KeyCode.W)) || (Input.GetKey(KeyCode.S)) || (Input.GetKey(KeyCode.A)) || (Input.GetKey(KeyCode.D))))
        {
            anim.CrossFade("CrouchWalk", 0.1f);

        }

        //If character is crouching and walking, then crouchwalk
        if ((Input.GetKey(KeyCode.LeftShift)) && ((Input.GetKeyDown(KeyCode.W)) || (Input.GetKeyDown(KeyCode.S)) || (Input.GetKeyDown(KeyCode.A)) || (Input.GetKeyDown(KeyCode.D))))
        {
            anim.SetBool("isCrouchWalking", true);
            anim.SetBool("isIdle", false);
            anim.SetBool("isRunning", false);
            anim.SetBool("isCrouching", false);
        }
        else if (Input.GetKey(KeyCode.LeftShift))
        {
            anim.SetBool("isCrouching", true);
        }
        else
        {
            anim.SetBool("isCrouching", false);
        }
    }
}
