using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [HideInInspector]public float moveSpeed = 5f;
    public float maxSpeed = 5f;
    private Vector2 movement;
    public Animator animator;
    
    public AudioSource footsteps;

    private Rigidbody2D rb;

    void Start()
    {
        // A lot easier for the game engine to handle. Essentially takes the component straight from our Player
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        if(Time.timeScale < 1f) return;

        //Input
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        movement = new Vector2(moveX, moveY).normalized; //Normalizing the movement Vector prevents faster moving speeds when going diagonal


        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //Sets the positive or negative value for horizontal and vertical movement animation (from 1 to -1) as well as speed (greater or less than 0.01)
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

        if(Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 ||
            Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical") == -1)
        {
            animator.SetFloat("last_Horizontal", Input.GetAxisRaw("Horizontal"));
            animator.SetFloat("last_Vertical", Input.GetAxisRaw("Vertical"));
        }
        
        if(movement != Vector2.zero)
        {
            footsteps.UnPause();
        } else
        {
            footsteps.Pause();
        }

    }

    void FixedUpdate()
    {
        //Movement
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    public void AdjustSpeed(float newSpeed)
    {
        moveSpeed = newSpeed;
    }
}
