using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveShoot : MonoBehaviour
{
    private float bulletSpeed = 1.5f; // �e�̃X�s�[�h
    private float shootRate = 0.4f; // �e�̔��˂���Ԋu

    private float timeSinceLastShoot = 0f; // 

    float offset = 0;

    bool offsetFlag = false;
    float changeFlagTimer = 5.0f;
    float offsetLastTime = 0;

    float shootAngle = 10.0f;

    private void OnEnable()
    {
        Update();
    }
    
    void Update()
    {
        ChengeTimer();

        if (Time.time > timeSinceLastShoot + shootRate)
        {
            WaveShootBullet();
            timeSinceLastShoot = Time.time;
        }
    }

    private void ChengeTimer()
    {
        if (Time.time > offsetLastTime + changeFlagTimer)
        {
            offsetFlag = !offsetFlag;
            offsetLastTime = Time.time;
        }

        if (offsetFlag)
        {
            offset += shootAngle * Time.deltaTime;
        }
        else
        {
            offset -= shootAngle * Time.deltaTime;
        }
    }

    private void WaveShootBullet()
    {
        float offsetAngle = offset;
        int setAngle = 35;
        int numDirections = 360 / setAngle;
        Vector2[] directions = new Vector2[numDirections];

        for (int i = 0; i < numDirections; i++)
        {
            float angle = i * setAngle + offsetAngle;
            directions[i] = Quaternion.Euler(0, 0, angle) * Vector2.right;
        }

        foreach (Vector2 direction in directions)
        {
            // �e�̎擾
            GameObject bullet = BulletPool.Instance.GetPooledObject(EnumData.InstanceObjType.ENEMY_BULLET);

            bullet.transform.position = this.transform.position;

            bullet.SetActive(true);

            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.velocity = direction * bulletSpeed;
        }
    }
}
