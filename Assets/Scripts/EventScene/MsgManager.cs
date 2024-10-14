using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MsgManager : Msg
{

    public Msg messageEvent1;
    public Msg crearEvent;

    private MessageData[] messageList;

    private float delay = 0.1f;

    public TextMeshProUGUI msgText;

    private int currentLine = 0;
    private bool isClicked = false;
    private bool isMsgFullText = false;

    public EventManager eventManager;

    [SerializeField] private GameObject textField;

    public GameObject TextFieldObj
    {
        get { return textField; }
    }

    public MessageData[] MessageList
    {
        get { return messageList; }
    }


    // Start is called before the first frame update
    void Start()
    {
        SaveData data = SaveAndLoader.Load();

        delay = (float)data.msgSpeed / 15;

        msgText.text = "";

        if (GameManager.crearFlag)
        {
            textField.SetActive(false);
            messageList = crearEvent.messages;
        }
        else
        {
            messageList = messageEvent1.messages;
            StartCoroutine(ShowText(messageList));
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        MessageStart();
    }

    public void MessageStart()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("aaa");
            if (!isMsgFullText)
            {
                Debug.Log("bbnb");

                isClicked = true;
            }
            else
            {

                Debug.Log("ccc");

                StopAllCoroutines();
                StartCoroutine(ShowText(messageList));
            }
        }
    }

    public IEnumerator ShowText(MessageData[] messages)
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
                // 改行コードの置換
                string newfullText = fullText.Replace("@", "\r\n");

                msgText.text = newfullText;
                isClicked = false;
                break;
            }

            if ('@' == fullText[i])
            {
                msgText.text += "\r\n";
            }
            else
            {
                msgText.text += fullText[i];
            }

            yield return new WaitForSeconds(delay);
        }

        currentLine = (currentLine + 1) % messages.Length;
        
        if ((currentLine == 0) && !GameManager.crearFlag)
        {
            yield return new WaitForSeconds(1.0f);
            eventManager.ToGameScene();
        }
        else
        {
            // クリア後で会話も終了した時の処理をここに書く
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
            case Speaker.NONE:
                eventManager.NoneImage();
                break;
            default:
                break;
        }
    }

}
