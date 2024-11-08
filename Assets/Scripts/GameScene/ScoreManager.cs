using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private const float ANGLE_MAX = 35.0f;
    private const float ANGLE_MIN = -35.0f;
    private const float OMP_SPEED = 1000.0f;
    private const int OMP_MAX_COUNT = 8;
    private const int CREAR_SCORE_POINT = 1000;

    private int scorePoint = 0;
    private int ompCount = 0;

    [SerializeField] private RectTransform ompObj;

    private Vector3 defaultOmpPos = new Vector3(0,0,0);

    private float currentAngle = 0;
    private bool angleChengeFlag = false;

    public int ScorePoint
    {
        get { return scorePoint; }
    }

    // Start is called before the first frame update
    void Start()
    {
        ompCount = OMP_MAX_COUNT;
        scorePoint = 0;
    }

    // Update is called once per frame
    void Update()
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
