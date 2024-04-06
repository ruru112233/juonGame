using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MsgManager : MonoBehaviour
{

    public enum Speaker
    {
        JUON,
        SATOKO,
    }

    [System.Serializable]
    public struct MessageData
    {
        public Speaker speaker;
        public string message;
        public EventManager.ImagePosition imagePos;
    }

    private float delay = 0.1f;
    public MessageData[] messages;
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
            if (!isMsgFullText)
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
        yield return new WaitForSeconds(delay);
        isClicked = false;
        isMsgFullText = false;

        string fullText = messages[currentLine].message;
        msgText.text = "";

        SetImage(messages[currentLine].speaker);

        for (int i = 0; i < fullText.Length; i++)
        {
            if (isClicked)
            {
                msgText.text = fullText;
                isClicked = false;
                break;
            }

            msgText.text += fullText[i];
            yield return new WaitForSeconds(delay);
        }

        currentLine = (currentLine + 1) % messages.Length;
        isMsgFullText = true;

    }

    private void SetImage(Speaker speaker)
    {
        switch (speaker)
        {
            case Speaker.JUON:
                eventManager.ChengeImage(messages[currentLine].imagePos, 0, "ジュオン");
                break;
            case Speaker.SATOKO:
                eventManager.ChengeImage(messages[currentLine].imagePos, 1, "サトコ");
                break;
            default:
                break;
        }
    }

}
