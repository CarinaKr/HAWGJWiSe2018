using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float speed;
    public float jumpHeight;
    public float maxGroundDistance;
    public PlayerManager playerManager;

    private Animator animator;
    private Rigidbody2D rb;
    private Vector2 movement;
    private int jumpCount;
    private int maxJumpCount = 2;
    private bool _isGrounded;
    private int playerNumber;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        jumpCount = 0;
        playerNumber = playerManager.playerNumber;
	}

    private void Update()
    {
        if (!playerManager.isAlive)
            return;

        if(isGrounded)
        {
            jumpCount = 0;
        }

        if(Input.GetButtonDown("Jump"+playerNumber)&&jumpCount<maxJumpCount)
        {
            Jump(jumpHeight);
            jumpCount++;
        }
    }

    void FixedUpdate () {
        if (!playerManager.isAlive)
            return;

        Move(Input.GetAxis("Horizontal"+playerNumber)*speed);
	}

    

    public void Move(float inputX)
    {
        rb.velocity = new Vector2(inputX, rb.velocity.y);
        if(rb.velocity.magnitude!=0 && isGrounded)
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
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
                return true;
            }
            return false;
        }
    }
}
