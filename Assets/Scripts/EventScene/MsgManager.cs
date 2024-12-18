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

    private void SetMessage(EnumData.Speaker speaker, string message)
    {
        Msg.MessageData msg;
        msg.speaker = speaker;
        msg.message = message;
        messageList.Add(msg);
    }

    private void SetOpningMessageList()
    {
        SetMessage(EnumData.Speaker.NONE, "�̑�Ȃ鉹�y�̐�l������@�l��͒m�肽��");
        SetMessage(EnumData.Speaker.NONE, "���̍��̂��Ȃ�������@��̉���z����@���y��t�łĂ����́H");
        SetMessage(EnumData.Speaker.NONE, "���̖��Ȃ͂ǂ�����č��ꂽ�̂�");
        SetMessage(EnumData.Speaker.NONE, "���̃o���h�͂ǂ����ĉ��U�����̂�");
        SetMessage(EnumData.Speaker.NONE, "���̃\���͂���Ő����Ȃ̂�");
        SetMessage(EnumData.Speaker.NONE, "�����������Ƃ͎R�قǂ���̂�@�^�C���}�V�����܂��Ȃ���@�̂ɖ߂��ĕ������Ƃ�����Ȃ�");
        SetMessage(EnumData.Speaker.NONE, "�����邠�Ȃ��Ƃ�@�����Ȃ��Ȃ��Ă��瑁���N...");
        SetMessage(EnumData.Speaker.NONE, "���͂ǂ��ɂ���́H");
        SetMessage(EnumData.Speaker.NONE, "�������Ă���́H");
        SetMessage(EnumData.Speaker.NONE, "���Ȃ��̂��Ȃ����E��@�l��͂Ȃ�Ƃ���炵�Ă���");
        SetMessage(EnumData.Speaker.NONE, "����ɂ��Ă�@���̂𗣂ꂽ�ނ�͍�@�ǂ��ŉ������Ă���̂��낤��");
        SetMessage(EnumData.Speaker.NONE, "�\�ɂ���@�ނ�͍����ǂ����ŏW�܂���@���y������Ă���炵��");
        SetMessage(EnumData.Speaker.NONE, "�����͉F��?@�V��?");
        SetMessage(EnumData.Speaker.NONE, "�ڂ���͑傫�ȑD�ɏ����@�ނ�̂Ƃ���֌�����...");
        SetMessage(EnumData.Speaker.NONE, "���Ȃ��̂��Ȃ����E��@�l��͂Ȃ�Ƃ���炵�Ă���");

    }

    private void SetEndingMessageList()
    {
        SetMessage(EnumData.Speaker.JUON, "����Ƃ����܂ł�����");
        SetMessage(EnumData.Speaker.SATOKO, "�ڂ���̂�������Ƃ��ꂳ��͂ǂ��H");
        SetMessage(EnumData.Speaker.JUON, "�����I�W�~�H@�������ɂ���̂̓u���C�A���W���[���Y�H");
        SetMessage(EnumData.Speaker.SATOKO, "�������ɍ����Ă�̂̓W���j�X����Ȃ��H@����́c�����I�W�������\���H");
        SetMessage(EnumData.Speaker.JUON, "�J�[�g�����邼�H");
        SetMessage(EnumData.Speaker.SATOKO, "�����I");
        SetMessage(EnumData.Speaker.NONE, "�N�����ɂ͂܂�������c�B");
        SetMessage(EnumData.Speaker.JUON, "�킠�[�[�[�[�I�I�I�I�I");
        SetMessage(EnumData.Speaker.NONE, "����Ƃ��̏ꏊ���������Ǝv�����̂�@�l��͂܂��ӂ肾���ɖ߂���Ă��܂����I@@�Â��c");
    }

    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.instance.eventSceneType == EnumData.EventSceneType.OPNING)
        {
            Debug.Log("Opning");
            SetOpningMessageList();
            if (messageList.Count > 0) StartCoroutine(ShowText(messageList));
        }
        else if (GameManager.instance.eventSceneType == EnumData.EventSceneType.ENDING)
        {
            SetEndingMessageList();
            if (textField) textField.SetActive(false);
            if (scoreHandImage) scoreHandImage.SetActive(false);
        }



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

        if ((SceneManager.GetActiveScene().name == "EventScene") && (currentLine == 0))
        {
            yield return new WaitForSeconds(1.0f);
            eventManager.ToGameScene();
        }

        isMsgFullText = true;

    }

    private void SetImage(EnumData.Speaker speaker)
    {
        switch (speaker)
        {
            case EnumData.Speaker.JUON:
                eventManager.ChengeImage(0, "�W���I��");
                break;
            case EnumData.Speaker.SATOKO:
                eventManager.ChengeImage(1, "�T�g�R");
                break;
            case EnumData.Speaker.PLAYER3:
                eventManager.ChengeImage(2, "Player");
                break;
            case EnumData.Speaker.NONE:
                eventManager.NoneImage();
                break;
            default:
                break;
        }
    }

}
