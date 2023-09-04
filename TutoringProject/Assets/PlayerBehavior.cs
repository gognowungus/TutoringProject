using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehavior : MonoBehaviour
{
    public float speed = 15;
    public float maxSpeed = 3;
    public float jumpHeight = 200;

    public Vector2 direction;
    public LayerMask ground;
    public static PlayerBehavior instance;

    private bool grounded;
    private Rigidbody rigidbody;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, 1, ground);

        rigidbody.AddForce(new Vector3(direction.x, 0, direction.y) * speed);

        Vector2 moveSpeed = new Vector2(rigidbody.velocity.x, rigidbody.velocity.z);

        if(moveSpeed.magnitude > maxSpeed)
        {
            moveSpeed = moveSpeed.normalized * maxSpeed;
            Vector3 playerSpeed = rigidbody.velocity;
            playerSpeed.x = moveSpeed.x;
            playerSpeed.z = moveSpeed.y;
            rigidbody.velocity = playerSpeed;
        }
    }

    private void OnMovement(InputValue iValue)
    {
        direction = iValue.Get<Vector2>();
    }

    private void OnJump()
    {
        if (grounded)
        {
            rigidbody.AddForce(Vector3.up * jumpHeight);
        }
    }
}