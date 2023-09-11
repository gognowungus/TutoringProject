using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehavior : MonoBehaviour
{
    public float speed = 15;
    public float maxSpeed = 3;
    public float jumpHeight = 200;
    public float slowDown = 1;
    public float meleeDamage = 1;

    public GameObject sword;

    public Vector2 direction;
    public LayerMask ground;
    public LayerMask enemy;
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
        }

        moveSpeed = Vector2.Lerp(moveSpeed, Vector2.zero, Time.deltaTime * slowDown);

        Vector3 playerSpeed = rigidbody.velocity;
        playerSpeed.x = moveSpeed.x;
        playerSpeed.z = moveSpeed.y;
        rigidbody.velocity = playerSpeed;

        if(direction.magnitude > 0)
        {
            transform.forward = Vector3.Lerp(transform.forward, new Vector3(direction.x, 0, direction.y), Time.deltaTime * 8);
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

    private void OnMelee()
    {
        StopAllCoroutines();
        StartCoroutine(WeaponSwing());

        Vector3 position = transform.position + (transform.forward);
        Vector3 halfExtents = new Vector3(1f, 1f, 1f);
        RaycastHit[] hit = Physics.BoxCastAll(position, halfExtents, transform.forward, transform.rotation, 1, enemy);

        foreach(RaycastHit h in hit)
        {
            if(h.collider.GetComponent<EnemyBehavior>())
            {
                h.collider.GetComponent<EnemyBehavior>().ChangeHealth(-meleeDamage);
                h.collider.GetComponent<Rigidbody>().AddExplosionForce(500, transform.position, 3);
            }
        }
    }

    int swingDirection = 1;
    float swingSpeed = 0.25f;

    private IEnumerator WeaponSwing()
    {
        sword.transform.localRotation = Quaternion.Euler(new Vector3(0, -30 * swingDirection, 0));

        float timeElapsed = 0;

        while(timeElapsed < swingSpeed)
        {
            sword.transform.Rotate(new Vector3(0, (60 * swingDirection * Time.deltaTime) / swingSpeed, 0));
            timeElapsed += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        swingDirection *= -1;
    }
}