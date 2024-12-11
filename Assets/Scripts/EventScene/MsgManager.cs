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

    private void SetMessage(Speaker speaker, string message)
    {
        Msg.MessageData msg;
        msg.speaker = speaker;
        msg.message = message;
        messageList.Add(msg);
    }

    private void SetOpningMessageList()
    {
        SetMessage(Speaker.NONE, "�̑�Ȃ鉹�y�̐�l������@�l��͒m�肽��");
        SetMessage(Speaker.NONE, "���̍��̂��Ȃ�������@��̉���z����@���y��t�łĂ����́H");
        SetMessage(Speaker.NONE, "���̖��Ȃ͂ǂ�����č��ꂽ�̂�");
        SetMessage(Speaker.NONE, "���̃o���h�͂ǂ����ĉ��U�����̂�");
        SetMessage(Speaker.NONE, "���̃\���͂���Ő����Ȃ̂�");
        SetMessage(Speaker.NONE, "�����������Ƃ͎R�قǂ���̂�@�^�C���}�V�����܂��Ȃ���@�̂ɖ߂��ĕ������Ƃ�����Ȃ�");
        SetMessage(Speaker.NONE, "�����邠�Ȃ��Ƃ�@�����Ȃ��Ȃ��Ă��瑁���N...");
        SetMessage(Speaker.NONE, "���͂ǂ��ɂ���́H");
        SetMessage(Speaker.NONE, "�������Ă���́H");
        SetMessage(Speaker.NONE, "���Ȃ��̂��Ȃ����E��@�l��͂Ȃ�Ƃ���炵�Ă���");
        SetMessage(Speaker.NONE, "����ɂ��Ă�@���̂𗣂ꂽ�ނ�͍�@�ǂ��ŉ������Ă���̂��낤��");
        SetMessage(Speaker.NONE, "�\�ɂ���@�ނ�͍����ǂ����ŏW�܂���@���y������Ă���炵��");
        SetMessage(Speaker.NONE, "�����͉F��?@�V��?");
        SetMessage(Speaker.NONE, "�ڂ���͑傫�ȑD�ɏ����@�ނ�̂Ƃ���֌�����...");
        SetMessage(Speaker.NONE, "���Ȃ��̂��Ȃ����E��@�l��͂Ȃ�Ƃ���炵�Ă���");
    }

    private void SetEndingMessageList()
    {
        SetMessage(Speaker.JUON, "����Ƃ����܂ł�����");
        SetMessage(Speaker.SATOKO, "�ڂ���̂�������Ƃ��ꂳ��͂ǂ��H");
        SetMessage(Speaker.JUON, "�����I�W�~�H@�������ɂ���̂̓u���C�A���W���[���Y�H");
        SetMessage(Speaker.SATOKO, "�������ɍ����Ă�̂̓W���j�X����Ȃ��H@����́c�����I�W�������\���H");
        SetMessage(Speaker.JUON, "�J�[�g�����邼�H");
        SetMessage(Speaker.SATOKO, "�����I");
        SetMessage(Speaker.NONE, "�N�����ɂ͂܂�������c�B");
        SetMessage(Speaker.JUON, "�킠�[�[�[�[�I�I�I�I�I");
        SetMessage(Speaker.NONE, "����Ƃ��̏ꏊ���������Ǝv�����̂�@�l��͂܂��ӂ肾���ɖ߂���Ă��܂����I@@�Â��c");
    }

    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.instance.eventSceneType == GameManager.EventSceneType.OPNING)
        {
            Debug.Log("Opning");
            SetOpningMessageList();
            if (messageList.Count > 0) StartCoroutine(ShowText(messageList));
        }
        else if (GameManager.instance.eventSceneType == GameManager.EventSceneType.ENDING)
        {
            SetEndingMessageList();
        }

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
                    if(GameManager.instance) GameManager.instance.isStopped = false;
                    if(textField) textField.SetActive(false);
                    if(scoreHandImage) scoreHandImage.SetActive(false);
                }

            }
        }
    }

    public IEnumerator ShowText(List<MessageData> messages)
    {
        // 1�t���[���~�߂�
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
                // ���s�R�[�h�̒u��
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
                eventManager.ChengeImage(0, "�W���I��");
                break;
            case Speaker.SATOKO:
                eventManager.ChengeImage(1, "�T�g�R");
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
