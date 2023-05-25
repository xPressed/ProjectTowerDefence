using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeButtons : MonoBehaviour
{
    public Transform cam;
    public float turnSpeed = 5f;

    void Update()
    {
        Vector3 dir = transform.position - cam.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        transform.rotation = Quaternion.Euler(rotation);
    }
}
