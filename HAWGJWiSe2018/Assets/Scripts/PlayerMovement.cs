using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float speed;
    public float jumpHeigt;
    public float maxGroundDistance;

    private Rigidbody2D rb;
    private Vector2 movement;
    private int jumpCount;
    private int maxJumpCount = 2;
    private bool _isGrounded;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        jumpCount = 0;
	}

    private void Update()
    {
        if(isGrounded)
        {
            jumpCount = 0;
        }

        if(Input.GetButtonDown("Jump")&&jumpCount<maxJumpCount)
        {
            Jump(jumpHeigt);
            jumpCount++;
        }
    }

    void FixedUpdate () {
        Move(Input.GetAxis("Horizontal")*speed);
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if(collision.transform.tag=="Platform")
        //{
        //    jumpCount = 0;
        //}
    }

    public void Move(float inputX)
    {
        rb.velocity = new Vector2(inputX, rb.velocity.y);
    }
    
    public void Jump(float heigt)
    {
        rb.velocity = new Vector2(rb.velocity.x,heigt);
    }

    public bool isGrounded
    {
        get
        {
            Debug.DrawRay(transform.position, Vector2.down * maxGroundDistance, Color.green);
            if (Physics2D.Raycast(transform.position, Vector2.down * maxGroundDistance, maxGroundDistance, LayerMask.GetMask("Platform")))
            {
                Debug.Log("isGrounded");
                return true;
            }
            return false;
        }
    }
}
