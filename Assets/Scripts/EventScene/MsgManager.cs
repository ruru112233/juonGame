using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MsgManager : MonoBehaviour
{
    private float delay = 0.1f;
    public string[] fullTexts;
    public TextMeshProUGUI msgText;

    private int currentLine = 0;
    private bool isClicked = false;
    private bool isMsgFullText = false;

    public EventManager eventManager;

    // Start is called before the first frame update
    void Start()
    {
        msgText.text = "";
        StartCoroutine(ShowText());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            bool canAccessPreviousLine = currentLine > 0;
            bool isNotLastLine = currentLine < fullTexts.Length;

            if (!string.IsNullOrEmpty(msgText.text) &&
                ((isNotLastLine && isMsgFullText && canAccessPreviousLine && msgText.text != fullTexts[currentLine - 1]) ||
                (!isMsgFullText && msgText.text != fullTexts[currentLine]))) 
            {
                    isClicked = true;
            }
            else
            {
                StopAllCoroutines();
                StartCoroutine(ShowText());
            }

        }
    }

    IEnumerator ShowText()
    {
        isClicked = false;
        isMsgFullText = false;

        //string fullText = fullTexts[currentLine];

        string fullText = SetText(currentLine);
        msgText.text = "";

        for (int i = 0; i < fullText.Length; i++)
        {
            if (isClicked)
            {
                msgText.text = fullText;
                isClicked = false;
                break;
            }

            if (i + 1 < fullText.Length)
            {

                if ((fullText[i + 1] == '。' || fullText[i + 1] == '、' || fullText[i + 1] == '？'))
                {
                    msgText.text += fullText[i].ToString() + fullText[i + 1].ToString();
                    i++;
                }
                else
                {
                    msgText.text += fullText[i];
                }
            }
            
            yield return new WaitForSeconds(delay);
        }

        currentLine = (currentLine + 1) % fullTexts.Length;
        isMsgFullText = true;

    }

    private string SetText(int currentLine)
    {
        string msg = "";
        SetImage(currentLine);
        msg = fullTexts[currentLine];

        return msg;
    }

    private void SetImage(int currentLine)
    {
        switch (currentLine)
        {
            case 0:
                eventManager.ChengeImage(EventManager.ImagePosition.LEFT, 0, "キャラA");
                break;
            case 1:
                eventManager.ChengeImage(EventManager.ImagePosition.RIGHT, 1, "キャラB");
                break;
            case 2:
                eventManager.ChengeImage(EventManager.ImagePosition.LEFT, 2, "キャラC");
                break;
            case 3:
                break;
            case 4:
                break;
            case 5:
                break;
            case 6:
                break;
            default:
                break;
        }
    }

}
