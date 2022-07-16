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
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if(movement.magnitude > 1) movement = movement.normalized; //fixes the bug of going faster when moving diagonal

        if(mousePos.x > transform.position.x)
        {
            transform.localScale = new Vector3(0.2110226f, transform.localScale.y, transform.localScale.z);
        } else
        {
            transform.localScale = new Vector3(-0.2110226f, transform.localScale.y, transform.localScale.z);
        }

        //placeholder for animation calling ~2 lines

    }

    void FixedUpdate()
    {
        //Movement
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
