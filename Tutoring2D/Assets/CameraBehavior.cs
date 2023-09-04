using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    public float followDistance = 4;
    public float followSpeed = 2;
    public float directionMod = 4;

    void FixedUpdate()
    {
        Vector3 followPosition = PlayerBehavior.instance.transform.position;
        followPosition.z = -10;
        float distance = Vector3.Distance(transform.position, followPosition);

        if(distance >= followDistance)
        {
            followPosition += (Vector3.right * PlayerBehavior.instance.direction * directionMod);
            transform.position = Vector3.Lerp(transform.position, followPosition, followSpeed * Time.deltaTime);
        }
    }
}