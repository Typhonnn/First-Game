using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // For Text

public class CoinPickup : MonoBehaviour
{
    public AudioSource pickupSound;
    public Text coinsText;
    public static int numCoins;

    // Start is called before the first frame update
    void Start()
    {
        numCoins = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        numCoins++;
        this.gameObject.SetActive(false); // Turn off the coin
        pickupSound.Play();
        coinsText.text = "Gold: " + numCoins;
    }
}
