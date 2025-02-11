using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleMove : MonoBehaviour
{
    [SerializeField] Transform center;
    private float speed = 250f;

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(center.position, Vector3.forward, -speed * Time.deltaTime);

        transform.up = Vector3.up;
    }
}
