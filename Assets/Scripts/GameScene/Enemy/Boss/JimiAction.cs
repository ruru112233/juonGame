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
    private enum MoveAction
    {
        UP,
        DOWN,
        RIGHT,
        LEFT,
        STOP,
    }

    // 上下左右の移動
    private enum ActionPattern
    {
        RUSH,
        MOVE_L_AND_R,
        UP_MOVE,
        INITIAL,
    }

    private MoveAction moveAction;
    private ActionPattern actionPattern;

    // エネミーのHP関係
    private int enemyMaxHp = 0;
    private int enemyHp = 0;

    private float speed = 2.0f;

    // Itemオブジェクトのドロップする数
    private int dropItemCount = 5;

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
        moveAction = MoveAction.DOWN;
        actionPattern = ActionPattern.INITIAL;

        rightMoveFlag = true;

        anime = this.GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        UpdateActionPattern();
        BossMove();

        BossAction();


    }


    private void BossMove()
    {
        switch (moveAction)
        {
            case MoveAction.DOWN:
                transform.position += new Vector3(0, -speed * Time.deltaTime, 0);
                break;
            case MoveAction.STOP:
            default:
                break;
        }
    }

    private void BossAction()
    {
        switch (actionPattern)
        {
            case ActionPattern.RUSH:

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
                            ChangeState(ActionPattern.MOVE_L_AND_R);
                        }
                    }
                    else
                    {
                        transform.position += new Vector3(0, (-speed * 2) * Time.deltaTime, 0);
                    }

                }

                break;
            case ActionPattern.MOVE_L_AND_R:
                
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

    void ChangeState(ActionPattern actionPattern)
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
            case ActionPattern.RUSH:
                break;
            case ActionPattern.MOVE_L_AND_R:
                if (this.patternTimer > MOVE_LR_END_TIME)
                {
                    ChangeState(ActionPattern.RUSH);
                }
                break;
            case ActionPattern.INITIAL:
                if (transform.position.y <= BASE_POS_Y)
                {
                    moveAction = MoveAction.STOP;
                    actionPattern = ActionPattern.MOVE_L_AND_R;
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

            //hpSlider.value = enemyHp_;
            if (enemyHp <= 0)
            {
                // ダメージアニメーション
                if (anime)
                {
                    anime.SetTrigger("damage");
                }

                // MaxHpの更新
                enemyHp = enemyMaxHp;
                ScatterItem();
                //Destroy(gameObject);
            }
        }
    }

    // Itemを散らして配置する関数
    private void ScatterItem()
    {
        for (int i = 0; i < dropItemCount; i++)
        {
            GameObject obj = SetItemObj();

            if (obj)
            {
                Vector3 randomItemPos = RandomPosition(transform.position);
                Instantiate(obj, randomItemPos, Quaternion.Euler(0, 0, 45));
            }


        }
    }

    // Itemオブジェクトをランダムに配置するための関数
    private Vector3 RandomPosition(Vector3 targetPos)
    {
        Vector3 pos = targetPos;

        pos.x = Random.Range(targetPos.x - 0.5f, targetPos.x + 0.5f);
        pos.y = Random.Range(targetPos.y - 0.5f, targetPos.y + 0.5f);

        return pos;
    }

    private GameObject SetItemObj()
    {
        GameObject obj = null;

        int rand = Random.Range(0, 5);

        switch (rand)
        {
            case 0:
                obj = GameManager.instance.itemStock.jimiGuiterObj;
                break;
            case 1:
            case 2:
                obj = GameManager.instance.itemStock.johnGuiterObj;
                break;
            case 3:
            case 4:
                obj = GameManager.instance.itemStock.thunder;
                break;
            default:
                Debug.Log("SetItemObj error");
                break;
        }

        return obj;
    }

}
