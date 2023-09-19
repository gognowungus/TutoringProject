using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour
{
    public ParticleSystem hitParticles;
    public float maxHealth;
    public float pursuitDistance;
    public float attackDelay;
    public float attackRange;
    public int damage;
    public LayerMask ground;
    public LayerMask playerLayer;

    bool stunned;
    float health;
    float delayLeft;
    NavMeshAgent agent;
    Rigidbody rigidbody;
    Transform player;

    void Start()
    {
        health = maxHealth;
        delayLeft = attackDelay;
        agent = GetComponent<NavMeshAgent>();
        rigidbody = GetComponent<Rigidbody>();
        player = FindAnyObjectByType<PlayerBehavior>().transform;
    }

    void Update()
    {
        if(stunned)
        {
            if(!Physics.Raycast(transform.position, Vector3.down, 6, ground))
            {
                Death();
            }

            delayLeft = attackDelay;
            return;
        }

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance < pursuitDistance)
        {
            agent.isStopped = false;
            agent.SetDestination(player.position);
            delayLeft -= Time.deltaTime;

            if(delayLeft <= 0 && distance <= attackRange)
            {
                Attack();
                delayLeft = attackDelay;
            }
        }
        else if(!agent.isStopped)
        {
            delayLeft = attackDelay;
            agent.isStopped = true;
        }
    }

    public void Attack()
    {
        print("attacked");
        Vector3 position = transform.position + (transform.forward);
        Vector3 halfExtents = Vector3.one * attackRange;
        RaycastHit[] hit = Physics.BoxCastAll(position, halfExtents, transform.forward, transform.rotation, 1, playerLayer);

        foreach (RaycastHit h in hit)
        {
            if(h.collider.GetComponent<PlayerBehavior>())
            {
                h.collider.GetComponent<PlayerBehavior>().ChangeHealth(-damage);
            }
        }
    }

    public void ChangeHealth(float amount)
    {
        health = Mathf.Clamp(health + amount, 0, maxHealth);

        if(amount < 0)
        {
            hitParticles.Play();
        }

        if(health <= 0)
        {
            Death();
        }
    }

    public void Death()
    {
        // particles
        // sound effect
        // add score
        Destroy(gameObject);
    }

    public void StunEnemy()
    {
        stunned = true;
        StopCoroutine(BeginStunned());
        StartCoroutine(BeginStunned());
    }

    private IEnumerator BeginStunned()
    {
        agent.enabled = false;
        rigidbody.isKinematic = false;

        yield return new WaitForSeconds(0.5f);

        rigidbody.velocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;
        agent.enabled = true;
        rigidbody.isKinematic = true;
        stunned = false;
    }
}
