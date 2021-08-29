using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorMotion : MonoBehaviour
{
    private Animator animator;
    private AudioSource doorSqueak;
    private int numInside;
    // Start is called before the first frame update
    void Start()
    {
        numInside = 0;
        animator = GetComponent<Animator>(); //THIS must have a component Animator
        doorSqueak = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        numInside++;
        if (numInside == 1)
        {
            animator.SetBool("DoorIsOpenning", true);
            doorSqueak.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        numInside--;
        if (numInside == 0)
        {
            animator.SetBool("DoorIsOpenning", false);
            doorSqueak.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
