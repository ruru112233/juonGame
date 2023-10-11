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
        GameObject enemyA = SpawnEnemy(minionEnemyObj, new Vector3(2, 6, 0));
        GameObject enemyB = SpawnEnemy(minionEnemyObj, new Vector3(-2, 6, 0));
        yield return new WaitForSeconds(1);

        SetEnemyInfo(enemyA, CreateEnemyGenInfo(MoveDirectionType.BOTTOM, 0));
        SetEnemyInfo(enemyB, CreateEnemyGenInfo(MoveDirectionType.BOTTOM, 0));
        
        yield return new WaitForSeconds(7);

        SetEnemyInfo(enemyA, CreateEnemyGenInfo(MoveDirectionType.TOP_RIGHT, 0.2f, 0));
        SetEnemyInfo(enemyB, CreateEnemyGenInfo(MoveDirectionType.TOP_LEFT, 0.2f, 0));

        GameObject enemyC = SpawnEnemy(minionEnemyObj, new Vector3(1, 6, 0));
        GameObject enemyD = SpawnEnemy(minionEnemyObj, new Vector3(-1, 6, 0));

        yield return new WaitForSeconds(1);

        SetEnemyInfo(enemyC, CreateEnemyGenInfo(MoveDirectionType.BOTTOM, 0));
        SetEnemyInfo(enemyD, CreateEnemyGenInfo(MoveDirectionType.BOTTOM, 0));

        yield return new WaitForSeconds(6);

        SetEnemyInfo(enemyC, CreateEnemyGenInfo(MoveDirectionType.TOP, 0));
        SetEnemyInfo(enemyD, CreateEnemyGenInfo(MoveDirectionType.TOP, 0));

        yield return new WaitForSeconds(5);

        SetEnemyInfo(enemyC, CreateEnemyGenInfo(MoveDirectionType.BOTTOM_LEFT, 0));
        SetEnemyInfo(enemyD, CreateEnemyGenInfo(MoveDirectionType.BOTTOM_RIGHT, 0));

        yield return null;


    }

    GameObject SpawnEnemy(GameObject enemy, Vector3 position)
    {
        return Instantiate(enemy, position, Quaternion.identity);
    }

    void SetEnemyInfo(GameObject enemy, EnemyGenInfo info)
    {
        enemy.GetComponent<MinionEnemy>().SetEnemyGenInfo(info);
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
