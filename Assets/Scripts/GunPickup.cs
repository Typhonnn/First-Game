using UnityEngine;
using UnityEngine.UI;

public class GunPickup : MonoBehaviour
{
    public GameObject gunInDrawer;
    public GameObject gunInHand;
    public GameObject aCamera;
    public Text gunPickupText;

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(aCamera.transform.position, aCamera.transform.forward, out hit, 4f))
        {
            // The view is focused on the gun
            if (hit.transform.gameObject == gunInDrawer.gameObject)
            {
                gunPickupText.gameObject.SetActive(true);
                if (Input.GetKeyDown(KeyCode.F))
                {
                    gunInDrawer.SetActive(false);
                    gunInHand.SetActive(true);
                }
            }
            else
            {
                gunPickupText.gameObject.SetActive(false);
            }
        }
    }
}
