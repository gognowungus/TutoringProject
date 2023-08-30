using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehavior : MonoBehaviour
{
    public float speed = 5;
    public float acceleration = 2;
    public float jumpHeight = 200;

    public LayerMask ground;

    private float xMove;
    private bool grounded;
    Rigidbody2D rb2d;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        grounded = Physics2D.Raycast(transform.position, Vector2.down, 1, ground);

        Vector2 v = rb2d.velocity;
        // linear interpolation
        v.x = Mathf.Lerp(v.x, xMove, Time.deltaTime * acceleration);
        rb2d.velocity = v;
    }

    private void OnMovement(InputValue iValue)
    {
        xMove = speed * iValue.Get<float>();
    }

    private void OnJump()
    {
        if(grounded)
        {
            rb2d.AddForce(Vector2.up * jumpHeight);
        }
    }
}