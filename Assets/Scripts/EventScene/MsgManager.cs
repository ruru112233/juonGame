using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MsgManager : Msg
{
    [SerializeField] private GameObject scoreHandImage;
    [SerializeField] private GameObject textField;

    private List<MessageData> messageList = new List<MessageData>();

    private float delay = 0.1f;

    public TextMeshProUGUI msgText;

    private int currentLine = 0;
    private bool isClicked = false;
    private bool isMsgFullText = false;

    public EventManager eventManager;

    public GameObject TextFieldObj
    {
        get { return textField; }
    }

    public List<MessageData> MessageList
    {
        get { return messageList; }
        set { messageList = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (textField) textField.SetActive(false);
        if (scoreHandImage) scoreHandImage.SetActive(false);

        SaveData data = SaveAndLoader.Load();

        delay = (float)data.msgSpeed / 15;

        msgText.text = "";

        //if (messageList.Count > 0) StartCoroutine(ShowText(messageList));

    }

    public void StartMessage(List<MessageData> messageListData)
    {
        if (messageListData.Count > 0) StartCoroutine(ShowText(messageListData));
    }

    // Update is called once per frame
    void Update()
    {
        MessageStart();
    }

    public void ActiveHandArrow(Vector3 pos)
    {
        scoreHandImage.transform.position = pos;
        scoreHandImage.SetActive(true);
    }

    public void MessageStart()
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
                if(messageList.Count > 0)
                {
                    StartCoroutine(ShowText(messageList));
                }
                else
                {
                    GameManager.instance.isStopped = false;
                    textField.SetActive(false);
                    scoreHandImage.SetActive(false);
                }

            }
        }
    }

    public IEnumerator ShowText(List<MessageData> messages)
    {
        // 1フレーム止める
        yield return null;

        textField.SetActive(true);

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

        currentLine = (currentLine + 1) % messages.Count;

        if (currentLine == 0)
        {
            messageList.Clear();
        }

        if ((SceneManager.GetActiveScene().name == "EventScene") && (currentLine == 0) && !GameManager.crearFlag)
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
            case Speaker.NONE:
                eventManager.NoneImage();
                break;
            default:
                break;
        }
    }

}
