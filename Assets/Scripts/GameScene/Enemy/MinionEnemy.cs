using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinionEnemy : EnemyShotPattern
{
    private EnemyGenInfo enemyGenInfo_;

    private float enemyMaxHp_ = 0;
    private float enemyHp_ = 0;

    private Animator anime;

    public EnemyGenInfo GetEnemyGenInfo
    {
        get { return enemyGenInfo_; }
    }

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        // EnemyGenInfoの初期化
        EnemyGenInfoInit();

        enemyMaxHp_ = 10.0f;
        enemyHp_ = enemyMaxHp_;

        anime = this.GetComponent<Animator>();

    }

    void EnemyGenInfoInit()
    {
        enemyGenInfo_.enemyDirectionType = EnumData.MoveDirectionType.NO_MOVE;
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

            info.enemyDirectionType = EnumData.MoveDirectionType.NO_MOVE;
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
        if (collision.gameObject.CompareTag("Bullet") && GameManager.instance.player)
        {
            enemyHp_ -= GameManager.instance.player.AttackPt;

            // ダメージアニメーション
            if (anime)
            {
                anime.SetTrigger("damage");
            }

            if (enemyHp_ <= 0)
            {
                AudioManager.instance.PlaySE((int)EnumData.SeType.DROP);

                // MaxHpの更新
                enemyHp_ = enemyMaxHp_;
                Common.ScatterItem(this.transform);
            }
        }
    }
}
