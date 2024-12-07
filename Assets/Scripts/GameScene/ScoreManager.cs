using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private const float ANGLE_MAX = 35.0f;
    private const float ANGLE_MIN = -35.0f;
    private const float OMP_SPEED = 1000.0f;
    private const int OMP_MAX_COUNT = 8;
    private const int LV_2_POINT = 1000;
    private const int LV_3_POINT = 2000;
    private const int LV_4_POINT = 3000;
    private const int LV_5_POINT = 4000;
    private const int CREAR_SCORE_POINT = 10000;
    private Color COLOR_YELLOW = new Color(1f, 0.9f, 0.1f, 1f); // 黄色

    private int scorePoint = 0;
    private int ompCount = 0;

    private const string LV_UP = "Level UP";

    [SerializeField] private RectTransform ompObj;

    private Player playerScript;

    private Vector3 defaultOmpPos = new Vector3(0,0,0);

    private float currentAngle = 0;
    private bool angleChengeFlag = false;

    public int ScorePoint
    {
        get { return scorePoint; }
    }

    private void Unlock()
    {
        if (LV_5_POINT <= scorePoint)
        {
            LvUp(5);
        }
        else if (LV_4_POINT <= scorePoint)
        {
            LvUp(4);
            GameManager.instance.unlockCounter = 4;
        }
        else if (LV_3_POINT <= scorePoint)
        {
            LvUp(3);
            GameManager.instance.unlockCounter = 3;
        }
        else if (LV_2_POINT <= scorePoint)
        {
            LvUp(2);
            GameManager.instance.unlockCounter = 2;
        }
    }

    private void LvUp(int lv)
    {
        int currentLv = GameManager.instance.PlayerLv;

        if (currentLv != lv)
        {
            // レベルアップ
            GameManager.instance.PlayerLv = lv;

            if (playerScript)
            {
                playerScript.ShowText(LV_UP, COLOR_YELLOW);
            }
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        ompCount = OMP_MAX_COUNT;
        scorePoint = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Unlock();

        TrembleOmp();
    }

    private void TrembleOmp()
    {
        if (OMP_MAX_COUNT > ompCount)
        {
            if (angleChengeFlag)
            {
                currentAngle += Time.deltaTime * OMP_SPEED;
                if (currentAngle >= ANGLE_MAX)
                {
                    ompCount++;
                    angleChengeFlag = !angleChengeFlag;
                }
            }
            else
            {
                currentAngle -= Time.deltaTime * OMP_SPEED;
                if (currentAngle <= ANGLE_MIN)
                {
                    ompCount++;
                    angleChengeFlag = !angleChengeFlag;
                }
            }
        }
        else
        {
            currentAngle = 0;
        }

        ompObj.transform.rotation = Quaternion.Euler(0, 0, currentAngle);
    }

    public void SetScore(int point)
    {
        scorePoint += point;
        ompObj.transform.rotation = Quaternion.Euler(defaultOmpPos);
        ompCount = 0;
    }

    public bool CrearCheck()
    {
        if (CREAR_SCORE_POINT <= scorePoint)
        {
            return true;
        }

        return false;
    }
}
