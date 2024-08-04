using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class EnemyMovement : MonoBehaviour
{
    // ▼ "Serialize Field" Attribute 
    //      → to be able to "See It" in the "Inspector" ▼
    [SerializeField] float moveSpeed = 1f;
    
    // ▼ "Reference" to the "Rigidbody2D" Component ▼
    Rigidbody2D myRigidbody;
    
    


    // ▬▬▬▬▬▬▬▬▬▬ "Start()" Method ▬▬▬▬▬▬▬▬▬▬
    void Start()
    {
        // ▼ "Getting" the "Rigidbody2D" Component ▼
        myRigidbody = GetComponent<Rigidbody2D>();
    }




    // ▬▬▬▬▬▬▬▬▬▬ "Update()" Method ▬▬▬▬▬▬▬▬▬▬
    void Update()
    {
        // ▼ "Moving" → "Only" on "X" Axis (not "Up" or "Down") ▼
        myRigidbody.velocity = new Vector2 (moveSpeed, 0f);
    }




    // ▬▬▬▬▬▬▬▬▬▬ "OnTriggerExit2D()" Method ▬▬▬▬▬▬▬▬▬▬
    void OnTriggerExit2D(Collider2D other) 
    {
        // ▼ "Changing" the "Enimy Movement" 
        //      → in to "Opposit Direction" 
        //      → when it "Hits" the "Wall" ▼
        moveSpeed = -moveSpeed;
        
        // ▼ "Calling" the "Method" ▼
        FlipEnemyFacing();
    }




    // ▬▬▬▬▬▬▬▬▬▬ "FlipEnemyFacing()" Method ▬▬▬▬▬▬▬▬▬▬
    void FlipEnemyFacing()
    {
        // ▼ "Flipping" the "Sprite" on "X" Axis
        //     → if it's "Positive" or "Negative" ("Mathf.Sign()")
        //     → when it "Hits" the "Wall" ▼
        transform.localScale = new Vector2 (-(Mathf.Sign(myRigidbody.velocity.x)), 1f);
    }
}
