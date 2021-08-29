using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPickup : MonoBehaviour
{
    // Reference to the character camera.
    public Camera characterCamera;
    // Reference to the slot for holding picked item.
    public Transform slot;
    public GameObject crosshairs;
    public GameObject crosshairsInteract;
    public Text itemText;
    // Reference to the currently held item.
    private PickableItem pickedItem;

    private void Update()
    {
        if (pickedItem)
        {
            itemText.text = "Press [F] to Drop";
            crosshairs.SetActive(false);
            crosshairsInteract.SetActive(true);
            itemText.gameObject.SetActive(true);
            if (Input.GetKeyDown(KeyCode.F))
            {
                DropItem(pickedItem);
            }
        }
        else if (Physics.Raycast(characterCamera.transform.position, characterCamera.transform.forward, out RaycastHit hit, 4f))
        {
            var pickable = hit.transform.GetComponent<PickableItem>();
            // If object has PickableItem class
            if (pickable)
            {
                itemText.text = "Press [F] to Pickup";
                crosshairs.SetActive(false);
                crosshairsInteract.SetActive(true);
                itemText.gameObject.SetActive(true);
                if (Input.GetKeyDown(KeyCode.F))
                {
                    // Pick it
                    PickItem(pickable);
                }
            }
        }
        else
        {
            crosshairs.SetActive(true);
            crosshairsInteract.SetActive(false);
            itemText.gameObject.SetActive(false);
        }
    }
    /// <summary>
    /// Method for picking up item.
    /// </summary>
    /// <param name="item">Item.</param>
    private void PickItem(PickableItem item)
    {
        item.itemPickedSound.Play();
        // Assign reference
        pickedItem = item;
        // Disable rigidbody and reset velocities
        item.Rb.isKinematic = true;
        item.Rb.velocity = Vector3.zero;
        item.Rb.angularVelocity = Vector3.zero;
        // Set Slot as a parent
        item.transform.SetParent(slot);
        // Reset position and rotation
        item.transform.localPosition = Vector3.zero;
        item.transform.localEulerAngles = Vector3.zero;
    }
    /// <summary>
    /// Method for dropping item.
    /// </summary>
    /// <param name="item">Item.</param>
    private void DropItem(PickableItem item)
    {
        item.itemDroppedSound.Play();
        // Remove reference
        pickedItem = null;
        // Remove parent
        item.transform.SetParent(null);
        // Enable rigidbody
        item.Rb.isKinematic = false;
        // Add force to throw item a little bit
        item.Rb.AddForce(item.transform.forward * 2, ForceMode.VelocityChange);
        // Enable Gravity
        item.Rb.useGravity = true;
    }
}
