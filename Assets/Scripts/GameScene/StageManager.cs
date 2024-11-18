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
        enemys.Add(SpawnEnemy(minionEnemyObj, new Vector3(CENTER, 6, 0))); // 0

        enemys.Add(SpawnEnemy(minionEnemyObj, new Vector3(-2, 6, 0))); // 1
        enemys.Add(SpawnEnemy(minionEnemyObj, new Vector3(2, 6, 0))); // 2

        enemys.Add(SpawnEnemy(minionEnemyObj, new Vector3(-2, 6, 0))); // 3
        enemys.Add(SpawnEnemy(minionEnemyObj, new Vector3(-1, 6, 0))); // 4
        enemys.Add(SpawnEnemy(minionEnemyObj, new Vector3(CENTER, 6, 0))); // 5
        enemys.Add(SpawnEnemy(minionEnemyObj, new Vector3(1, 6, 0))); // 6
        enemys.Add(SpawnEnemy(minionEnemyObj, new Vector3(2, 6, 0))); // 7
        
        enemys.Add(SpawnEnemy(minionEnemyObj, new Vector3(2, 6, 0))); // 8
        enemys.Add(SpawnEnemy(minionEnemyObj, new Vector3(1, 6, 0))); // 9
        enemys.Add(SpawnEnemy(minionEnemyObj, new Vector3(CENTER, 6, 0))); // 10
        enemys.Add(SpawnEnemy(minionEnemyObj, new Vector3(-1, 6, 0))); // 11
        enemys.Add(SpawnEnemy(minionEnemyObj, new Vector3(-2, 6, 0))); // 12

        enemys.Add(SpawnEnemy(minionEnemyObj, new Vector3(-3.5f, 1, 0))); // 13
        enemys.Add(SpawnEnemy(minionEnemyObj, new Vector3(-3.5f, 0, 0))); // 14
        enemys.Add(SpawnEnemy(minionEnemyObj, new Vector3(3.5f, 1, 0))); // 15
        enemys.Add(SpawnEnemy(minionEnemyObj, new Vector3(3.5f, 0, 0))); // 16
    }

    public IEnumerator EnemyPatternTutorial()
    {
        AddEnemyListTutorial();

        yield return new WaitForSeconds(0.1f);

        // Å‰‚ÌƒRƒƒ“ƒg
        GameManager.instance.isStopped = true;

        msgManager.StartMessage(msgManager.MessageList);

        yield return new WaitWhile(() => GameManager.instance.isStopped);

        // 1‘ÎoŒ‚
        SetMessage(Msg.Speaker.JUON, "‚ ‚¢‚¤‚¦‚¨");
        SetMessage(Msg.Speaker.JUON, "‚©‚«‚­‚¯‚±");
        SetMessage(Msg.Speaker.JUON, "‚—‚—‚—‚—‚—‚—‚—‚—‚—‚—‚—‚—‚—‚—‚—‚—‚—‚—‚—");

        msgManager.StartMessage(msgManager.MessageList);

        SetEnemyInfo(enemys[0], SetEnemyGenInfo(MoveDirectionType.BOTTOM, 0));

        GameManager.instance.isStopped = true;

        msgManager.ActiveHandArrow(new Vector3(0,0,0));

        yield return new WaitWhile(() => GameManager.instance.isStopped);


        yield return Wait_Y_PositionCheck(enemys[0], 1.8f);

        SetEnemyInfo(enemys[0], SetEnemyGenInfo(MoveDirectionType.NO_MOVE, 0));
    }


    public IEnumerator EnemyPattern1Stage()
    {
        AddEnemyList1Stage();

        yield return new WaitForSeconds(0.1f);

        while (true)
        {
            // 1w
            SetEnemyInfo(enemys[0], SetEnemyGenInfo(MoveDirectionType.BOTTOM, 0));

            yield return StartCoroutine(Wait_Y_PositionCheck(enemys[0], 1.7f));

            SetEnemyInfo(enemys[0], SetEnemyGenInfo(MoveDirectionType.NO_MOVE, 0));

            yield return new WaitForSeconds(3.0f);

            SetEnemyInfo(enemys[0], SetEnemyGenInfo(MoveDirectionType.TOP_RIGHT, 0.4f, 0));

            // 2w
            SetEnemyInfo(enemys[1], SetEnemyGenInfo(MoveDirectionType.BOTTOM, 0));
            SetEnemyInfo(enemys[2], SetEnemyGenInfo(MoveDirectionType.BOTTOM, 0));

            yield return StartCoroutine(Wait_Y_PositionCheck(enemys[1], 0.5f));
            yield return StartCoroutine(Wait_Y_PositionCheck(enemys[2], 0.5f));

            SetEnemyInfo(enemys[1], SetEnemyGenInfo(MoveDirectionType.RIGHT, 3.0f, 0));
            SetEnemyInfo(enemys[2], SetEnemyGenInfo(MoveDirectionType.LEFT, 3.0f, 0));

            yield return new WaitForSeconds(1.0f);

            // 3w
            SetEnemyInfo(enemys[3], SetEnemyGenInfo(MoveDirectionType.BOTTOM, 0));
            StartCoroutine(ChangeEnemyPattan(enemys[3], -1, MoveDirectionType.TOP_RIGHT, 2.0f));

            yield return new WaitForSeconds(0.7f);

            SetEnemyInfo(enemys[4], SetEnemyGenInfo(MoveDirectionType.BOTTOM, 0));
            StartCoroutine(ChangeEnemyPattan(enemys[4], -1, MoveDirectionType.TOP_RIGHT, 2.0f));

            yield return new WaitForSeconds(0.7f);

            SetEnemyInfo(enemys[5], SetEnemyGenInfo(MoveDirectionType.BOTTOM, 0));
            StartCoroutine(ChangeEnemyPattan(enemys[5], -1, MoveDirectionType.TOP_RIGHT, 2.0f));

            yield return new WaitForSeconds(0.7f);

            SetEnemyInfo(enemys[6], SetEnemyGenInfo(MoveDirectionType.BOTTOM, 0));
            StartCoroutine(ChangeEnemyPattan(enemys[6], -1, MoveDirectionType.TOP_RIGHT, 2.0f));

            yield return new WaitForSeconds(0.7f);

            SetEnemyInfo(enemys[7], SetEnemyGenInfo(MoveDirectionType.BOTTOM, 0));
            StartCoroutine(ChangeEnemyPattan(enemys[7], -1, MoveDirectionType.TOP_RIGHT, 2.0f));

            yield return new WaitForSeconds(1.0f);

            // 4w
            SetEnemyInfo(enemys[8], SetEnemyGenInfo(MoveDirectionType.BOTTOM, 0));
            StartCoroutine(ChangeEnemyPattan(enemys[8], -1, MoveDirectionType.TOP_LEFT, 2.0f));

            yield return new WaitForSeconds(0.7f);

            SetEnemyInfo(enemys[9], SetEnemyGenInfo(MoveDirectionType.BOTTOM, 0));
            StartCoroutine(ChangeEnemyPattan(enemys[9], -1, MoveDirectionType.TOP_LEFT, 2.0f));

            yield return new WaitForSeconds(0.7f);

            SetEnemyInfo(enemys[10], SetEnemyGenInfo(MoveDirectionType.BOTTOM, 0));
            StartCoroutine(ChangeEnemyPattan(enemys[10], -1, MoveDirectionType.TOP_LEFT, 2.0f));

            yield return new WaitForSeconds(0.7f);

            SetEnemyInfo(enemys[11], SetEnemyGenInfo(MoveDirectionType.BOTTOM, 0));
            StartCoroutine(ChangeEnemyPattan(enemys[11], -1, MoveDirectionType.TOP_LEFT, 2.0f));

            yield return new WaitForSeconds(0.7f);

            SetEnemyInfo(enemys[12], SetEnemyGenInfo(MoveDirectionType.BOTTOM, 0));
            StartCoroutine(ChangeEnemyPattan(enemys[12], -1, MoveDirectionType.TOP_LEFT, 2.0f));
            yield return new WaitForSeconds(5.0f);
        }

    }

    public IEnumerator ChangeEnemyPattan(GameObject enemyObj, float changePosition, MoveDirectionType moveDirectionType, float speed)
    {
        yield return StartCoroutine(Wait_Y_PositionCheck(enemyObj, changePosition));

        SetEnemyInfo(enemyObj, SetEnemyGenInfo(moveDirectionType, speed, 0));
    }

    private void SetMessage(Msg.Speaker speaker, string message)
    {
        Msg.MessageData msg;
        msg.speaker = speaker;
        msg.message = message;
        msgManager.MessageList.Add(msg);
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
