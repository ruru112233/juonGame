using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int numberObBullets = 12; // ���˂���e�̐�
    private float bulletSpeed = 4f; // �e�̑��x
    private float shotCooldown = 1.5f; // ���˂̃N�[���_�E������

    private float lastShotTime;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > lastShotTime + shotCooldown)
        {
            Debug.Log("����");
            ShootBullets();
            lastShotTime = Time.time;
        }

    }

    void ShootBullets()
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
