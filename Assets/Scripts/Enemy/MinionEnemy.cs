using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionEnemy : EnemyShotPattern
{
    // -----------------------------
    private EnemyGenInfo enemyGenInfo_;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        // EnemyGenInfoの初期化
        EnemyGenInfoInit();

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
                Debug.Log("定義値以外");
                break;
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
        enemyGenInfo_ = enemyInfo;
    }

    // -----------------------------
}
