using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MutantMotion : MonoBehaviour
{
    private Animator animator;
    private NavMeshAgent agent;
    public GameObject player; // is a target
    private LineRenderer line;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetInteger("state", 1);
        agent = GetComponent<NavMeshAgent>();
        line = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.enabled)
        {
            agent.SetDestination(player.transform.position); // Runs A* to find the path to target
            // Drawing this path
            
            line.positionCount = agent.path.corners.Length;
            for(int i = 0; i<agent.path.corners.Length; i++)
            {
                line.SetPosition(i, agent.path.corners[i]);
            }
            
        }
        /*
        if (Input.GetKeyDown(KeyCode.Z)) // Goes to idle
        {
            animator.SetInteger("state", 0);
        }else if (Input.GetKeyDown(KeyCode.X)) // Goes to walking
        {
            animator.SetInteger("state", 1);
        }
        else if (Input.GetKeyDown(KeyCode.C)) // Goes to dying
        {
            animator.SetInteger("state", 2);
        }
        else if (Input.GetKeyDown(KeyCode.V)) // Goes to jumping
        {
            animator.SetInteger("state", 3);
        }
        */
    }
}
