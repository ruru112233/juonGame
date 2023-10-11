using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGeneretor : EnemyMove
{
    // ----------------------------
    [SerializeField] private GameObject bossObj, minionEnemyObj;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(EnemyPattern());
    }

    IEnumerator EnemyPattern()
    {
        Vector3 RightPos = new Vector3(2,5,0);
        Vector3 LeftPos = new Vector3(-2, 5, 0);

        GameObject enemyA = Instantiate(minionEnemyObj, RightPos, Quaternion.identity);

        yield return new WaitForSeconds(1);

        EnemyGenInfo enemyInfo = CreateEnemyGenInfo(MoveDirectionType.BOTTOM, 2);
        Debug.Log(enemyInfo.enemyDirectionType);

        enemyA.GetComponent<MinionEnemy>().SetEnemyGenInfo(enemyInfo);

        yield return null;


    }

    EnemyGenInfo CreateEnemyGenInfo(MoveDirectionType enemyDirectionType, int shotPattern)
    {
        EnemyGenInfo info = new EnemyGenInfo();

        info.enemyDirectionType = enemyDirectionType;
        info.shotPattern = shotPattern;

        return info;
    }

    EnemyGenInfo CreateEnemyGenInfo(MoveDirectionType enemyDirectionType, float speed, int shotPattern)
    {
        EnemyGenInfo info = new EnemyGenInfo();

        info.enemyDirectionType = enemyDirectionType;
        info.firstSpeed = speed;
        info.shotPattern = shotPattern;

        return info;
    }

    EnemyGenInfo CreateEnemyGenInfo(MoveDirectionType enemyDirectionType, float xSpeed, float ySpeed, int shotPattern)
    {
        EnemyGenInfo info = new EnemyGenInfo();

        info.enemyDirectionType = enemyDirectionType;
        info.firstSpeed = xSpeed;
        info.secondSpeed = ySpeed;
        info.shotPattern = shotPattern;

        return info;
    }

    // ----------------------------

}
