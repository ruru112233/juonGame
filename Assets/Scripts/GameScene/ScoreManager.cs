using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    private const int SHOW_BOSS_POINT = 6000;
    private const int CREAR_SCORE_POINT = 10000;
    private const float FADE_DURATION = 1.2f;
    private Color COLOR_YELLOW = new Color(1f, 0.9f, 0.1f, 1f); // 黄色
    private Color COLOR_SHOU_CLEAR_JOUKEN_TEXT = new Color(1, 0.390566f, 0.6449661f, 1);
    private float FADE_OUT_ALFAR1 = 0.5f;
    private float HOLD_TIME = 0.5f;

    private int scorePoint = 0;
    private int ompCount = 0;

    [SerializeField] private GameObject clearJoukenText;

    private const string LV_UP = "Level UP";

    [SerializeField] private RectTransform ompObj;

    private Vector3 defaultOmpPos = new Vector3(0,0,0);

    private float currentAngle = 0;
    private bool angleChengeFlag = false;

    public int ScorePoint
    {
        get { return scorePoint; }
    }

    public bool IsShowBoss()
    {
        return scorePoint >= SHOW_BOSS_POINT;
    }

    public bool IsClear()
    {
        return scorePoint >= CREAR_SCORE_POINT;
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

    private IEnumerator ShowClearJoukenText()
    {
        TextMeshProUGUI text = clearJoukenText.GetComponent<TextMeshProUGUI>();
        text.text = "ギターを取って「" + CREAR_SCORE_POINT + "」点集めよう";
        text.color = COLOR_SHOU_CLEAR_JOUKEN_TEXT;
        Color tempColor = text.color;
        tempColor.a = 0f;
        text.color = tempColor;

        while (text.color.a < 1.0f)
        {
            tempColor.a += Time.deltaTime / FADE_DURATION;
            text.color = tempColor;
            yield return null;
        }

        tempColor.a = 1.0f;
        text.color = tempColor;

        yield return new WaitForSeconds(HOLD_TIME);

        while (text.color.a > FADE_OUT_ALFAR1)
        {
            tempColor.a -= Time.deltaTime / FADE_DURATION;
            text.color = tempColor;
            yield return null;
        }

        tempColor.a = FADE_OUT_ALFAR1;
        text.color = tempColor;

        while (text.color.a < 1.0f)
        {
            tempColor.a += Time.deltaTime / FADE_DURATION;
            text.color = tempColor;
            yield return null;
        }

        tempColor.a = 1.0f;
        text.color = tempColor;

        yield return new WaitForSeconds(HOLD_TIME);


        while (text.color.a > FADE_OUT_ALFAR1)
        {
            tempColor.a -= Time.deltaTime / FADE_DURATION;
            text.color = tempColor;
            yield return null;
        }

        tempColor.a = FADE_OUT_ALFAR1;
        text.color = tempColor;

        while (text.color.a < 1.0f)
        {
            tempColor.a += Time.deltaTime / FADE_DURATION;
            text.color = tempColor;
            yield return null;
        }

        tempColor.a = 1.0f;
        text.color = tempColor;

        while (text.color.a > 0.0f)
        {
            tempColor.a -= Time.deltaTime / FADE_DURATION;
            text.color = tempColor;
            yield return null;
        }

        clearJoukenText.SetActive(false);
    }

    private void LvUp(int lv)
    {
        int currentLv = GameManager.instance.PlayerLv;

        if (currentLv != lv)
        {
            // レベルアップ
            GameManager.instance.PlayerLv = lv;

            GameManager.instance.player.ShowText(LV_UP, COLOR_YELLOW);
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ShowClearJoukenText());

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
}
