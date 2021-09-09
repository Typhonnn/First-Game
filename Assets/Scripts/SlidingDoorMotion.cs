using UnityEngine;

public class SlidingDoorMotion : MonoBehaviour
{
    private Animator animator;
    private AudioSource doorSound;
    private int numInside;
    // Start is called before the first frame update
    void Start()
    {
        numInside = 0;
        animator = GetComponent<Animator>(); //THIS must have a component Animator
        doorSound = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        numInside++;
        if (numInside == 1)
        {
            doorSound.Play();
            animator.SetBool("Open", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        numInside--;
        if (numInside == 0)
        {
            doorSound.Play();
            animator.SetBool("Open", false);
        }
    }
}
