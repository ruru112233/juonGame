using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TornadoShoot : MonoBehaviour
{
    private float bulletSpeed = 1f; // �e�̑��x
    private float spiralAngle = 30f; // �Q���̊p�x

    private float shotCooldown = 0.3f; // ���˂̃N�[���_�E������
    private float lastShotTime = 0;
    private float currentAngle = 0f;

    // ---------------------------
    private void OnEnable()
    {
        if (Time.time > lastShotTime + shotCooldown)
        {
            TornadoBulletShoot();
            lastShotTime = Time.time;
        }
    }

    // ---------------------------

    void Update()
    {

    }

    private void TornadoBulletShoot()
    {
        // �e���擾
        GameObject bullet = BulletPool.Instance.GetEnemyPooledObject();
        
        // �e�̈ʒu�𔭎ˈʒu�ɐݒ�
        bullet.transform.position = transform.position;

        // �e�ɑ��x��^����
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        Vector2 direction = new Vector2(Mathf.Cos(currentAngle * Mathf.Deg2Rad), 
                                        Mathf.Sin(currentAngle * Mathf.Deg2Rad));
        bullet.SetActive(true);
        if (rb != null)
        {
            rb.velocity = direction * bulletSpeed;
        }

        // ���̒e�ɑ΂��Ċp�x��ύX
        currentAngle += spiralAngle;
    }
}
