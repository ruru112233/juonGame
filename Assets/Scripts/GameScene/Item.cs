using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    //private int itemPoint = 10; // アイテムの加算ポイント
    //private GameObject uiManagerObj; // UiManagerのオブジェクト
    //private UiManager uiManager; // UiManagerのコンポーネント

    private Rigidbody2D rb; // Rigidbody取得用変数

    [SerializeField] private GameObject scoreBall;
    // Start is called before the first frame update
    void Start()
    {
        // UiManagerのオブジェクトを取得する
        //uiManagerObj = GameObject.FindGameObjectWithTag("UiManager");
        // UiManagerのコンポーネントを取得する。
        //uiManager = uiManagerObj.GetComponent<UiManager>();

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
            Instantiate(scoreBall, this.transform.position, Quaternion.identity);
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

}
