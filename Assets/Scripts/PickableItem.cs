using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PickableItem : MonoBehaviour
{
    // Reference to the rigidbody
    private Rigidbody rb;
    public Rigidbody Rb => rb;
    public AudioSource itemPickedSound;
    public AudioSource itemDroppedSound;

    private void Start()
    {
        // Get reference to the rigidbody
        rb = GetComponent<Rigidbody>();
    }
}