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

        string fullText = fullTexts[currentLine];
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

                if ((fullText[i + 1] == 'B' || fullText[i + 1] == 'A' || fullText[i + 1] == 'H'))
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

}
