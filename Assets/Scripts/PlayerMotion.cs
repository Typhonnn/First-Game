using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotion : MonoBehaviour
{
    private float speed, angularSpeed;
    private CharacterController controller;
    private float rotationAboutX=0, rotationAboutY=180;
    public GameObject playerCamera;
    private AudioSource footStepSound;
    // Start is called before the first frame update
    void Start()
    {
        speed = 5;
        angularSpeed = 100;
        controller = GetComponent<CharacterController>();
        footStepSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    { //Time.deltaTime is the time that has passed from frame to frame
        float dx, dy = -1, dz; // dy=-1 is gravity

        // Player rotation
        rotationAboutY += Input.GetAxis("Mouse X") * angularSpeed * Time.deltaTime;
        transform.localEulerAngles = new Vector3(0, rotationAboutY, 0);

        // Camera rotation
        rotationAboutX -= Input.GetAxis("Mouse Y") * angularSpeed * Time.deltaTime;
        playerCamera.transform.localEulerAngles = new Vector3(rotationAboutX, 0, 0);

        // Motion after rotation
        dz = Input.GetAxis("Vertical"); // can be one of: -1,0,1
        dz *= speed * Time.deltaTime;
        dx = Input.GetAxis("Horizontal"); // can be one of: -1,0,1
        dx *= speed * Time.deltaTime;

        // Example of simple motion:
        // Vector3 = (x,z,y)
        // forward/backward
        //this.transform.Translate(new Vector3(0, 0, dz * speed * Time.deltaTime));
        // left/right
        //this.transform.Translate(new Vector3(dx * speed * Time.deltaTime, 0, 0));

        // Motion using CharacterController
        Vector3 motion = new Vector3(dx, dy, dz); // Motion is defined in local coordinates
        motion = transform.TransformDirection(motion); // Now Motion is defined in global coordinates
        controller.Move(motion); // Must recieve Vector3 in global coordinate

        // Add footstep sound effect
        if (dz != 0f || dx != 0)
        {
            if (!footStepSound.isPlaying)
            {
                footStepSound.Play();
            }
        }
        else
        {
            footStepSound.Stop();
        }
    }
}
