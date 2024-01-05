using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleShoot : MonoBehaviour
{
    private int numberObBullets = 12; // ���˂���e�̐�
    private float bulletSpeed = 2.0f; // �e�̑��x
    private float shotCooldown = 2.0f; // ���˂̃N�[���_�E������
    private float lastShotTime = 0;
    
    private void OnEnable()
    {
        Update();
    }
    void Update()
    {
        if (Time.time > lastShotTime + shotCooldown)
        {
            CircleShootBullets();
            lastShotTime = Time.time;
        }
    }

    void CircleShootBullets()
    {
        for (int i = 0; i < numberObBullets; i++)
        {
            float angle = (360f / numberObBullets) * i;
            Vector3 direction = Quaternion.Euler(0, 0, angle) * transform.up;
            GameObject bullet = BulletPool.Instance.GetEnemyPooledObject();
            
            bullet.transform.position = transform.position;
            bullet.transform.rotation = Quaternion.Euler(0, 0, angle);

            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            bullet.SetActive(true);
            if (rb != null)
            {
                rb.velocity = direction * bulletSpeed;
            }
        }
    }

}
