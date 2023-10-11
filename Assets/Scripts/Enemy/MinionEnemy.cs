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
        // EnemyGenInfoÇÃèâä˙âª
        EnemyGenInfoInit();

    }
    void EnemyGenInfoInit()
    {
        enemyGenInfo_.enemyDirectionType = MoveDirectionType.NO_MOVE;
        enemyGenInfo_.firstSpeed = 0;
        enemyGenInfo_.secondSpeed = 0;
        enemyGenInfo_.shotPattern = 0;
    }


    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        CreateEnemy(enemyGenInfo_);
    }

    public void CreateEnemy(EnemyGenInfo enemyInfo)
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
                Debug.Log("íËã`ílà»äO");
                break;
        }
    }



    public void SetEnemyGenInfo(EnemyGenInfo enemyInfo)
    {
        enemyGenInfo_ = enemyInfo;
    }

    // -----------------------------
}
