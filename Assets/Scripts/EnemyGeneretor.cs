using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGeneretor : EnemyMove
{
    // ----------------------------
    [SerializeField] private GameObject bossObj, minionEnemyObj;

    private MinionEnemy minionEnemy;

    // Start is called before the first frame update
    void Start()
    {
        minionEnemy = minionEnemyObj.GetComponent<MinionEnemy>();
        StartCoroutine(EnemyPattern());
    }

    IEnumerator EnemyPattern()
    {
        Vector3 RightPos = new Vector3(3,5,0);
        Vector3 LeftPos = new Vector3(-3, 5, 0);

        EnemyGenInfo info = new EnemyGenInfo();
        info.enemyDirectionType = MoveDirectionType.BOTTOM;

        GameObject enemyA = Instantiate(minionEnemyObj, RightPos, Quaternion.identity);
        enemyA.GetComponent<MinionEnemy>().SetEnemyGenInfo(info);


        yield return null;


    }
    // ----------------------------

}
