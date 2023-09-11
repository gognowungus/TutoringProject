using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public float maxHealth;
    float health;

    void Start()
    {
        health = maxHealth;
    }

    void Update()
    {
        
    }

    public void ChangeHealth(float amount)
    {
        health = Mathf.Clamp(health + amount, 0, maxHealth);

        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
