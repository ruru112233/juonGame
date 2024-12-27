using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
    private Vector3 GORL_POS = new Vector3(0, 10, 0);

    // ���x�����Ƃ̍U���p�^�[��
    private const int SIDE_FIRE_LV = 2;
    private const int BOM_LV = 3;
    private const int PIC_RIGHT_LV = 4;
    private const int PIC_LEFT_LV = 5;

    /// <summary>
    /// �ϐ�
    /// </summary>

    private GameObject playerGeneretorObj;
    private PlayerGeneretor playerGeneretor;

    public GameObject picRight, picLeft;

    // �W���C�X�e�B�b�N
    [SerializeField] protected FloatingJoystick joystick;

    private float speed = 2.0f;
    private float attackPt = 1.0f;

    public float Speed
    {
        get { return speed; }
        set { speed = value; }
    }

    public float AttackPt
    {
        get { return attackPt; }
        set { attackPt = value; }
    }
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

    public bool IsStop
    {
        get { return stopFlag; }
    }

    private float duration = 15.0f; // �N���A��̈ړ�����
    private float elapsedTime = 0f;

    // �{���֌W
    [SerializeField] private GameObject bom;
    private float bomShotTime = 5.0f;
    private float bomTimer = 0;


    [SerializeField] private GameObject breakdownPanel;

    private Coroutine blinkingPanelCoroutine;

    [SerializeField] private GameObject powerUpTextObj;
    private Coroutine powerUpTextCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        picRight.SetActive(false);
        picLeft.SetActive(false);

        shotTime = SHOT_TIME;

        colorChengeFlag = false;
        stopFlag = false;
        sprite = GetComponent<SpriteRenderer>();

        breakdownPanel.SetActive(false);

        powerUpTextObj.SetActive(false);

    }

    private IEnumerator EndingStart()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(0, 0, 0), 1.0f * Time.deltaTime);

        yield return new WaitForSeconds(1.3f);

        if (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;

            float t = elapsedTime / duration;
            float easpdT = Mathf.Pow(t, 2);

            transform.position = Vector3.Lerp(transform.position, GORL_POS, easpdT);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.isEnding)
        {
            StartCoroutine(EndingStart());
        }

        // �̏ᒆ�̃p�l����\�������� 
        if (ParalysisTimer())
        {
            if (blinkingPanelCoroutine == null)
            {
                blinkingPanelCoroutine = StartCoroutine(BlinkingPanel());
            }
            return;
        }
        else
        {
            if (blinkingPanelCoroutine != null)
            {
                StopCoroutine(blinkingPanelCoroutine);
                blinkingPanelCoroutine = null;
            }
            breakdownPanel.SetActive(false);
        }

        PlayerAction();

        reviveCounter += Time.deltaTime;
        if (reviveCounter >= reviveTime)
        {
            // ��莞�Ԍo������A���G���Ԃ��C������
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
                    sprite.color = new Color(1, 1, 1, 1);
                }
                else
                {
                    sprite.color = new Color(1, 1, 1, 0);
                }
            }
        }

        BomShot();

        if (GameManager.instance.PlayerLv >= PIC_RIGHT_LV)
        {
            picRight.SetActive(true);
        }

        if (GameManager.instance.PlayerLv >= PIC_LEFT_LV)
        {
            picLeft.SetActive(true);
        }
    }

    // �p���[�A�b�v�e�L�X�g�̕\��
    public void ShowText(string text, Color color)
    {
        if (powerUpTextCoroutine != null)
        {
            StopCoroutine(powerUpTextCoroutine);
            powerUpTextCoroutine = null;
        }

        TextMeshProUGUI powerUpText = powerUpTextObj.GetComponent<TextMeshProUGUI>();
        powerUpText.text = text;
        powerUpText.color = color;

        if (powerUpTextCoroutine == null)
        {
            powerUpTextCoroutine = StartCoroutine(ItemMove());
        }
    }

    private IEnumerator ItemMove()
    {
        powerUpTextObj.transform.position = transform.position + new Vector3(0.35f, 0.2f, 0);
        powerUpTextObj.SetActive(true);

        for (int i = 0; i < 15; i++)
        {
            powerUpTextObj.transform.position += new Vector3(0, 10.0f * Time.deltaTime, 0);
            yield return new WaitForSeconds(0.05f);
        }

        yield return new WaitForSeconds(0.05f);
        powerUpTextObj.SetActive(false);
    }

    private IEnumerator BlinkingPanel()
    {
        breakdownPanel.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        breakdownPanel.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        breakdownPanel.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        breakdownPanel.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        breakdownPanel.SetActive(true);

    }

    private void BomShot()
    {
        if (GameManager.instance.PlayerLv < BOM_LV) return;

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
            Fire(bulletSpawnPoint);
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
    void Fire(Transform SpawnPoint)
    {
        GameObject bullet = BulletPool.Instance.GetPooledObject(EnumData.InstanceObjType.PICK_BULLET);

        if (bullet != null)
        {
            bullet.transform.position = SpawnPoint.position;
            bullet.transform.rotation = Quaternion.identity;
            bullet.SetActive(true);

            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2(0, pickSpeed);
        }

    }

    private void RightFire()
    {
        if (GameManager.instance.PlayerLv < SIDE_FIRE_LV) return;

        GameObject rightBullet = BulletPool.Instance.GetPooledObject(EnumData.InstanceObjType.STICK_BULLET); ;

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

        GameObject LeftBullet = BulletPool.Instance.GetPooledObject(EnumData.InstanceObjType.STICK_BULLET);

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
        if (collision.gameObject.CompareTag("Thunder"))
        {
            if (blinkingPanelCoroutine != null)
            {
                StopCoroutine(blinkingPanelCoroutine);
                blinkingPanelCoroutine = null;
            }
            stopFlag = true;
        }
    }
}
