using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Vector3 offset;
    [SerializeField][Range(1, 10)] float smoothFactor = 3;
    [SerializeField] Vector3 minValue, maxValue;

    void FixedUpdate()
    {
        Follow();
    }

    void Follow()
    {
        Vector3 targetPosition = target.position + offset;
        Vector3 boundPosition = new Vector3(
            Mathf.Clamp(targetPosition.x, minValue.x, maxValue.x),  //bound x value around the min and the max
            Mathf.Clamp(targetPosition.y, minValue.y, maxValue.y),  //bound y value around the min and the max
            Mathf.Clamp(targetPosition.z, minValue.z, maxValue.z)   //bound z value around the min and the max
            );
        Vector3 smoothPosition = Vector3.Lerp(transform.position, boundPosition, Time.fixedDeltaTime);
        transform.position = smoothPosition;
    }
}
