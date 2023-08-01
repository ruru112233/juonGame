using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int EnemyHp = 5;

    private float speed = 2f;
    private float a = 1f;
    private float b = -5f;
    private float c = 10f;

    private float initialX;
    private float initialY;
    private float t;

    // Start is called before the first frame update
    void Start()
    {
        initialX = transform.position.x;
        initialY = transform.position.y;
        t = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (EnemyHp <= 0)
        {
            Destroy(this.gameObject);
        }

        t += Time.deltaTime;

        float x = initialX + speed * t;
        float y = initialY + (a * t * t + b * t + c);

        transform.position = new Vector2(x, y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            EnemyHp--;
        }
    }
}
