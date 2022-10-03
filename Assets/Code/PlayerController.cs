using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float speed = 6;
    public Rigidbody2D rb2D;
    private Vector2 jumpForce = new Vector2(0, 1000);
    private bool canJump = true;
    private bool canDoubleJump = true;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float xMove = Input.GetAxis("MoveX");
        Vector2 newPosition = gameObject.transform.position;
        newPosition.x += xMove * speed * Time.deltaTime;
        //newPosition.x = Mathf.Clamp(newPosition.x, -12f, 12f);
        gameObject.transform.position = newPosition;

        bool shouldJump = (Input.GetKeyUp(KeyCode.Space));
        if (shouldJump && canJump)
        {
            rb2D.velocity = Vector2.zero;
            rb2D.AddForce(jumpForce);
            canJump = false;
        }
        else if (shouldJump && canDoubleJump)
        {
            rb2D.velocity = Vector2.zero;
            rb2D.AddForce(jumpForce);
            canDoubleJump = false;
        }
    }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            canJump = true;
            canDoubleJump = true;
        }
}
