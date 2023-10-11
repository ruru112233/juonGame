using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTargetShot : MonoBehaviour
{
    private float bulletSpeed = 1f; // 弾の速度
    private float shotCooldown = 1.5f; // 発射のクールダウン時間
    private float lastShotTime = 0;

    private Transform playerTransform; // プレイヤーの位置
    // ---------------------------
    private void OnEnable()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        if (Time.time > lastShotTime + shotCooldown)
        {
            PlayerShootBullet();
            lastShotTime = Time.time;
        }
    }
    // ---------------------------

    // Update is called once per frame
    void Update()
    {

    }

    private void PlayerShootBullet()
    {
        // 弾の方向をプレイヤーの方向に設定
        Vector2 directionToPlayer = (playerTransform.position - transform.position).normalized;

        // 弾を取得
        GameObject bullet = BulletPool.Instance.GetEnemyPooledObject();

        // 弾の位置を発射位置に設定
        bullet.transform.position = transform.position;
        bullet.SetActive(true);

        // 弾に速度を与える
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = directionToPlayer * bulletSpeed;
        }
    }
}
