using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ClickToMove : MonoBehaviour
{

    private Animator animator;
    private NavMeshAgent navMeshAgent;

    private bool moving;

    void Start()
    {
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100))
            {
                navMeshAgent.SetDestination(hit.point);
            }
        }
    }

    private void FixedUpdate()
    {
        if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
        {
            moving = false;
        }
        else
        {
            moving = true;
        }
        Debug.Log(navMeshAgent.velocity.magnitude);

        animator.SetBool("Moving", moving);
        animator.SetFloat("Velocity Z", navMeshAgent.velocity.magnitude);
    }
}
