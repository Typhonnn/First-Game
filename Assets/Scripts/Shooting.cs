using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Shooting : MonoBehaviour
{
    public GameObject gun;
    public GameObject aCamera;
    public GameObject target;
    public GameObject muzzlePoint;
    public GameObject npc;
    public AudioSource fireSound;
    private LineRenderer line;
    private Animator animator;
    private int hitCount;

    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<LineRenderer>();
        animator = npc.GetComponent<Animator>(); // NPC must have Animator component
        hitCount = 0;
    }

    IEnumerator ShowFlash()
    {
        line.SetPosition(0, muzzlePoint.transform.position);
        line.SetPosition(1, target.transform.position);
        fireSound.Play();

        yield return new WaitForSeconds(0.02f);

        line.SetPosition(0, transform.position);
        line.SetPosition(1, transform.position);
    }

    IEnumerator fallAndGetUp()
    {
        // Before delay
        NavMeshAgent agent = npc.GetComponent<NavMeshAgent>();
        agent.enabled = false;
        animator.SetInteger("state", 2);
        // Delay
        yield return new WaitForSeconds(4);
        // After delay
        animator.SetInteger("state", 0);
        yield return new WaitForSeconds(1);
        animator.SetInteger("state", 1);
        agent.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        // Shooting
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            RaycastHit hit;
            if (Physics.Raycast(aCamera.transform.position, aCamera.transform.forward, out hit))
            {
                target.GetComponent<MeshRenderer>().enabled = true;
                target.transform.position = hit.point;
                StartCoroutine(ShowFlash());
                // Check if the npc was hit
                if (npc.transform.gameObject == hit.transform.gameObject)
                {
                    hitCount++;
                    // Play animation of falling
                    if (hitCount < 2)
                    {
                        StartCoroutine(fallAndGetUp());
                    }
                    else
                    {
                        NavMeshAgent agent = npc.GetComponent<NavMeshAgent>();
                        agent.enabled = false;
                        animator.SetInteger("state", 2);
                    }
                }
            }
        }
    }
}
