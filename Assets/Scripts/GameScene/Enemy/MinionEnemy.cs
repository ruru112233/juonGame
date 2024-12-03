using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinionEnemy : EnemyShotPattern
{
    private EnemyGenInfo enemyGenInfo_;

    private int enemyMaxHp_ = 0;
    private int enemyHp_ = 0;

    private Animator anime;

    public EnemyGenInfo GetEnemyGenInfo
    {
        get { return enemyGenInfo_; }
    }

    // Itemオブジェクトのドロップする数
    private int dropItemCount = 5;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        // EnemyGenInfoの初期化
        EnemyGenInfoInit();

        enemyMaxHp_ = 10;
        enemyHp_ = enemyMaxHp_;

        anime = this.GetComponent<Animator>();

    }

    void EnemyGenInfoInit()
    {
        enemyGenInfo_.enemyDirectionType = MoveDirectionType.NO_MOVE;
        enemyGenInfo_.firstSpeed = 0;
        enemyGenInfo_.secondSpeed = 0;
        enemyGenInfo_.shotPattern = ShotScriptList.Count;
    }


    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        DestroyEnemy();

        if (GameManager.instance.isStopped) return;

        // エネミーの移動
        MoveEnemy(enemyGenInfo_);

        // エネミーの攻撃パターン決定
        SetShotScript();
    }

    private void MoveEnemy(EnemyGenInfo enemyInfo)
    {
        switch (CheckMethod(enemyInfo))
        {
            case 0:
                MoveDirection(enemyInfo.enemyDirectionType);
                break;
            case 1:
                MoveDirection(enemyInfo.enemyDirectionType, enemyInfo.firstSpeed);
                break;
            case 2:
                MoveDirection(enemyInfo.enemyDirectionType, enemyInfo.firstSpeed, enemyInfo.secondSpeed);
                break;
            default:
                //Debug.Log("定義値以外");
                break;
        }
    }

    private void DestroyEnemy()
    {
        if (this.transform.position.x > DESTROY_RIGHT_LINE ||
            this.transform.position.x < DESTROY_REFT_LINE ||
            this.transform.position.y > DESTROY_TOP_LINE ||
            this.transform.position.y < DESTROY_BOTTOM_LINE)
        {

            EnemyGenInfo info = new EnemyGenInfo();

            info.enemyDirectionType = MoveDirectionType.NO_MOVE;
            info.shotPattern = 0;

            SetEnemyGenInfo(info);

            this.transform.position = startPos;
        }
    }

    private void SetShotScript()
    {
        if (enemyGenInfo_.shotPattern < ShotScriptList.Count)
        {
            ActiveScriptByIndex(enemyGenInfo_.shotPattern);
        }
    }

    public void SetEnemyGenInfo(EnemyGenInfo enemyInfo)
    {
        EnemyGenInfoInit();
        enemyGenInfo_ = enemyInfo;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {

            enemyHp_--;

            // ダメージアニメーション
            if (anime)
            {
                anime.SetTrigger("damage");
            }

            //hpSlider.value = enemyHp_;
            if (enemyHp_ <= 0)
            {
            
                // MaxHpの更新
                enemyHp_ = enemyMaxHp_;
                ScatterItem();
                //Destroy(gameObject);
            }
        }
    }

    // Itemを散らして配置する関数
    private void ScatterItem()
    {
        for (int i = 0; i < dropItemCount; i++)
        {
            GameObject obj = GameManager.instance.itemStock.SetItemObj();

            if (obj)
            {
                Vector3 randomItemPos = RandomPosition(transform.position);
                Instantiate(obj, randomItemPos, Quaternion.Euler(0, 0, 45));
            }
        }
    }

    // Itemオブジェクトをランダムに配置するための関数
    private Vector3 RandomPosition(Vector3 targetPos)
    {
        Vector3 pos = targetPos;

        pos.x = Random.Range(targetPos.x - 0.5f, targetPos.x + 0.5f);
        pos.y = Random.Range(targetPos.y - 0.5f, targetPos.y + 0.5f);

        return pos;
    }
}
