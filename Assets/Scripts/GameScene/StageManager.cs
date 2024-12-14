using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : EnemyMove
{
    [SerializeField] private GameObject bossObj, minionEnemyObj;
    [SerializeField] private MsgManager msgManager;
    private List<GameObject> enemys = new List<GameObject>();

    private void AddEnemyListTutorial()
    {
        enemys.Clear();
        enemys.Add(SpawnEnemy(minionEnemyObj, new Vector3(0, 6, 0)));

        enemys.Add(SpawnEnemy(minionEnemyObj, new Vector3(-2, 6, 0)));
    }

    private void AddEnemyList1Stage()
    {
        enemys.Clear();
        enemys.Add(SpawnEnemy(minionEnemyObj, new Vector3(0, 6, 0))); // 0

        enemys.Add(SpawnEnemy(minionEnemyObj, new Vector3(-2, 6, 0))); // 1
        enemys.Add(SpawnEnemy(minionEnemyObj, new Vector3(2, 6, 0))); // 2

        enemys.Add(SpawnEnemy(minionEnemyObj, new Vector3(-2, 6, 0))); // 3
        enemys.Add(SpawnEnemy(minionEnemyObj, new Vector3(-1, 6, 0))); // 4
        enemys.Add(SpawnEnemy(minionEnemyObj, new Vector3(0, 6, 0))); // 5
        enemys.Add(SpawnEnemy(minionEnemyObj, new Vector3(1, 6, 0))); // 6
        enemys.Add(SpawnEnemy(minionEnemyObj, new Vector3(2, 6, 0))); // 7
        
        enemys.Add(SpawnEnemy(minionEnemyObj, new Vector3(2, 6, 0))); // 8
        enemys.Add(SpawnEnemy(minionEnemyObj, new Vector3(1, 6, 0))); // 9
        enemys.Add(SpawnEnemy(minionEnemyObj, new Vector3(0, 6, 0))); // 10
        enemys.Add(SpawnEnemy(minionEnemyObj, new Vector3(-1, 6, 0))); // 11
        enemys.Add(SpawnEnemy(minionEnemyObj, new Vector3(-2, 6, 0))); // 12

        enemys.Add(SpawnEnemy(minionEnemyObj, new Vector3(-3.5f, -2, 0))); // 13
        enemys.Add(SpawnEnemy(minionEnemyObj, new Vector3(-3.5f, -2, 0))); // 14
        enemys.Add(SpawnEnemy(minionEnemyObj, new Vector3(-3.5f, -2, 0))); // 15
        enemys.Add(SpawnEnemy(minionEnemyObj, new Vector3(3.5f, -2, 0))); // 16
        enemys.Add(SpawnEnemy(minionEnemyObj, new Vector3(3.5f, -2, 0))); // 17
        enemys.Add(SpawnEnemy(minionEnemyObj, new Vector3(3.5f, -2, 0))); // 18
        enemys.Add(SpawnEnemy(minionEnemyObj, new Vector3(3.5f, 1, 0))); // 19
        enemys.Add(SpawnEnemy(minionEnemyObj, new Vector3(3.5f, 0, 0))); // 20

        bossObj.SetActive(false);
        enemys.Add(SpawnEnemy(bossObj, new Vector3(0, 10, 0))); // 21
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
        SetMessage(EnumData.Speaker.JUON, "‚ ‚¢‚¤‚¦‚¨");
        SetMessage(EnumData.Speaker.JUON, "‚©‚«‚­‚¯‚±");
        SetMessage(EnumData.Speaker.JUON, "‚—‚—‚—‚—‚—‚—‚—‚—‚—‚—‚—‚—‚—‚—‚—‚—‚—‚—‚—");

        msgManager.StartMessage(msgManager.MessageList);

        SetEnemyInfo(enemys[0], SetEnemyGenInfo(EnumData.MoveDirectionType.BOTTOM, 0));

        GameManager.instance.isStopped = true;

        msgManager.ActiveHandArrow(new Vector3(0,0,0));

        yield return new WaitWhile(() => GameManager.instance.isStopped);


        yield return Wait_Y_PositionCheck(enemys[0], 1.8f);

        SetEnemyInfo(enemys[0], SetEnemyGenInfo(EnumData.MoveDirectionType.NO_MOVE, 0));
    }


    public IEnumerator EnemyPattern1Stage()
    {
        AddEnemyList1Stage();
        StartCoroutine(ShowBoss());

        yield return new WaitForSeconds(0.1f);

        //while (GameManager.instance.unlockCounter == 1)
        while (true)
        {
            // 1w
            SetEnemyInfo(enemys[0], SetEnemyGenInfo(EnumData.MoveDirectionType.BOTTOM, 0));

            yield return StartCoroutine(Wait_Y_PositionCheck(enemys[0], 1.7f));

            SetEnemyInfo(enemys[0], SetEnemyGenInfo(EnumData.MoveDirectionType.NO_MOVE, 0));

            yield return new WaitForSeconds(3.0f);

            SetEnemyInfo(enemys[0], SetEnemyGenInfo(EnumData.MoveDirectionType.TOP_RIGHT, 0.4f, 0));

            // 2w
            SetEnemyInfo(enemys[1], SetEnemyGenInfo(EnumData.MoveDirectionType.BOTTOM, 0));
            SetEnemyInfo(enemys[2], SetEnemyGenInfo(EnumData.MoveDirectionType.BOTTOM, 0));

            yield return StartCoroutine(Wait_Y_PositionCheck(enemys[1], 0.5f));
            yield return StartCoroutine(Wait_Y_PositionCheck(enemys[2], 0.5f));

            SetEnemyInfo(enemys[1], SetEnemyGenInfo(EnumData.MoveDirectionType.RIGHT, 3.0f, 0));
            SetEnemyInfo(enemys[2], SetEnemyGenInfo(EnumData.MoveDirectionType.LEFT, 3.0f, 0));

            yield return new WaitForSeconds(1.0f);

            // 3w
            SetEnemyInfo(enemys[3], SetEnemyGenInfo(EnumData.MoveDirectionType.BOTTOM, 0));
            StartCoroutine(ChangeEnemyPattan(enemys[3], -1, EnumData.MoveDirectionType.TOP_RIGHT, 2.0f));

            yield return new WaitForSeconds(0.7f);

            SetEnemyInfo(enemys[4], SetEnemyGenInfo(EnumData.MoveDirectionType.BOTTOM, 0));
            StartCoroutine(ChangeEnemyPattan(enemys[4], -1, EnumData.MoveDirectionType.TOP_RIGHT, 2.0f));

            yield return new WaitForSeconds(0.7f);

            SetEnemyInfo(enemys[5], SetEnemyGenInfo(EnumData.MoveDirectionType.BOTTOM, 0));
            StartCoroutine(ChangeEnemyPattan(enemys[5], -1, EnumData.MoveDirectionType.TOP_RIGHT, 2.0f));

            yield return new WaitForSeconds(0.7f);

            SetEnemyInfo(enemys[6], SetEnemyGenInfo(EnumData.MoveDirectionType.BOTTOM, 0));
            StartCoroutine(ChangeEnemyPattan(enemys[6], -1, EnumData.MoveDirectionType.TOP_RIGHT, 2.0f));

            yield return new WaitForSeconds(0.7f);

            SetEnemyInfo(enemys[7], SetEnemyGenInfo(EnumData.MoveDirectionType.BOTTOM, 0));
            StartCoroutine(ChangeEnemyPattan(enemys[7], -1, EnumData.MoveDirectionType.TOP_RIGHT, 2.0f));

            yield return new WaitForSeconds(1.0f);

            // 4w
            SetEnemyInfo(enemys[8], SetEnemyGenInfo(EnumData.MoveDirectionType.BOTTOM, 0));
            StartCoroutine(ChangeEnemyPattan(enemys[8], -1, EnumData.MoveDirectionType.TOP_LEFT, 2.0f));

            yield return new WaitForSeconds(0.7f);

            SetEnemyInfo(enemys[9], SetEnemyGenInfo(EnumData.MoveDirectionType.BOTTOM, 0));
            StartCoroutine(ChangeEnemyPattan(enemys[9], -1, EnumData.MoveDirectionType.TOP_LEFT, 2.0f));

            yield return new WaitForSeconds(0.7f);

            SetEnemyInfo(enemys[10], SetEnemyGenInfo(EnumData.MoveDirectionType.BOTTOM, 0));
            StartCoroutine(ChangeEnemyPattan(enemys[10], -1, EnumData.MoveDirectionType.TOP_LEFT, 2.0f));

            yield return new WaitForSeconds(0.7f);

            SetEnemyInfo(enemys[11], SetEnemyGenInfo(EnumData.MoveDirectionType.BOTTOM, 0));
            StartCoroutine(ChangeEnemyPattan(enemys[11], -1, EnumData.MoveDirectionType.TOP_LEFT, 2.0f));

            yield return new WaitForSeconds(0.7f);

            SetEnemyInfo(enemys[12], SetEnemyGenInfo(EnumData.MoveDirectionType.BOTTOM, 0));
            StartCoroutine(ChangeEnemyPattan(enemys[12], -1, EnumData.MoveDirectionType.TOP_LEFT, 2.0f));
            
            // 5w
            SetEnemyInfo(enemys[13], SetEnemyGenInfo(EnumData.MoveDirectionType.TOP_RIGHT, 0));
            StartCoroutine(ChangeEnemyPattanList(enemys[13], Pattern1()));

            SetEnemyInfo(enemys[16], SetEnemyGenInfo(EnumData.MoveDirectionType.TOP_LEFT, 0));
            StartCoroutine(ChangeEnemyPattanList(enemys[16], Pattern2()));

            yield return new WaitForSeconds(1.5f);

            SetEnemyInfo(enemys[14], SetEnemyGenInfo(EnumData.MoveDirectionType.TOP_RIGHT, 0));
            StartCoroutine(ChangeEnemyPattanList(enemys[14], Pattern1(0.5f)));

            SetEnemyInfo(enemys[17], SetEnemyGenInfo(EnumData.MoveDirectionType.TOP_LEFT, 0));
            StartCoroutine(ChangeEnemyPattanList(enemys[17], Pattern2(0.5f)));

            yield return new WaitForSeconds(1.5f);

            SetEnemyInfo(enemys[15], SetEnemyGenInfo(EnumData.MoveDirectionType.TOP_RIGHT, 0));
            StartCoroutine(ChangeEnemyPattanList(enemys[15], Pattern1(1.0f)));

            SetEnemyInfo(enemys[18], SetEnemyGenInfo(EnumData.MoveDirectionType.TOP_LEFT, 0));
            StartCoroutine(ChangeEnemyPattanList(enemys[18], Pattern2(1.0f)));

            yield return new WaitForSeconds(5.0f);
        }

    }

    private IEnumerator ShowBoss()
    {
        yield return new WaitUntil(() => GameManager.instance.scoreManager.IsShowBoss());

        enemys[enemys.Count - 1].SetActive(true);
    }

    // pattern1
    private ChangePosition[] Pattern1(float hosei = 0f)
    {

        ChangePosition[] changePositionList = new ChangePosition[4];

        changePositionList[0] = new ChangePosition()
        {
            stopDirection = EnumData.StopDirectionType.Y,
            changePos = 0.5f + hosei,
            moveDirectionType = EnumData.MoveDirectionType.BOTTOM,
            speed = 1.0f
        };

        changePositionList[1] = new ChangePosition()
        {
            stopDirection = EnumData.StopDirectionType.Y,
            changePos = -1.0f,
            moveDirectionType = EnumData.MoveDirectionType.RIGHT,
            speed = 1.0f
        };

        changePositionList[2] = new ChangePosition()
        {
            stopDirection = EnumData.StopDirectionType.X,
            changePos = 2.0f,
            moveDirectionType = EnumData.MoveDirectionType.TOP,
            speed = 1.0f
        };

        changePositionList[3] = new ChangePosition()
        {
            stopDirection = EnumData.StopDirectionType.Y,
            changePos = 2.0f + hosei,
            moveDirectionType = EnumData.MoveDirectionType.LEFT,
            speed = 1.0f
        };

        return changePositionList;
    }

    // pattern1
    private ChangePosition[] Pattern2(float hosei = 0f)
    {
        ChangePosition[] changePositionList = new ChangePosition[4];

        changePositionList[0] = new ChangePosition()
        {
            stopDirection = EnumData.StopDirectionType.Y,
            changePos = 0.5f + hosei,
            moveDirectionType = EnumData.MoveDirectionType.BOTTOM,
            speed = 1.0f
        };

        changePositionList[1] = new ChangePosition()
        {
            stopDirection = EnumData.StopDirectionType.Y,
            changePos = -1.0f,
            moveDirectionType = EnumData.MoveDirectionType.LEFT,
            speed = 1.0f
        };

        changePositionList[2] = new ChangePosition()
        {
            stopDirection = EnumData.StopDirectionType.X,
            changePos = -2.0f,
            moveDirectionType = EnumData.MoveDirectionType.TOP,
            speed = 1.0f
        };

        changePositionList[3] = new ChangePosition()
        {
            stopDirection = EnumData.StopDirectionType.Y,
            changePos = 2.0f + hosei,
            moveDirectionType = EnumData.MoveDirectionType.RIGHT,
            speed = 1.0f
        };

        return changePositionList;
    }

    public IEnumerator ChangeEnemyPattan(GameObject enemyObj, float changePosition, EnumData.MoveDirectionType moveDirectionType, float speed)
    {
        yield return StartCoroutine(Wait_Y_PositionCheck(enemyObj, changePosition));

        SetEnemyInfo(enemyObj, SetEnemyGenInfo(moveDirectionType, speed, 0));
    }



    public struct ChangePosition
    {
        public EnumData.StopDirectionType stopDirection;
        public float changePos;
        public EnumData.MoveDirectionType moveDirectionType;
        public float speed;
    }

    public IEnumerator ChangeEnemyPattanList(GameObject enemyObj, ChangePosition[] changePositionList)
    {

        for (int i = 0; i < changePositionList.Length; i++)
        {
            switch (changePositionList[i].stopDirection)
            {
                case EnumData.StopDirectionType.X:
                    yield return StartCoroutine(Wait_X_PositionCheck(enemyObj, changePositionList[i].changePos)); 
                    break;
                case EnumData.StopDirectionType.Y:
                    yield return StartCoroutine(Wait_Y_PositionCheck(enemyObj, changePositionList[i].changePos));
                    break;
                default:
                    break;
            }

            SetEnemyInfo(enemyObj, SetEnemyGenInfo(changePositionList[i].moveDirectionType, changePositionList[i].speed, 0));
        }


    }

    private void SetMessage(EnumData.Speaker speaker, string message)
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

    private IEnumerator Wait_X_PositionCheck(GameObject obj, float targetPositionX, float tolerance = 0.1f)
    {
        while (Mathf.Abs(obj.transform.position.x - targetPositionX) > tolerance)
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

    EnemyGenInfo SetEnemyGenInfo(EnumData.MoveDirectionType enemyDirectionType, int shotPattern)
    {
        EnemyGenInfo info = new EnemyGenInfo();

        info.enemyDirectionType = enemyDirectionType;
        info.shotPattern = shotPattern;

        return info;
    }

    EnemyGenInfo SetEnemyGenInfo(EnumData.MoveDirectionType enemyDirectionType, float speed, int shotPattern)
    {
        EnemyGenInfo info = new EnemyGenInfo();

        info.enemyDirectionType = enemyDirectionType;
        info.firstSpeed = speed;
        info.shotPattern = shotPattern;

        return info;
    }

    EnemyGenInfo SetEnemyGenInfo(EnumData.MoveDirectionType enemyDirectionType, float xSpeed, float ySpeed, int shotPattern)
    {
        EnemyGenInfo info = new EnemyGenInfo();

        info.enemyDirectionType = enemyDirectionType;
        info.firstSpeed = xSpeed;
        info.secondSpeed = ySpeed;
        info.shotPattern = shotPattern;

        return info;
    }


}
