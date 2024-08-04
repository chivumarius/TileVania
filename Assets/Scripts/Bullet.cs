using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Bullet : MonoBehaviour
{
    // ▼ "Reference Variables" ▼
    [SerializeField] float bulletSpeed = 20f;
    
    
    Rigidbody2D myRigidbody;
    PlayerMovement player;
    float xSpeed;
    



    // ▬▬▬▬▬▬▬▬▬▬ "Start()" Method ▬▬▬▬▬▬▬▬▬▬
    void Start()
    {
        // ▼ "Getting" the "Rigidbody2D" Component ▼
        myRigidbody = GetComponent<Rigidbody2D>();
        
        // ▼ "Finding" the "Player Movement" Component ▼
        player = FindAnyObjectByType<PlayerMovement>();
        
        // ▼ "Changing" the "Bullet Direction" on "X" Axis 
        //     → to the "Player Direction" ("Left"/"Right") ▼
        xSpeed = player.transform.localScale.x * bulletSpeed;
    }




    // ▬▬▬▬▬▬▬▬▬▬ "Update()" Method ▬▬▬▬▬▬▬▬▬▬
    void Update()
    {
        // ▼ "Moving" the "Bullet" → "Only" on "X" Axis ▼
        myRigidbody.velocity = new Vector2 (xSpeed, 0f);
    }




    // ▬▬▬▬▬▬▬▬▬▬ "On Trigger Enter 2D()" Method ▬▬▬▬▬▬▬▬▬▬
    void OnTriggerEnter2D(Collider2D other) 
    {
        // ▼ "Checking" if the "Bullet Collides" with "Enemy" ▼
        if(other.tag == "Enemy")
        {
            // ▼ "Destroying" the "Enemy" ▼
            Destroy(other.gameObject);
        }

        // ▼ "Destroying" the "Bullet" ▼
        Destroy(gameObject);
    }




    // ▬▬▬▬▬▬▬▬▬▬ "On Collision Enter 2D()" Method ▬▬▬▬▬▬▬▬▬▬
    void OnCollisionEnter2D(Collision2D other) 
    {
        // ▼ "Destroying" the "Bullet" ▼
        Destroy(gameObject);    
    }
}
