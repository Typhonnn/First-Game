using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Drawer1Motion : MonoBehaviour
{
    public Animator animator;
    public GameObject crosshairs;
    public GameObject crosshairsInteract;
    public GameObject aCamera;
    public Text drawerText;
    private bool lookingAtDrawer;
    public AudioSource drawerSound;
    // Start is called before the first frame update
    void Start()
    {
        lookingAtDrawer = false;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(aCamera.transform.position, aCamera.transform.forward, out hit, 4f))
        {
            if (hit.transform.gameObject == this.gameObject)
            {
                lookingAtDrawer = true;
                if (animator.GetBool("drawerIsOpen"))
                {
                    drawerText.text = "Press [E] to Close";
                }
                else
                {
                    drawerText.text = "Press [E] to Open";
                }
                crosshairs.SetActive(false);
                crosshairsInteract.SetActive(true);
                drawerText.gameObject.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (animator.GetBool("drawerIsOpen"))
                    {
                        animator.SetBool("drawerIsOpen", false);
                    }
                    else
                    {
                        animator.SetBool("drawerIsOpen", true);
                    }
                    drawerSound.PlayDelayed(0.2f);
                }
      
            }
            else if (lookingAtDrawer)
            {
                crosshairs.SetActive(true);
                crosshairsInteract.SetActive(false);
                drawerText.gameObject.SetActive(false);
                lookingAtDrawer = false;
            }
        }
    }

}
