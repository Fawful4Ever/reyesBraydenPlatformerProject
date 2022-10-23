using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

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
    public float Health = 5;
    public TMP_Text HealthText;
    public static bool Dead = false;
    public static bool SwitchOn = false;
    // Start is called before the first frame update
    void Start()
    {
        canJump = true;
        canDoubleJump = true;
        //Application.targetFrameRate = 10;
    }

    // Update is called once per frame
    private void Update()
    {
        bool shouldJump = (Input.GetKeyDown(KeyCode.W));
            
            //Jump
            if (shouldJump && canJump && Dead == false)
            {
                rb2D.velocity = Vector2.zero;
                rb2D.AddForce(jumpForce);
                canJump = false;
            }
            //Double Jump
            else if (CanWallJump == false && Dead == false)
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
        if (Dead == false)
        {
            float xMove = Input.GetAxis("MoveX");
            Vector2 newPosition = gameObject.transform.position;
            newPosition.x += xMove * speed * Time.deltaTime;
            gameObject.transform.position = newPosition;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Dead == false)
        {
            if (collision.gameObject.tag == "Hazard")
            {
                TakeDamage();
            }
            if (collision.gameObject.tag == "Water")
            {
                Death();
            }
            if (collision.gameObject.tag == "RefreshJump")
            {
                canJump = true;
                canDoubleJump = true;
            }
            if (collision.gameObject.tag == "Switch")
            {
                SwitchOn = true;
            }
        }
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
        if (shouldJump && CanWallJump && wallLeftTouch && Dead == false)
        {
            rb2D.velocity = Vector2.zero;
            rb2D.AddForce(wallJumpLeftForce);
            CanWallJump = false;
        }
    }
    private void WallJumpRight()
    {
        bool shouldJump = (Input.GetKeyDown(KeyCode.W));
        if (shouldJump && CanWallJump && wallRightTouch && Dead == false)
        {
            rb2D.velocity = Vector2.zero;
            rb2D.AddForce(wallJumpRightForce);
            CanWallJump = false;
        }
    }
    public void TakeDamage()
    {
        Health -= 1;
        HealthText.text = "HP = " + Health;
        if (Health <= 0)
        {
            Death();
        }
    }

    public void Death()
    {
        Health = 0;
        HealthText.text = "HP = " + Health;
        Dead = true;
    }
}
