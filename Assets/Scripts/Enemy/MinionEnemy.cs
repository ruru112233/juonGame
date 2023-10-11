using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionEnemy : EnemyShotPattern
{
    // -----------------------------
    private EnemyGenInfo enemyGenInfo_;

    // Start is called before the first frame update
    void Start()
    {
        enemyGenInfo_ = new EnemyGenInfo();
        enemyGenInfo_.enemyDirectionType = MoveDirectionType.BOTTOM;
        enemyGenInfo_.xSpeed = 0;
        enemyGenInfo_.ySpeed = 0;

    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        CreateEnemy(enemyGenInfo_);
        Debug.Log(enemyGenInfo_.enemyDirectionType);
    }

    public void CreateEnemy(EnemyGenInfo enemyInfo)
    {
        switch (CheckMethod(enemyInfo))
        {
            case 0:
                MoveDirection(enemyInfo.enemyDirectionType);
                break;
            case 1:
                MoveDirection(enemyInfo.enemyDirectionType, enemyInfo.xSpeed);
                break;
            case 2:
                MoveDirection(enemyInfo.enemyDirectionType, enemyInfo.xSpeed, enemyInfo.ySpeed);
                break;
            default:
                Debug.Log("’è‹`’lˆÈŠO");
                break;
        }
    }

    public void SetEnemyGenInfo(EnemyGenInfo enemyInfo)
    {
        enemyGenInfo_ = enemyInfo;
    }

    // -----------------------------
}
