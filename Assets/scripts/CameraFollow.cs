using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform target;

    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    private float nextTimeToSearch = 0;

    public void LateUpdate()
    {
        if (target == null)
        {
            FindPlayer();
            return;
        }

        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        transform.position = smoothedPosition;


    }

    void FindPlayer()
    {
        if(nextTimeToSearch <= Time.time)
        {
            GameObject searchResult = GameObject.FindGameObjectWithTag("Player");
            if (searchResult != null)
                target = searchResult.transform;
            nextTimeToSearch = Time.time + 0.5f;
        }
    }

}
