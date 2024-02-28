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
    
    [SerializeField] private Transform bulletSpawnPoint;
    private float bulletSpeed = 10f;

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
            rb.velocity = new Vector2(0, bulletSpeed);
        }

    }

    // プレイヤーの移動
    void PlayerMove()
    {
        if (ParalysisTimer()) return;

        
        if (this.transform.position.y < UP_MOVE_LIMIT &&
            this.transform.position.y > DOWN_MOVE_LIMIT &&
            this.transform.position.x < RIGHT_MOVE_LIMIT &&
            this.transform.position.x > LEFT_MOVE_LIMIT)
        {
            this.transform.position += new Vector3(0, joystick.Vertical * speed * Time.deltaTime, 0);
            this.transform.position += new Vector3(joystick.Horizontal * speed * Time.deltaTime, 0, 0);
        }

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
