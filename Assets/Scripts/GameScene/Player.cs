using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    /// <summary>
    /// 定数
    /// </summary>

    const float UP_MOVE_LIMIT = 4.8f;
    const float DOWN_MOVE_LIMIT = -4.8f;
    const float RIGHT_MOVE_LIMIT = 2.6f;
    const float LEFT_MOVE_LIMIT = -2.6f;
    const float SHOT_TIME = 0.3f;
    const float PARALYSIS_TIME = 2.0f;

    /// <summary>
    /// 変数
    /// </summary>

    private GameObject playerGeneretorObj;
    private PlayerGeneretor playerGeneretor;

    // ジョイスティック
    [SerializeField] FloatingJoystick joystick;

    private float speed = 2.0f;
    
    private bool reviveFlag;
    float reviveTime = 2.0f;
    float reviveCounter = 0;

    private SpriteRenderer sprite;
    private float colorChengeTime = 0.05f;
    private float colorChengeCounter = 0;
    private bool colorChengeFlag = false;
    
    [SerializeField] private Transform bulletSpawnPoint, bulletLeftPoint, bulletRightPoint;
    private float pickSpeed = 10f;
    private float stickSpeed = 5f;

    // 攻撃パターンフラグ
    private bool sideFireFlag = false;

    // 発射時間
    private float shotTime = 0.0f;

    // 痺れ時間
    private float paralysisTime = 0.0f;
    private bool stopFlag = false;


    // Start is called before the first frame update
    void Start()
    {
        playerGeneretorObj = GameObject.FindGameObjectWithTag("PlayerGeneretor");
        playerGeneretor = playerGeneretorObj.GetComponent<PlayerGeneretor>();
     
        shotTime = SHOT_TIME;
        sideFireFlag = true;

        reviveFlag = true;
        colorChengeFlag = false;
        stopFlag = false;
        sprite = GetComponent<SpriteRenderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerAction();

        reviveCounter += Time.deltaTime;
        if (reviveCounter >= reviveTime)
        {
            // 一定時間経ったら、無敵時間を修了する
            reviveFlag = false;
            sprite.color = new Color(255, 255, 255, 255);
        }
        else
        {
            // 無敵時間中は点滅する
            colorChengeCounter += Time.deltaTime;
            if (colorChengeCounter >= colorChengeTime)
            {
                colorChengeCounter = 0;
                colorChengeFlag = !colorChengeFlag;
                if (colorChengeFlag)
                {
                    sprite.color = new Color(255, 255, 255, 255);
                }
                else
                {
                    sprite.color = new Color(255, 255, 255, 0);
                }
            }
        }
    }


    void PlayerAction()
    {
        PlayerMove();

        if (ShotTimer())
        {
            Fire();
            RightFire();
            LeftFire();
        }
    }

    // 発射時間
    bool ShotTimer()
    {
        bool shotFlag = false;

        shotTime -= Time.deltaTime;

        if (shotTime <= 0)
        {
            shotTime = SHOT_TIME;
            shotFlag = true;
        }

        return shotFlag;
    }

    // 痺れ時間
    bool ParalysisTimer()
    {
        if (!stopFlag) return false;

        paralysisTime += Time.deltaTime;

        if (paralysisTime >= PARALYSIS_TIME)
        {
            paralysisTime = 0;
            stopFlag = false;
        }

        return true;
    }

    // 弾の発射
    void Fire()
    {
        GameObject bullet = BulletPool.Instance.GetPooledObject();

        if (bullet != null)
        {
            bullet.transform.position = bulletSpawnPoint.position;
            bullet.transform.rotation = Quaternion.identity;
            bullet.SetActive(true);

            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2(0, pickSpeed);
        }

    }

    private void RightFire()
    {
        if (!sideFireFlag) return;

        GameObject rightBullet = BulletPool.Instance.GetSticksObject();

        if (rightBullet != null)
        {
            Debug.Log("右側");
            rightBullet.transform.position = bulletRightPoint.position;
            rightBullet.transform.rotation = Quaternion.identity;
            rightBullet.SetActive(true);

            Rigidbody2D rb = rightBullet.GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2(2, stickSpeed);
        }

    }
    private void LeftFire()
    {
        if (!sideFireFlag) return;

        GameObject LeftBullet = BulletPool.Instance.GetSticksObject();

        if (LeftBullet != null)
        {
            Debug.Log("左側");
            LeftBullet.transform.position = bulletLeftPoint.position;
            LeftBullet.transform.rotation = Quaternion.identity;
            LeftBullet.SetActive(true);

            Rigidbody2D rb = LeftBullet.GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2(-2, stickSpeed);
        }
    }

    // プレイヤーの移動
    void PlayerMove()
    {
        if (ParalysisTimer()) return;

        float joyconX = joystick.Horizontal;
        float joyconY = joystick.Vertical;
        Vector3 newPosition = this.transform.position;

        // X軸の移動範囲をチェック
        if (joyconX > 0 && newPosition.x < RIGHT_MOVE_LIMIT) // 右に移動
        {
            newPosition.x += joyconX * speed * Time.deltaTime;
        }
        else if (joyconX < 0 && newPosition.x > LEFT_MOVE_LIMIT) // 左に移動
        {
            newPosition.x += joyconX * speed * Time.deltaTime;
        }

        // Y軸の移動範囲をチェック
        if (joyconY > 0 && newPosition.y < UP_MOVE_LIMIT) // 上に移動
        {
            newPosition.y += joyconY * speed * Time.deltaTime;
        }
        else if (joyconY < 0 && newPosition.y > DOWN_MOVE_LIMIT) // 下に移動
        {
            newPosition.y += joyconY * speed * Time.deltaTime;
        }

        // 新しい位置を設定
        this.transform.position = newPosition;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            if (reviveFlag) return;

            playerGeneretor.ReducePlayerLife(1);
         
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Thunder"))
        {
            stopFlag = true;
        }
    }
}
