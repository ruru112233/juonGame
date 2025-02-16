using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastEndingPlayer : MonoBehaviour
{
    [SerializeField] private Transform earthPoint;
    [SerializeField] private GameObject hoshi;

    private float angle;
    private float angleSpeed = 300;

    private float speed = 3.7f;

    private float scaleSpeed = 2.0f;

    // Update is called once per frame
    void Update()
    {
        angle += Time.deltaTime * angleSpeed;
        transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, angle);
        
        transform.position = Vector3.MoveTowards(transform.position, earthPoint.position, speed * Time.deltaTime);

        float s = scaleSpeed * Time.deltaTime;
        transform.localScale -= new Vector3(s, s, 0);

        if (transform.localScale.x < float.Epsilon)
        {
            Instantiate(hoshi, new Vector3(transform.position.x,transform.position.y, transform.position.z), Quaternion.identity);
            Destroy(gameObject);
        }

    }
}
