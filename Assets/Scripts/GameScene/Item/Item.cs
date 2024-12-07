using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Item : MonoBehaviour
{
    // �F
    private Color COLOR_WHITE = new Color(1f, 1f, 1f, 1f); // ��
    private Color COLOR_YELLOW = new Color(1f, 0.9f, 0.1f, 1f); // ���F
    private Color COLOR_PINK = new Color(0.8113208f, 0.0956746f, 0.5544159f, 1f); // �s���N
    private Color COLOR_RED = new Color(1, 0, 0.1568494f, 1);
    private Color COLOR_GREEN = new Color(0.212765f, 1, 0, 1);

    // �X�P�[��
    private Vector3 OMP_SCALE = new Vector3(0.4f, 0.4f, 0.5f);
    private Vector3 THUNDER_SCALE = new Vector3(1.2f, 1.2f, 1f);

    // �X�R�A�̓��_
    private const int JIMI_SCORE = 100;
    private const int JOHN_SCORE = 50;
    private const int THUNDER_SCORE = -30;

    // �p���[�A�b�v�̃e�L�X�g
    private const string AT_UP = "��Attack";
    private const string SP_UP = "��Speed";

    private Player playerScript;

    // item���̊i�[�p�\����
    struct ItemInfo
    {
        public Sprite sprite;
        public Color color;
        public Vector3 imageScale;
    }

    private ItemInfo itemInfo;

    private Rigidbody2D rb; // Rigidbody�擾�p�ϐ�

    [SerializeField] private GameObject scoreBall;

    // �A�C�e���̎��
    public enum ItemPattern
    {
        JIMI_GUITAR,    // �W�~�w���M�^�[
        JOHN_GUITAR,    // �W�����M�^�[
        THUNDER,        // ���
        AT_POWER_UP,    // �U���̓A�b�v
        SP_POWER_UP,    // �X�s�[�h�A�b�v
        UNSETTILED,     // ���m�� 
    }

    public ItemPattern itemPattern;

    // Start is called before the first frame update
    void Start()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        
        RandomForce();
    }

    private void RandomForce()
    {
        // Rigidbody2d���擾
        rb = GetComponent<Rigidbody2D>();
        
        float randX = Random.Range(-100.0f, 100.0f);
        
        Vector2 force = new Vector2(randX, 200.0f);

        rb.AddForce(force);

    }

    // Update is called once per frame
    void Update()
    {
        // PlayerItemMove();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // �X�R�A�̉��Z
            //uiManager.SetScore(itemPoint);
            // ItemPattern�����m��ӊO�̓A�C�e�����X�R�A�̃A�C�e�����o��������
            if ((itemPattern == ItemPattern.JIMI_GUITAR) || 
                (itemPattern == ItemPattern.JOHN_GUITAR) )
            {
                GameObject scoreBallObj = (GameObject)Instantiate(scoreBall, this.transform.position, Quaternion.identity);
                // �A�C�e���̃C���[�W���X�V
                SpriteRenderer spriteRnd = scoreBallObj.GetComponent<SpriteRenderer>();
                ItemInfo itemInfo = SetScoreImage();
                spriteRnd.sprite = itemInfo.sprite;
                spriteRnd.color = itemInfo.color;
                scoreBallObj.transform.localScale = itemInfo.imageScale;

                // �X�R�A���X�V
                ScoreBall scoreBallScript = scoreBallObj.GetComponent<ScoreBall>();
                scoreBallScript.SetScorePoint(ItemCheckValue());
            }

            // �U���̓A�b�v
            if (itemPattern == ItemPattern.AT_POWER_UP && playerScript && playerScript.AttackPt < 3.0f)
            {
                playerScript.AttackPt += 0.1f;
                playerScript.ShowPowerUpText(AT_UP, COLOR_RED);
            }

            // �X�s�[�h�A�b�v
            if (itemPattern == ItemPattern.SP_POWER_UP && playerScript && playerScript.Speed < 6.0f)
            {
                playerScript.Speed += 0.2f;
                playerScript.ShowPowerUpText(SP_UP, COLOR_GREEN);
            }

            // Item�I�u�W�F�N�g���폜����
            Destroy(gameObject);
        }
    }

    private void PlayerItemMove()
    {
        float movePoint = 2.0f;
        float itemMoveSpeed = 1.5f;
        // �v���C���[�̃I�u�W�F�N�g���擾
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            // �v���C���[��Item�̋������v��
            float distance = Vector3.Distance(playerObj.transform.position, transform.position);
            // �v���C���[�ƃA�C�e���̊Ԋu������菬�����Ȃ�����
            if (distance < movePoint)
            {
                // �v���C���[�̕��Ɍ������Đi��
                Vector3 playerDistance = (playerObj.transform.position - transform.position).normalized;
                transform.position += playerDistance * itemMoveSpeed * Time.deltaTime;
            }
        }
    }

    private int ItemCheckValue()
    {
        int value = 0;

        switch (itemPattern)
        {
            case ItemPattern.JIMI_GUITAR: // �W�~�w���M�^�[
                value = JIMI_SCORE;
                break;
            case ItemPattern.JOHN_GUITAR: // �W�����M�^�[
                value = JOHN_SCORE;
                break;
            case ItemPattern.THUNDER: // ���
                value = THUNDER_SCORE;
                break;
            case ItemPattern.UNSETTILED: // ���m��
            default:
                break;
        }

        return value;
    }

    private ItemInfo SetScoreImage()
    {

        itemInfo.sprite = null;
        itemInfo.color = new Color(255, 255, 255, 255);
        itemInfo.imageScale = Vector3.one;

        switch (itemPattern)
        {
            case ItemPattern.JIMI_GUITAR:
                itemInfo.sprite = GameManager.instance.itemStock.eighth;
                itemInfo.color = COLOR_PINK;
                itemInfo.imageScale = OMP_SCALE;
                break;
            case ItemPattern.JOHN_GUITAR:
                itemInfo.sprite = GameManager.instance.itemStock.eighth;
                itemInfo.color = COLOR_YELLOW;
                itemInfo.imageScale = OMP_SCALE;
                break;
            case ItemPattern.THUNDER:
                itemInfo.sprite = GameManager.instance.itemStock.thunderImage;
                itemInfo.color = COLOR_WHITE;
                itemInfo.imageScale = THUNDER_SCALE;
                break;
            case ItemPattern.UNSETTILED:
            default:
                break;
        }

        return itemInfo;
    }

}
