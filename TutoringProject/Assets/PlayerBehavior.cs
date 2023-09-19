using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class PlayerBehavior : MonoBehaviour
{
    public float speed = 15;
    public float maxSpeed = 3;
    public float jumpHeight = 200;
    public float slowDown = 1;
    public float meleeDamage = 1;
    public float knockBack = 500;

    public int health;
    public int maxHealth;

    public GameObject sword;
    public CinemachineVirtualCamera cinemachineCamera;

    public Vector2 direction;
    public LayerMask ground;
    public LayerMask enemy;
    public static PlayerBehavior instance;

    private bool canAttack = true;
    private bool grounded;
    private Rigidbody rigidbody;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        health = maxHealth;
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
            transform.forward = Vector3.Lerp(transform.forward, new Vector3(direction.x, 0, direction.y), Time.deltaTime * 16);
        }
    }

    public void ChangeHealth(int amount)
    {
        health = Mathf.Clamp(health + amount, 0, maxHealth);

        UIBehavior.instance.UpdateHealth(health, maxHealth);

        if(amount < 0)
        {
            StopCoroutine(nameof(ScreenShake));
            StartCoroutine(ScreenShake(0.5f));
        }

        if(health <= 0)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
        }
    }

    private IEnumerator ScreenShake(float time)
    {
        CinemachineBasicMultiChannelPerlin noise = cinemachineCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        noise.m_AmplitudeGain = 5;
        noise.m_FrequencyGain = 1;
        yield return new WaitForSeconds(time);
        noise.m_AmplitudeGain = 0;
        noise.m_FrequencyGain = 0;
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
        if(!canAttack)
        {
            return;
        }

        StopAllCoroutines();
        StartCoroutine(WeaponSwing());

        Vector3 position = transform.position + (transform.forward);
        Vector3 halfExtents = new Vector3(1f, 1f, 1f);
        RaycastHit[] hit = Physics.BoxCastAll(position, halfExtents, transform.forward, transform.rotation, 1, enemy);

        foreach(RaycastHit h in hit)
        {
            if(h.collider.GetComponent<EnemyBehavior>())
            {
                h.collider.GetComponent<EnemyBehavior>().StunEnemy();
                h.collider.GetComponent<EnemyBehavior>().ChangeHealth(-meleeDamage);
                h.collider.GetComponent<Rigidbody>().AddExplosionForce(knockBack, transform.position, 3);
            }
        }
    }

    int swingDirection = 1;
    float swingSpeed = 0.25f;

    private IEnumerator WeaponSwing()
    {
        canAttack = false;
        sword.transform.localRotation = Quaternion.Euler(new Vector3(0, -30 * swingDirection, 0));

        float timeElapsed = 0;

        while(timeElapsed < swingSpeed)
        {
            sword.transform.Rotate(new Vector3(0, (60 * swingDirection * Time.deltaTime) / swingSpeed, 0));
            timeElapsed += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        swingDirection *= -1;
        canAttack = true;
    }
}