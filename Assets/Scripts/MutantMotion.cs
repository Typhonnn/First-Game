using UnityEngine;
using UnityEngine.AI;

public class MutantMotion : MonoBehaviour
{
    public GameObject player; // is a target
    private Animator animator;
    private NavMeshAgent agent;
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
    }
}
