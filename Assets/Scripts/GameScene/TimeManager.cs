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
        timerText.text = TIME_MSG + timer.ToString("F1");
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.scoreManager.IsClear())
        {
            //StartCoroutine(BlinkingTimerText());
            timerText.text = TIME_MSG + timer.ToString("F1");
        }
        else
        {
            timer += Time.deltaTime;
            timerText.text = TIME_MSG + timer.ToString("F1");
        }
    }

    private IEnumerator BlinkingTimerText()
    {
        timerText.text = TIME_MSG + "";
        yield return new WaitForSeconds(BlinkingTime);

        timerText.text = TIME_MSG + timer.ToString("F1");
        yield return new WaitForSeconds(BlinkingTime);

        //timerText.gameObject.SetActive(false);
        //yield return new WaitForSeconds(BlinkingTime);

        //timerText.gameObject.SetActive(true);

        //yield return new WaitForSeconds(BlinkingTime);

        //timerText.gameObject.SetActive(false);
        //yield return new WaitForSeconds(BlinkingTime);

        //timerText.gameObject.SetActive(true);

        //yield return new WaitForSeconds(BlinkingTime);

        //timerText.gameObject.SetActive(false);
        //yield return new WaitForSeconds(BlinkingTime);

        //timerText.gameObject.SetActive(true);

    }

}
