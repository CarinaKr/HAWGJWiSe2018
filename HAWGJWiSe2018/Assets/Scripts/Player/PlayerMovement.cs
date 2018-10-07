using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float speed;
    public float jumpHeight;
    public float maxGroundDistance;
    public PlayerManager playerManager;

    private Rigidbody2D rb;
    private Vector2 movement;
    private int jumpCount;
    private int maxJumpCount = 2;
    private bool _isGrounded;
    // private int playerNumber;
    private int playerMoveNumber;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        jumpCount = 0;
        playerMoveNumber = playerManager.playerMoveNumber;
    }

    private void Update()
    {
        if (!playerManager.isAlive)
            return;

        if(isGrounded)
        {
            jumpCount = 0;
        }

        if(Input.GetButtonDown("Jump"+playerMoveNumber)&&jumpCount<maxJumpCount)
        {
            Jump(jumpHeight);
            jumpCount++;
        }
    }

    void FixedUpdate () {
        if (!playerManager.isAlive || playerManager.isEating)
            return;

        Move(Input.GetAxis("Horizontal"+playerMoveNumber)*speed);
	}

    

    public void Move(float inputX)
    {
        rb.velocity = new Vector2(inputX, rb.velocity.y);
        if(rb.velocity.magnitude!=0 && isGrounded)
        {
            playerManager.animator.SetBool("isWalking", true);
            //Debug.Log("isWalking: true");
        }
        else
        {
            playerManager.animator.SetBool("isWalking", false);
            //Debug.Log("isWalking: false");
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
            Vector3 offset = Vector3.down * (maxGroundDistance *(3.0f / 4.0f));
            Vector3 startPosition = transform.position + offset;
            float distance = maxGroundDistance / 3;
            Debug.DrawRay(startPosition, Vector2.down * distance, Color.green);
            if (Physics2D.Raycast(startPosition, Vector2.down * maxGroundDistance, distance, LayerMask.GetMask("Platform")) && Mathf.Abs(rb.velocity.y) <=0.001f)
            {
                //Debug.Log(Mathf.Abs(rb.velocity.y));
                return true;
                
            }
            return false;
        }
    }
}
