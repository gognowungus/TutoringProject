using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehavior : MonoBehaviour
{
    public float speed = 5;
    public float acceleration = 2;
    public float jumpHeight = 200;

    public float direction = 0;

    public LayerMask ground;

    public static PlayerBehavior instance;

    private float xMove;
    private bool grounded;
    private Rigidbody2D rb2d;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        grounded = Physics2D.Raycast(transform.position, Vector2.down, 1, ground);

        Vector2 v = rb2d.velocity;
        v.x = Mathf.Lerp(v.x, xMove, Time.deltaTime * acceleration);
        rb2d.velocity = v;
    }

    private void OnMovement(InputValue iValue)
    {
        direction = iValue.Get<float>();
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