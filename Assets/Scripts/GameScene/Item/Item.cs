using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Item : MonoBehaviour
{
    const float DOWN_ITEM_LIMIT = -5.8f;

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

    // パワーアップ関係
    private const string AT_UP = "↑Attack";
    private const string AT_MAX = "Max Attack";
    private const float AT_MAX_PT = 3.0f;

    private const string SP_UP = "↑Speed";
    private const string SP_MAX = "Max Speed";
    private const float SP_MAX_PT = 7.0f;

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

    public EnumData.ItemPattern itemPattern;



    private void OnEnable()
    {
        RandomForce();
    }

    void RandomForce()
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
        if (GameManager.instance.IsItemMove)
        {
            if ((itemPattern == EnumData.ItemPattern.JIMI_GUITAR) ||
                (itemPattern == EnumData.ItemPattern.JOHN_GUITAR))
            {
                PlayerItemMove();
            }
        }

        if (this.transform.position.y <= DOWN_ITEM_LIMIT)
        {
            this.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // スコアの加算
            // ItemPatternが未確定意外はアイテムをスコアのアイテムを出現させる
            if ((itemPattern == EnumData.ItemPattern.JIMI_GUITAR) || 
                (itemPattern == EnumData.ItemPattern.JOHN_GUITAR) )
            {
                GuitarInstanceAndScoreUp();
            }

            // 攻撃力アップ
            if (itemPattern == EnumData.ItemPattern.AT_POWER_UP && GameManager.instance.player.AttackPt < AT_MAX_PT)
            {
                AtUp();
            }

            // スピードアップ
            if (itemPattern == EnumData.ItemPattern.SP_POWER_UP && GameManager.instance.player.Speed < SP_MAX_PT)
            {
                SpUp();
            }

            // マグネット
            if (itemPattern == EnumData.ItemPattern.MAGNET)
            {
                GameManager.instance.CurrentItemMoveSec = 0.0f;
                GameManager.instance.IsItemMove = true;
            }

            // Itemオブジェクトを削除する
            Destroy(gameObject);
        }
    }

    private void GuitarInstanceAndScoreUp()
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

    private void AtUp()
    {
        GameManager.instance.player.AttackPt += 0.1f;
        if (GameManager.instance.player.AttackPt >= AT_MAX_PT)
        {
            GameManager.instance.player.ShowText(AT_MAX, COLOR_RED);
        }
        else
        {
            GameManager.instance.player.ShowText(AT_UP, COLOR_RED);
        }
    }

    private void SpUp()
    {
        GameManager.instance.player.Speed += 0.2f;
        if (GameManager.instance.player.Speed >= SP_MAX_PT)
        {
            GameManager.instance.player.ShowText(SP_MAX, COLOR_GREEN);
        }
        else
        {
            GameManager.instance.player.ShowText(SP_UP, COLOR_GREEN);
        }
    }


    private void PlayerItemMove()
    {
        //float movePoint = 2.0f;
        float itemMoveSpeed = 5.5f;
        // プレイヤーのオブジェクトを取得
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            // プレイヤーとItemの距離を計測
            //float distance = Vector3.Distance(playerObj.transform.position, transform.position);
            // プレイヤーとアイテムの間隔が一定より小さくなったら
            //if (distance < movePoint)
            //{
                // プレイヤーの方に向かって進む
                Vector3 playerDistance = (playerObj.transform.position - transform.position).normalized;
                transform.position += playerDistance * itemMoveSpeed * Time.deltaTime;
            //}
        }
    }

    private int ItemCheckValue()
    {
        int value = 0;

        switch (itemPattern)
        {
            case EnumData.ItemPattern.JIMI_GUITAR: // ジミヘンギター
                value = JIMI_SCORE;
                break;
            case EnumData.ItemPattern.JOHN_GUITAR: // ジョンギター
                value = JOHN_SCORE;
                break;
            case EnumData.ItemPattern.THUNDER: // 稲妻
                value = THUNDER_SCORE;
                break;
            case EnumData.ItemPattern.UNSETTILED: // 未確定
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
            case EnumData.ItemPattern.JIMI_GUITAR:
                itemInfo.sprite = GameManager.instance.itemStock.eighth;
                itemInfo.color = COLOR_PINK;
                itemInfo.imageScale = OMP_SCALE;
                break;
            case EnumData.ItemPattern.JOHN_GUITAR:
                itemInfo.sprite = GameManager.instance.itemStock.eighth;
                itemInfo.color = COLOR_YELLOW;
                itemInfo.imageScale = OMP_SCALE;
                break;
            case EnumData.ItemPattern.THUNDER:
                itemInfo.sprite = GameManager.instance.itemStock.thunderImage;
                itemInfo.color = COLOR_WHITE;
                itemInfo.imageScale = THUNDER_SCALE;
                break;
            case EnumData.ItemPattern.UNSETTILED:
            default:
                break;
        }

        return itemInfo;
    }

}
