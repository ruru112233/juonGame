using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeManager : MonoBehaviour
{
    const string TIME_MSG = "Time:";

    public TextMeshProUGUI timerText;

    private float timer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        timerText.text = TIME_MSG + timer.ToString("F1");
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.isStopped) return;

        timer += Time.deltaTime;
        timerText.text = TIME_MSG + timer.ToString("F1");
    }
}
