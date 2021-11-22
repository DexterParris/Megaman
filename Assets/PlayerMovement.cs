using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    public GameObject prefab;


    const string player_idle = "Idle";
    const string player_run = "Run";
    const string player_fall = "Falling";

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        DoJump();
        DoMove();

    }

    void DoJump()
    {
        float xpos = transform.position.x;
        float ypos = transform.position.y;
        float yvelocity = rb.velocity.y;
        float xvelocity = rb.velocity.x;

        Helper.DoRayCollisionCheck(gameObject, xpos, ypos);


        // check for jump
        if (Input.GetKeyDown("space"))
        {
            
            if (Helper.IsGrounded == true)
            {
                Helper.IsGrounded = false;
                Helper.SetVelocity(gameObject, 0, 7);
                anim.Play(player_fall);
            }
        }
    }

    void DoMove()
    {
        float yvelocity = rb.velocity.y;
        // stop player sliding when not pressing left or right
        Helper.SetVelocity(gameObject, 0, yvelocity);
        // check for moving left
        if (Input.GetKey("a"))
        {
            Helper.FlipSprite(gameObject, true);
            Helper.SetVelocity(gameObject, -4, 0);
            if (Helper.IsGrounded == true)
            {
                anim.Play(player_run);
            }
        }
        else if (Input.GetKey("d"))
        {
            Helper.FlipSprite(gameObject, false);    //flip sprite left
            Helper.SetVelocity(gameObject, 4, 0);
            if (Helper.IsGrounded == true)
            {
                anim.Play(player_run);
            }
        }
        else if (Helper.IsGrounded == true)
        {
            anim.Play(player_idle);
        }

    }


}
