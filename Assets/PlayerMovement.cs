using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Vector2 movement;
    float moveSpeed = 4f;

    Rigidbody2D rb;

    public bool isGrounded; //temporarly public bcs of the bug sometimes
    public GameObject InventoryPanel;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(movement.x * moveSpeed, rb.velocity.y);
        if (Input.GetAxisRaw("Vertical") > 0f && isGrounded == true)
        {
            rb.AddForce(new Vector2(rb.velocity.x, movement.y * moveSpeed), ForceMode2D.Impulse);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground") isGrounded = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground") isGrounded = false;
    }
}
