using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJump : MonoBehaviour
{
    Rigidbody2D myBody;
    public PlayerMoving Player;
    int maxjumps = 2,jumps;
    void Start()
    {
        myBody = GetComponent<Rigidbody2D>();
    }       
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }
    void Jump()
    {
        if (jumps > 0)
        { 
            Player.Grounded = false;
            myBody.velocity = new Vector2(0, Player.MaxJumpHeight);
            jumps = jumps - 1;
        }
        if (jumps == 0)
        {
            return;
        }
    }
    void OnCollisionEnter2D(Collision2D collide)
    {
        if (collide.gameObject.tag == "Ground"|| collide.gameObject.tag == "Step")
        {
            jumps = maxjumps;
            Player.Grounded = true;
        }
    }
}