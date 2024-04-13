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
        PLAYER3,
    }

    [System.Serializable]
    public struct MessageData
    {
        public Speaker speaker;
        public string message;
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
        // 1フレーム止める
        yield return null;

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
        
        if (currentLine == 0)
        {
            yield return new WaitForSeconds(1.0f);
            eventManager.ToGameScene();
        }

        isMsgFullText = true;

    }

    private void SetImage(Speaker speaker)
    {
        switch (speaker)
        {
            case Speaker.JUON:
                eventManager.ChengeImage(0, "ジュオン");
                break;
            case Speaker.SATOKO:
                eventManager.ChengeImage(1, "サトコ");
                break;
            case Speaker.PLAYER3:
                eventManager.ChengeImage(2, "Player");
                break;
            default:
                break;
        }
    }

}
