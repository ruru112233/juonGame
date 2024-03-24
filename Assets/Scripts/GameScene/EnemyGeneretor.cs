using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGeneretor : EnemyMove
{
    [SerializeField] private GameObject bossObj, minionEnemyObj;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(EnemyPattern());
    }

    IEnumerator EnemyPattern()
    {
        GameObject enemyA = SpawnEnemy(minionEnemyObj, new Vector3(2, 6, 0));
        GameObject enemyB = SpawnEnemy(minionEnemyObj, new Vector3(-2, 6, 0));
        yield return new WaitForSeconds(1);

        SetEnemyInfo(enemyA, SetEnemyGenInfo(MoveDirectionType.BOTTOM, 0));
        SetEnemyInfo(enemyB, SetEnemyGenInfo(MoveDirectionType.BOTTOM, 0));

        yield return new WaitForSeconds(7);

        SetEnemyInfo(enemyA, SetEnemyGenInfo(MoveDirectionType.TOP_RIGHT, 0.2f, 0));
        SetEnemyInfo(enemyB, SetEnemyGenInfo(MoveDirectionType.TOP_LEFT, 0.2f, 0));

        //GameObject enemyC = SpawnEnemy(minionEnemyObj, new Vector3(1, 6, 0));
        //GameObject enemyD = SpawnEnemy(minionEnemyObj, new Vector3(-1, 6, 0));

        //yield return new WaitForSeconds(1);

        //SetEnemyInfo(enemyC, SetEnemyGenInfo(MoveDirectionType.BOTTOM, 0));
        //SetEnemyInfo(enemyD, SetEnemyGenInfo(MoveDirectionType.BOTTOM, 0));

        //yield return new WaitForSeconds(6);

        //SetEnemyInfo(enemyC, SetEnemyGenInfo(MoveDirectionType.TOP, 0));
        //SetEnemyInfo(enemyD, SetEnemyGenInfo(MoveDirectionType.TOP, 0));

        //yield return new WaitForSeconds(5);

        //SetEnemyInfo(enemyC, SetEnemyGenInfo(MoveDirectionType.BOTTOM_LEFT, 0));
        //SetEnemyInfo(enemyD, SetEnemyGenInfo(MoveDirectionType.BOTTOM_RIGHT, 0));

        yield return new WaitForSeconds(3);

        GameObject boss = SpawnEnemy(bossObj, new Vector3(0, 6, 0));
        yield return new WaitForSeconds(1);

        //SetEnemyInfo(boss, SetEnemyGenInfo(MoveDirectionType.BOTTOM, 0));

        
        
        //boss.GetComponent<BossEnemy>().BossMoveFlag = true;

    }

    GameObject SpawnEnemy(GameObject enemy, Vector3 position)
    {
        GameObject obj = null;

        if (enemy != null)
        {
            obj = Instantiate(enemy, position, Quaternion.identity);
        }

        return obj;
    }

    void SetEnemyInfo(GameObject enemy, EnemyGenInfo info)
    {
        if (enemy != null)
        {
            enemy.GetComponent<MinionEnemy>().SetEnemyGenInfo(info);
        }
        
    }

    EnemyGenInfo SetEnemyGenInfo(MoveDirectionType enemyDirectionType, int shotPattern)
    {
        EnemyGenInfo info = new EnemyGenInfo();

        info.enemyDirectionType = enemyDirectionType;
        info.shotPattern = shotPattern;

        return info;
    }

    EnemyGenInfo SetEnemyGenInfo(MoveDirectionType enemyDirectionType, float speed, int shotPattern)
    {
        EnemyGenInfo info = new EnemyGenInfo();

        info.enemyDirectionType = enemyDirectionType;
        info.firstSpeed = speed;
        info.shotPattern = shotPattern;

        return info;
    }

    EnemyGenInfo SetEnemyGenInfo(MoveDirectionType enemyDirectionType, float xSpeed, float ySpeed, int shotPattern)
    {
        EnemyGenInfo info = new EnemyGenInfo();

        info.enemyDirectionType = enemyDirectionType;
        info.firstSpeed = xSpeed;
        info.secondSpeed = ySpeed;
        info.shotPattern = shotPattern;

        return info;
    }

}
