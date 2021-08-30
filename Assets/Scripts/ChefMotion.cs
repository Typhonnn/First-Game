using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChefMotion : MonoBehaviour
{
    private Animator animator;
    private NavMeshAgent agent;
    public Transform[] points;
    private int destination;

    // Start is called before the first frame update
    void Start()
    {
        destination = 0;
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        gotoNextPoint();
    }

    // Update is called once per frame
    void Update()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.1f && !agent.isStopped)
        {
            gotoNextPoint();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "doWork")
        {
            StartCoroutine(doWork(other));
        }
        else if(other.gameObject.tag == "NPCIdle")
        {
            StartCoroutine(idle(other, 10));
        }
    }

    void gotoNextPoint()
    {
        // No points were given
        if (points.Length == 0)
        {
            return;
        }
        animator.SetInteger("state", 1);
        // Set the next destination
        agent.SetDestination(points[destination].position);
        // update index to next destination
        destination = (destination + 1) % points.Length;
    }

    IEnumerator doWork(Collider other)
    {
        agent.isStopped = true;
        transform.rotation = other.gameObject.transform.rotation;
        // do some work
        animator.SetInteger("state", 2);
        // delay
        yield return new WaitForSeconds(5);
        // after delay
        agent.isStopped = false;
        animator.SetInteger("state", 1);
        
    }

    IEnumerator idle(Collider other, int seconds)
    {
        agent.isStopped = true;
        transform.rotation = other.gameObject.transform.rotation;
        // do some work
        animator.SetInteger("state", 0);
        // delay
        yield return new WaitForSeconds(seconds);
        // after delay
        agent.isStopped = false;
        animator.SetInteger("state", 1);
    }
}
