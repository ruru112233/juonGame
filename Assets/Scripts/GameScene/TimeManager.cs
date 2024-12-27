using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeManager : MonoBehaviour
{
    const string TIME_MSG = "Time:";
    const float BlinkingTime = 0.5f;

    public TextMeshProUGUI timerText;

    private float timer = 0.0f;

    public float GetTimer
    {
        get { return timer; }
    }

    // Start is called before the first frame update
    void Start()
    {
        timerText.text = TIME_MSG + timer.ToString("F2");
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.scoreManager.IsClear())
        {
            timer += Time.deltaTime;
        }

        timerText.text = TIME_MSG + timer.ToString("F2");
    }

}
