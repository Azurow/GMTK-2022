using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Vector2 movement;

    
    private Rigidbody2D rb;

    void Start()
    {
        // A lot easier for the game engine to handle. Essentially takes the component straight from our Player
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Input
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        movement = new Vector2(moveX, moveY).normalized; //Normalizing the movement Vector prevents faster moving speeds when going diagonal

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Flip Object depending on if mouse is left or right from object
        if(mousePos.x > transform.position.x)
        {
            transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
        } else
        {
            transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
        }

        //placeholder for animation calling ~2 lines

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
