using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    /// <summary>
    /// íËêî
    /// </summary>

    const float UP_MOVE_LIMIT = 4.8f;
    const float DOWN_MOVE_LIMIT = -4.8f;
    const float RIGHT_MOVE_LIMIT = 2.6f;
    const float LEFT_MOVE_LIMIT = -2.6f;
    const float SHOT_TIME = 0.1f;

    /// <summary>
    /// ïœêî
    /// </summary>
    
    private GameObject playerGeneretorObj;
    private PlayerGeneretor playerGeneretor;

    private float speed = 2.0f;
    
    private bool reviveFlag;
    float reviveTime = 2.0f;
    float reviveCounter = 0;

    private SpriteRenderer sprite;
    private float colorChengeTime = 0.05f;
    private float colorChengeCounter = 0;
    private bool colorChengeFlag = false;
    
    [SerializeField] private Transform bulletSpawnPoint;
    private float bulletSpeed = 15f;

    // î≠éÀéûä‘
    private float shotTime = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        playerGeneretorObj = GameObject.FindGameObjectWithTag("PlayerGeneretor");
        playerGeneretor = playerGeneretorObj.GetComponent<PlayerGeneretor>();
     
        shotTime = SHOT_TIME;
        
        reviveFlag = true;
        colorChengeFlag = false;
        sprite = GetComponent<SpriteRenderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerAction();

        reviveCounter += Time.deltaTime;
        if (reviveCounter >= reviveTime)
        {
            // àÍíËéûä‘åoÇ¡ÇΩÇÁÅAñ≥ìGéûä‘ÇèCóπÇ∑ÇÈ
            reviveFlag = false;
            sprite.color = new Color(255, 255, 255, 255);
        }
        else
        {
            // ñ≥ìGéûä‘íÜÇÕì_ñ≈Ç∑ÇÈ
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

    // î≠éÀéûä‘
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

    // íeÇÃî≠éÀ
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

    // ÉvÉåÉCÉÑÅ[ÇÃà⁄ìÆ
    void PlayerMove()
    {
        // è„
        if (this.transform.position.y < UP_MOVE_LIMIT)
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                this.transform.position += new Vector3(0, speed * Time.deltaTime, 0);
            }
        }

        // â∫
        if (this.transform.position.y > DOWN_MOVE_LIMIT)
        {
            if (Input.GetKey(KeyCode.DownArrow))
            {
                this.transform.position -= new Vector3(0, speed * Time.deltaTime, 0);
            }
        }

        // âE
        if (this.transform.position.x < RIGHT_MOVE_LIMIT)
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                this.transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
            }
        }

        // ç∂
        if (this.transform.position.x > LEFT_MOVE_LIMIT)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                this.transform.position -= new Vector3(speed * Time.deltaTime, 0, 0);
            }
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
    }
}
