using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : EnemyMove
{
    const float CENTER = 0.0f;

    [SerializeField] private GameObject bossObj, minionEnemyObj;
    [SerializeField] private MsgManager msgManager;
    private List<GameObject> enemys = new List<GameObject>();

    private void AddEnemyListTutorial()
    {
        enemys.Clear();
        enemys.Add(SpawnEnemy(minionEnemyObj, new Vector3(CENTER, 6, 0)));

        enemys.Add(SpawnEnemy(minionEnemyObj, new Vector3(-2, 6, 0)));
    }

    private void AddEnemyList1Stage()
    {
        enemys.Clear();
        enemys.Add(SpawnEnemy(minionEnemyObj, new Vector3(0, 6, 0)));
        enemys.Add(SpawnEnemy(minionEnemyObj, new Vector3(-2, 6, 0)));
    }

    public IEnumerator EnemyPatternTutorial()
    {
        AddEnemyListTutorial();

        yield return new WaitForSeconds(0.1f);

        yield return new WaitForSeconds(5.0f);

        GameManager.instance.isStopped = false;

        SetMessage(Msg.Speaker.JUON, "‚ ‚¢‚¤‚¦‚¨");
        SetMessage(Msg.Speaker.JUON, "‚©‚«‚­‚¯‚±");
        SetMessage(Msg.Speaker.JUON, "‚—‚—‚—‚—‚—‚—‚—‚—‚—‚—‚—‚—‚—‚—‚—‚—‚—‚—‚—");

        msgManager.StartMessage(msgManager.MessageList);

        SetEnemyInfo(enemys[0], SetEnemyGenInfo(MoveDirectionType.BOTTOM, 0));

        GameManager.instance.isStopped = true;

        yield return new WaitWhile(() => GameManager.instance.isStopped);


        yield return Wait_Y_PositionCheck(enemys[0], 1.8f);

        SetEnemyInfo(enemys[0], SetEnemyGenInfo(MoveDirectionType.NO_MOVE, 0));
    }

    private void SetMessage(Msg.Speaker speaker, string message)
    {
        Msg.MessageData msg;
        msg.speaker = speaker;
        msg.message = message;
        msgManager.MessageList.Add(msg);
    }

    public IEnumerator EnemyPattern1Stage()
    {
        AddEnemyList1Stage();

        yield return new WaitForSeconds(0.1f);

        SetEnemyInfo(enemys[0], SetEnemyGenInfo(MoveDirectionType.BOTTOM, 0));

        yield return StartCoroutine(Wait_Y_PositionCheck(enemys[0], 1.7f));

        SetEnemyInfo(enemys[0], SetEnemyGenInfo(MoveDirectionType.TOP_RIGHT, 0.2f, 0));

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

        //yield return new WaitForSeconds(3);

        //GameObject boss = SpawnEnemy(bossObj, new Vector3(0, 6, 0));
        //yield return new WaitForSeconds(1);

        //SetEnemyInfo(boss, SetEnemyGenInfo(MoveDirectionType.BOTTOM, 0));



        //boss.GetComponent<BossEnemy>().BossMoveFlag = true;

    }

    private IEnumerator Wait_Y_PositionCheck(GameObject obj, float targetPositionY, float tolerance = 0.1f)
    {
        while (Mathf.Abs(obj.transform.position.y - targetPositionY) > tolerance)
        {
            yield return null;
        }
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
