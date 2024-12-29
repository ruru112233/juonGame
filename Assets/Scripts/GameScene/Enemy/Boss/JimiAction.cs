using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JimiAction : MonoBehaviour
{
    private float BASE_POS_Y = 3.0f;
    private float UNDER_POS_Y = -3.0f;
    private float BASE_RIGHT = 2.0f;
    private float BASE_LEFT = -2.0f;

    private float MOVE_LR_END_TIME = 3.0f;
    private float RASH_START = 2.0f;
    private bool isClear = false;

    private EnumData.MoveAction moveAction;
    private EnumData.ActionPattern actionPattern;

    // エネミーのHP関係
    private int enemyMaxHp = 0;
    private int enemyHp = 0;

    private float speed = 2.0f;

    private Animator anime;

    // カウンター関係
    private float rushCounter = 0;
    private float patternTimer = 0;


    // フラグ関係
    private bool rightMoveFlag = false;
    private bool upFlag = false;

    // Start is called before the first frame update
    void Start()
    {
        enemyMaxHp = 10;
        enemyHp = enemyMaxHp;
        moveAction = EnumData.MoveAction.DOWN;
        actionPattern = EnumData.ActionPattern.INITIAL;

        rightMoveFlag = true;

        anime = this.GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.scoreManager.IsClear() && !isClear)
        {
            isClear = true;
            StartCoroutine(GameClearAction());
        }
        else if(!isClear)
        {
            UpdateActionPattern();
            BossAction();
        }
        BossMove();
    }

    private IEnumerator GameClearAction()
    {
        moveAction = EnumData.MoveAction.STOP;

        yield return new WaitForSeconds(1.0f);

        moveAction = EnumData.MoveAction.UP;
    }


    private void BossMove()
    {
        switch (moveAction)
        {
            case EnumData.MoveAction.UP:
                transform.position += new Vector3(0, speed * Time.deltaTime, 0);
                break;
            case EnumData.MoveAction.DOWN:
                transform.position += new Vector3(0, -speed * Time.deltaTime, 0);
                break;
            case EnumData.MoveAction.STOP:
            default:
                break;
        }
    }

    private void BossAction()
    {
        switch (actionPattern)
        {
            case EnumData.ActionPattern.RUSH:

                rushCounter += Time.deltaTime;

                if (transform.position.y <= UNDER_POS_Y)
                {
                    upFlag = true;
                }

                if (rushCounter >= RASH_START)
                {

                    if (upFlag)
                    {
                        transform.position += new Vector3(0, speed * Time.deltaTime, 0);

                        if (transform.position.y >= BASE_POS_Y)
                        {
                            ChangeState(EnumData.ActionPattern.MOVE_L_AND_R);
                        }
                    }
                    else
                    {
                        transform.position += new Vector3(0, (-speed * 2) * Time.deltaTime, 0);
                    }

                }

                break;
            case EnumData.ActionPattern.MOVE_L_AND_R:
                
                if (rightMoveFlag)
                {
                    transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
                    if (transform.position.x > BASE_RIGHT)
                    {
                        rightMoveFlag = false;
                    }
                }
                else
                {
                    transform.position += new Vector3(-speed * Time.deltaTime, 0, 0);
                    if (transform.position.x < BASE_LEFT)
                    {
                        rightMoveFlag = true;
                    }
                }
                break;
            default:
                break;
        }
    }

    void ChangeState(EnumData.ActionPattern actionPattern)
    {
        this.actionPattern = actionPattern;
        this.patternTimer = 0;
        this.rushCounter = 0;
        this.upFlag = false;
    }

    void UpdateActionPattern()
    {
        this.patternTimer += Time.deltaTime;
        switch (this.actionPattern)
        {
            case EnumData.ActionPattern.RUSH:
                break;
            case EnumData.ActionPattern.MOVE_L_AND_R:
                if (this.patternTimer > MOVE_LR_END_TIME)
                {
                    ChangeState(EnumData.ActionPattern.RUSH);
                }
                break;
            case EnumData.ActionPattern.INITIAL:
                if (transform.position.y <= BASE_POS_Y)
                {
                    moveAction = EnumData.MoveAction.STOP;
                    actionPattern = EnumData.ActionPattern.MOVE_L_AND_R;
                }
                break;
            default:
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            enemyHp--;

            if (enemyHp <= 0)
            {
                // ダメージアニメーション
                if (anime)
                {
                    anime.SetTrigger("damage");
                }

                // MaxHpの更新
                enemyHp = enemyMaxHp;
                Common.ScatterItem(this.transform);
            }
        }
    }
}
