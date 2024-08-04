using System.Collections.Generic;
using UnityEngine;



public class CoinPickup : MonoBehaviour
{ 
    [SerializeField] AudioClip coinPickupSFX;
    [SerializeField] int pointsForCoinPickup = 100;

    // ▼ Preventing "Double Collecting" of "Coins" ▼
    bool wasCollected = false;




    // ▬▬▬▬▬▬▬▬▬▬ "On Trigger Enter 2D()" Method ▬▬▬▬▬▬▬▬▬▬
    void OnTriggerEnter2D(Collider2D other) 
    {
        // ▼ "Checking" if the "Coin Collides" with "Player" ▼
        if (other.tag == "Player" && !wasCollected)
        {
            // ▼ "Enabling" the "Coin Pickup" ▼
            wasCollected = true;

            // ▼ "Adding" "Points" to "Player" ▼
            FindFirstObjectByType<GameSession>().AddToScore(pointsForCoinPickup);

            // ▼ "Playing" the "Coin Pickup SFX" at "Camera" Position ▼
            AudioSource.PlayClipAtPoint(coinPickupSFX, Camera.main.transform.position);

            // ▼ "Destroying" the "Coin Pickup" ▼
            gameObject.SetActive(false);

            // ▼ "Destroying" the "Coin" ▼
            Destroy(gameObject);
        }
    }
}
