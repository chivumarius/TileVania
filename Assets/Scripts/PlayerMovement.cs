using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;



public class PlayerMovement : MonoBehaviour
{
    // ▼ "Serializing" the "Fields" ▼
    [SerializeField] float runSpeed = 10f;    
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float climbSpeed = 5f;
    [SerializeField] Vector2 deathKick = new Vector2 (10f, 10f);

    // ▼ "Shooting" Refrences ▼
    [SerializeField] GameObject bullet;
    [SerializeField] Transform gun;




    // ▼ "Variables" ▼
    Vector2 moveInput;
    Rigidbody2D myRigidbody;
    
    // ▼ "Referencing" the "Animator" Component ▼
    Animator myAnimator;

    // ▼ "Referencing" the "Capsule Collider 2D" Component ▼
    CapsuleCollider2D myBodyCollider;

    // ▼ "Referencing" the "Box Collider 2D" Component ▼
    BoxCollider2D myFeetCollider;

    // ▼ "Stop Sliding" on "Ladder" by Setting "Gravity Scale" ▼
    float gravityScaleAtStart;


    // ▼ "Detecting" if "Player" is "Alive" ▼
    bool isAlive = true;

    


    // ▬▬▬▬▬▬▬▬▬▬ "Start()" Method ▬▬▬▬▬▬▬▬▬▬
    void Start()
    {
        // ▼ "Getting" the "Rigidbody2D" Component ▼
        myRigidbody = GetComponent<Rigidbody2D>();

        // ▼ "Getting" the "Animator" Component ▼
        myAnimator = GetComponent<Animator>();

        // ▼ "Getting" the "Capsule Collider 2D" Component ▼    
        myBodyCollider = GetComponent<CapsuleCollider2D>();

        // ▼ "Getting" the "Box Collider 2D" Component ▼
        myFeetCollider = GetComponent<BoxCollider2D>();


        // ▼ "Setting" the "Gravity Scale" at "Start" 
        //     → to "Stop Sliding" on "Ladder" 
        //     → by Accessing the "Gravity Scale" 
        //     → from "Rigidbody2D" Component ▼
        gravityScaleAtStart = myRigidbody.gravityScale;
    }





    // ▬▬▬▬▬▬▬▬▬▬ "Update()" Method ▬▬▬▬▬▬▬▬▬▬
    void Update()
    {
        // ▼ "Checking" if "Player" is "Not Alive" ▼
        if (!isAlive) 
        { 
            // ▼ "Do Not Run this Method" ("Get Out" Of this "Method") ▼
            return; 
        }


        // ▼ "Calling" the "Methods" ▼
        Run();
        FlipSprite();
        ClimbLadder();
        Die();
    }



    // ▬▬▬▬▬▬▬▬▬▬ "On Fire()" Method ▬▬▬▬▬▬▬▬▬▬
    void OnFire(InputValue value)
    {
        // ▼ "Checking" if "Player" is "Not Alive" ▼
        if (!isAlive) 
        { 
            // ▼ "Do Not Run this Method" ("Get Out" Of this "Method") ▼
            return; 
        }
        
        // ▼ "Instantiating" the "Bullet" at "Gun" Position
        //     → by Accessing the "Transform" Component ▼
        // •► Instantiate(What, Where, How); ◄•
        Instantiate(bullet, gun.position, transform.rotation);
    }





    // ▬▬▬▬▬▬▬▬▬▬ "On Move()" Method ▬▬▬▬▬▬▬▬▬▬
    void OnMove(InputValue value)
    {
        // ▼ "Checking" if "Player" is "Not Alive" ▼
        if (!isAlive) 
        { 
            // ▼ "Do Not Run this Method" ("Get Out" Of this "Method") ▼
            return;
        }

        // ▼ "Getting" the "Vector2
        moveInput = value.Get<Vector2>();      
    }





    // ▬▬▬▬▬▬▬▬▬▬ "Climb Ladder()" Method ▬▬▬▬▬▬▬▬▬▬
    void ClimbLadder()
    {
        // ▼ "Checking" if the "Player" do mot "Touch" the "Climbing"
        if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Climbing"))) 
        { 
            // ▼ "Setting" the "Gravity Scale" Properly (of "Rigidbody2D" Component)
            //     → to the "Gravity Scale At Start" ▼ 
            myRigidbody.gravityScale = gravityScaleAtStart;

            // ▼ "Disable" the "Climbing Animation"
            //     → if "Player" is "Not Climbing" ▼
            myAnimator.SetBool("isClimbing", false);

            // ▼ "Do Not" "Climb" ("Get Out" Of this "Method") ▼
            return;
        }
        
        // ▼ "Moving" the "Player" → "Only" on "X" Axis ▼
        Vector2 climbVelocity = new Vector2 (myRigidbody.velocity.x, moveInput.y * climbSpeed);
        
        // ▼ "Setting" the "Velocity" of the "Player" for "Climbing" ▼
        myRigidbody.velocity = climbVelocity;

        // ▼ If the "Player" is "Climbing", 
        //     → then "Stop Gravity" ▼
        myRigidbody.gravityScale = 0f;


        // ▼ "Checking" if the "Player" is "Climbing/Moving" on "Y" Axis ▼
        bool playerHasVerticalSpeed = Mathf.Abs(myRigidbody.velocity.y) > Mathf.Epsilon;
        
        // ▼ "Enable" the  "Climbing Animation" 
        //     → if "Player" is "Climbing/Moving" on "Y" Axis ▼
        myAnimator.SetBool("isClimbing", playerHasVerticalSpeed);
    }




    // ▬▬▬▬▬▬▬▬▬▬ "On Jump()" Method ▬▬▬▬▬▬▬▬▬▬
    void OnJump(InputValue value)
    {
         // ▼ "Checking" if "Player" is "Not Alive" ▼
        if (!isAlive) 
        { 
            // ▼ "Do Not Run this Method" ("Get Out" Of this "Method") ▼
            return;
        }


        // ▼ "Checking" if the "Player" do mot "Touch" the "Ground"
        //     → then "Do Not" "Jump" ▼
        if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) 
        { 
            // ▼ "Do Not Jump" ("Get Out" Of this "Method") ▼
            return;
        }
        

        // ▼ "Checking" if the "Value" ("Button") is "Pressed" ▼
        if(value.isPressed)
        {
            // ▼ "Adding" the "Velocity" of the "Player" ▼
            myRigidbody.velocity += new Vector2 (0f, jumpSpeed);
        }
    }





    // ▬▬▬▬▬▬▬▬▬▬ "Run()" Method ▬▬▬▬▬▬▬▬▬▬
    void Run()
    {
        // ▼ "Moving" the "Player" → "Only" on "X" Axis ▼
        Vector2 playerVelocity = new Vector2 (moveInput.x * runSpeed, myRigidbody.velocity.y);
        
        // ▼ "Setting" the "Velocity" of the "Player" ▼
        myRigidbody.velocity = playerVelocity;


        // ▼ "Checking" if the "Player" is moving on "X" Axis ▼
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        
        // ▼ "Setting" the "Bool" of the "Animator" Component ▼
        myAnimator.SetBool("isRunning", playerHasHorizontalSpeed);
    }




    // ▬▬▬▬▬▬▬▬▬▬ "FlipSprite()" Method ▬▬▬▬▬▬▬▬▬▬
    void FlipSprite()
    {
        // ▼ "Checking" if the "Player" is moving on "X" Axis ▼
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;


        // ▼ "If" that is "True" then "Flipping" the "Sprite" on "X" Axis ▼
        if (playerHasHorizontalSpeed)
        {
            // ▼ "Accessing" the "Scale" of the "Transform" Component 
            //     → and "Flipping" the "Sprite" on "X" Axis 
            //     → if it's "Positive" or "Negative" ("Mathf.Sign()") ▼
            transform.localScale = new Vector2 (Mathf.Sign(myRigidbody.velocity.x), 1f);
        }
    }



    // ▬▬▬▬▬▬▬▬▬▬ "Die()" Method ▬▬▬▬▬▬▬▬▬▬
    void Die()
    {
        // ▼ "Checking" if the "Player Touches" the "Enemy" or "Hazard" ("Spikes")
        //     → then it will "Die" ▼
        if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemies", "Hazards")))
        {
            // ▼ "Die" the "Player" ▼
             isAlive = false;

            // ▼ "Setting" the "Trigger" Property of the "Animator" Component ▼
            myAnimator.SetTrigger("Dying");
            
            // ▼ "Setting" the "Velocity" of the "Player" ▼
            myRigidbody.velocity = deathKick;


            // ▼ "Find" the "GameSession" Object & Access" the "Method" ▼
            FindFirstObjectByType<GameSession>().ProcessPlayerDeath();      
        }
    }
}