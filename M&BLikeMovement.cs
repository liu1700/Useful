// With the help of http://www.ootii.com/Unity/CameraController/CCUsersGuide.pdf
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    GameObject currentPlayer;
    private bool moving;
    private Animator animator;


    void Start()
    {
        animator = GetComponent<Animator>();
        currentPlayer = GameObject.FindGameObjectWithTag("Player");
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W))
        {
            currentPlayer.transform.position += Camera.main.transform.forward * 5 * Time.deltaTime;

            float step = 5 * Time.deltaTime;

            Vector3 newDir = Vector3.RotateTowards(transform.forward, Camera.main.transform.forward, step, 0.0f);

            currentPlayer.transform.rotation = Quaternion.LookRotation(newDir);

            moving = true;
        }
        else
        {
            moving = false;
        }
    }

    void Update()
    {
        if (moving)
        {
            animator.SetFloat("Velocity Z", 5);
        }
        animator.SetBool("Moving", moving);
    }
}
