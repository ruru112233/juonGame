using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Item : MonoBehaviour
{
    // 色
    private Color COLOR_WHITE = new Color(1f, 1f, 1f, 1f); // 白
    private Color COLOR_YELLOW = new Color(1f, 0.9f, 0.1f, 1f); // 黄色
    private Color COLOR_PINK = new Color(0.8113208f, 0.0956746f, 0.5544159f, 1f); // ピンク
    private Color COLOR_RED = new Color(1, 0, 0.1568494f, 1);
    private Color COLOR_GREEN = new Color(0.212765f, 1, 0, 1);

    // スケール
    private Vector3 OMP_SCALE = new Vector3(0.4f, 0.4f, 0.5f);
    private Vector3 THUNDER_SCALE = new Vector3(1.2f, 1.2f, 1f);

    // スコアの得点
    private const int JIMI_SCORE = 100;
    private const int JOHN_SCORE = 50;
    private const int THUNDER_SCORE = -30;

    // パワーアップのテキスト
    private const string AT_UP = "↑Attack";
    private const string SP_UP = "↑Speed";

    private Player playerScript;

    // item情報の格納用構造体
    struct ItemInfo
    {
        public Sprite sprite;
        public Color color;
        public Vector3 imageScale;
    }

    private ItemInfo itemInfo;

    private Rigidbody2D rb; // Rigidbody取得用変数

    [SerializeField] private GameObject scoreBall;

    // アイテムの種類
    public enum ItemPattern
    {
        JIMI_GUITAR,    // ジミヘンギター
        JOHN_GUITAR,    // ジョンギター
        THUNDER,        // 稲妻
        AT_POWER_UP,    // 攻撃力アップ
        SP_POWER_UP,    // スピードアップ
        UNSETTILED,     // 未確定 
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
        // Rigidbody2dを取得
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
            // スコアの加算
            //uiManager.SetScore(itemPoint);
            // ItemPatternが未確定意外はアイテムをスコアのアイテムを出現させる
            if ((itemPattern == ItemPattern.JIMI_GUITAR) || 
                (itemPattern == ItemPattern.JOHN_GUITAR) )
            {
                GameObject scoreBallObj = (GameObject)Instantiate(scoreBall, this.transform.position, Quaternion.identity);
                // アイテムのイメージを更新
                SpriteRenderer spriteRnd = scoreBallObj.GetComponent<SpriteRenderer>();
                ItemInfo itemInfo = SetScoreImage();
                spriteRnd.sprite = itemInfo.sprite;
                spriteRnd.color = itemInfo.color;
                scoreBallObj.transform.localScale = itemInfo.imageScale;

                // スコアを更新
                ScoreBall scoreBallScript = scoreBallObj.GetComponent<ScoreBall>();
                scoreBallScript.SetScorePoint(ItemCheckValue());
            }

            // 攻撃力アップ
            if (itemPattern == ItemPattern.AT_POWER_UP && playerScript && playerScript.AttackPt < 3.0f)
            {
                playerScript.AttackPt += 0.1f;
                playerScript.ShowPowerUpText(AT_UP, COLOR_RED);
            }

            // スピードアップ
            if (itemPattern == ItemPattern.SP_POWER_UP && playerScript && playerScript.Speed < 6.0f)
            {
                playerScript.Speed += 0.2f;
                playerScript.ShowPowerUpText(SP_UP, COLOR_GREEN);
            }

            // Itemオブジェクトを削除する
            Destroy(gameObject);
        }
    }

    private void PlayerItemMove()
    {
        float movePoint = 2.0f;
        float itemMoveSpeed = 1.5f;
        // プレイヤーのオブジェクトを取得
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            // プレイヤーとItemの距離を計測
            float distance = Vector3.Distance(playerObj.transform.position, transform.position);
            // プレイヤーとアイテムの間隔が一定より小さくなったら
            if (distance < movePoint)
            {
                // プレイヤーの方に向かって進む
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
            case ItemPattern.JIMI_GUITAR: // ジミヘンギター
                value = JIMI_SCORE;
                break;
            case ItemPattern.JOHN_GUITAR: // ジョンギター
                value = JOHN_SCORE;
                break;
            case ItemPattern.THUNDER: // 稲妻
                value = THUNDER_SCORE;
                break;
            case ItemPattern.UNSETTILED: // 未確定
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
