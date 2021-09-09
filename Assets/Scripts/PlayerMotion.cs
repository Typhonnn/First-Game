using UnityEngine;

public class PlayerMotion : MonoBehaviour
{
    public GameObject playerCamera;
    private CharacterController controller;
    private AudioSource footStepSound;
    private float speed, angularSpeed;
    private float rotationAboutX=0, rotationAboutY=180;

    // Start is called before the first frame update
    void Start()
    {
        speed = 5;
        angularSpeed = 100;
        controller = GetComponent<CharacterController>();
        footStepSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    //Time.deltaTime is the time that has passed from frame to frame
    void Update()
    { 
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
