using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TornadoShoot : MonoBehaviour
{
    private float bulletSpeed = 1f; // 弾の速度
    private float spiralAngle = 30f; // 渦巻の角度

    private float shotCooldown = 0.3f; // 発射のクールダウン時間
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
        // 弾を取得
        GameObject bullet = BulletPool.Instance.GetEnemyPooledObject();
        
        // 弾の位置を発射位置に設定
        bullet.transform.position = transform.position;

        // 弾に速度を与える
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        Vector2 direction = new Vector2(Mathf.Cos(currentAngle * Mathf.Deg2Rad), 
                                        Mathf.Sin(currentAngle * Mathf.Deg2Rad));
        bullet.SetActive(true);
        if (rb != null)
        {
            rb.velocity = direction * bulletSpeed;
        }

        // 次の弾に対して角度を変更
        currentAngle += spiralAngle;
    }
}
