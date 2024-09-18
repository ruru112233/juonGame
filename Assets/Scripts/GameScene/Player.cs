using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    /// <summary>
    /// �萔
    /// </summary>

    const float UP_MOVE_LIMIT = 4.8f;
    const float DOWN_MOVE_LIMIT = -4.8f;
    const float RIGHT_MOVE_LIMIT = 2.6f;
    const float LEFT_MOVE_LIMIT = -2.6f;
    const float SHOT_TIME = 0.3f;
    const float PARALYSIS_TIME = 2.0f;
    const float SIDE_SHOT_ANGLE = 1.0f;

    // ���x�����Ƃ̍U���p�^�[��
    private const int SIDE_FIRE_LV = 2;
    private const int BOM_LV = 3;

    /// <summary>
    /// �ϐ�
    /// </summary>

    private GameObject playerGeneretorObj;
    private PlayerGeneretor playerGeneretor;

    // �W���C�X�e�B�b�N
    [SerializeField] protected FloatingJoystick joystick;

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


    // ���ˎ���
    private float shotTime = 0.0f;

    // Ⴢꎞ��
    private float paralysisTime = 0.0f;
    private bool stopFlag = false;

    // �{���֌W
    [SerializeField] private GameObject bom;
    private float bomShotTime = 5.0f;
    private float bomTimer = 0;

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
            // ��莞�Ԍo������A���G���Ԃ��C������
            reviveFlag = false;
            sprite.color = new Color(255, 255, 255, 255);
        }
        else
        {
            // ���G���Ԓ��͓_�ł���
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

        // �{���̔���
        bomTimer += Time.deltaTime;

        if (bomTimer >= bomShotTime)
        {
            bomTimer = 0;
            Instantiate(bom, transform.position, Quaternion.identity);
        }
    }


    void PlayerAction()
    {
        if (ParalysisTimer()) return;

        PlayerMove();

        if (ShotTimer())
        {
            Fire();
            RightFire();
            LeftFire();
        }
    }

    // ���ˎ���
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

    // Ⴢꎞ��
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

    // �e�̔���
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
        if (GameManager.instance.PlayerLv < SIDE_FIRE_LV) return;

        GameObject rightBullet = BulletPool.Instance.GetSticksObject();

        if (rightBullet != null)
        {
            rightBullet.transform.position = bulletRightPoint.position;
            rightBullet.transform.rotation = Quaternion.identity;
            rightBullet.SetActive(true);

            Rigidbody2D rb = rightBullet.GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2(SIDE_SHOT_ANGLE, stickSpeed);
        }

    }
    private void LeftFire()
    {
        if (GameManager.instance.PlayerLv < SIDE_FIRE_LV) return;

        GameObject LeftBullet = BulletPool.Instance.GetSticksObject();

        if (LeftBullet != null)
        {
            LeftBullet.transform.position = bulletLeftPoint.position;
            LeftBullet.transform.rotation = Quaternion.identity;
            LeftBullet.SetActive(true);

            Rigidbody2D rb = LeftBullet.GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2(-SIDE_SHOT_ANGLE, stickSpeed);
        }
    }

    // �v���C���[�̈ړ�
    protected virtual void PlayerMove()
    {
        float joyconX = joystick.Horizontal;
        float joyconY = joystick.Vertical;
        Vector3 newPosition = this.transform.position;

        HorizontalPosSetting(joyconX, ref newPosition);

        VerticalPosSetting(joyconY, ref newPosition);

        // �V�����ʒu��ݒ�
        this.transform.position = newPosition;

    }

    protected virtual void HorizontalPosSetting(float joyconX, ref Vector3 newPosition)
    {
        // X���̈ړ��͈͂��`�F�b�N
        if (joyconX > 0 && newPosition.x < RIGHT_MOVE_LIMIT) // �E�Ɉړ�
        {
            newPosition.x += joyconX * speed * Time.deltaTime;
        }
        else if (joyconX < 0 && newPosition.x > LEFT_MOVE_LIMIT) // ���Ɉړ�
        {
            newPosition.x += joyconX * speed * Time.deltaTime;
        }

    }

    protected virtual void VerticalPosSetting(float joyconY, ref Vector3 newPosition)
    {
        // Y���̈ړ��͈͂��`�F�b�N
        if (joyconY > 0 && newPosition.y < UP_MOVE_LIMIT) // ��Ɉړ�
        {
            newPosition.y += joyconY * speed * Time.deltaTime;
        }
        else if (joyconY < 0 && newPosition.y > DOWN_MOVE_LIMIT) // ���Ɉړ�
        {
            newPosition.y += joyconY * speed * Time.deltaTime;
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
