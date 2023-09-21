using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoltBehavior : MonoBehaviour
{
    public float spinSpeed = 30;
    void Start()
    {
        
    }

    void Update()
    {
        transform.Rotate(new Vector3(0, spinSpeed, 0) * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
