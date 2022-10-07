using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float speed = 8;
    public Rigidbody2D rb2D;
    private Vector2 jumpForce = new Vector2(0, 800);
    private bool canJump = true;
    private bool canDoubleJump = true;
    private Vector2 wallJumpRightForce = new Vector2(-450, 800);
    private Vector2 wallJumpLeftForce = new Vector2(450, 800);
    public bool CanWallJump;
    private bool wallLeftTouch;
    private bool wallRightTouch;
    // Start is called before the first frame update
    void Start()
    {
        canJump = true;
        canDoubleJump = true;
    }

    // Update is called once per frame
    private void Update()
    {
        bool shouldJump = (Input.GetKeyDown(KeyCode.W));

            if (shouldJump && canJump)
            {
                rb2D.velocity = Vector2.zero;
                rb2D.AddForce(jumpForce);
                canJump = false;
            }
            else if (CanWallJump == false)
            {
                if (shouldJump && canDoubleJump)
                {
                    rb2D.velocity = Vector2.zero;
                    rb2D.AddForce(jumpForce);
                    canDoubleJump = false;
                }
            }
            WallJumpLeft();
            WallJumpRight();
    }
    void FixedUpdate()
    {
        float xMove = Input.GetAxis("MoveX");
        Vector2 newPosition = gameObject.transform.position;
        newPosition.x += xMove * speed * Time.deltaTime;
        gameObject.transform.position = newPosition;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        canJump = true;
        canDoubleJump = true;
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "WallRight")
        {
            CanWallJump = true;
            wallRightTouch = true;
        }

        if (collision.gameObject.tag == "WallLeft")
        {
            CanWallJump = true;
            wallLeftTouch = true;
        }
        Debug.Log(collision.gameObject.name);
    }


    public void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "WallRight")
        {
            CanWallJump = false;
            wallRightTouch = false;
        }

        if (collision.gameObject.tag == "WallLeft")
        {
            CanWallJump = false;
            wallLeftTouch = false;
        }
    }

    private void WallJumpLeft()
    {
        bool shouldJump = (Input.GetKeyDown(KeyCode.W));
        if (shouldJump && canDoubleJump && CanWallJump && wallLeftTouch)
        {
            rb2D.velocity = Vector2.zero;
            rb2D.AddForce(wallJumpLeftForce);
            CanWallJump = false;
        }
    }
    private void WallJumpRight()
    {
        bool shouldJump = (Input.GetKeyDown(KeyCode.W));
        if (shouldJump && canDoubleJump && CanWallJump && wallRightTouch)
        {
            rb2D.velocity = Vector2.zero;
            rb2D.AddForce(wallJumpRightForce);
            CanWallJump = false;
        }
    }
}
