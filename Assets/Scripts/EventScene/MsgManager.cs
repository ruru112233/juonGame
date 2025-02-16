using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MsgManager : Msg
{
    [SerializeField] private GameObject scoreHandImage;
    [SerializeField] private GameObject textField,nextText;

    private List<MessageData> messageList = new List<MessageData>();

    private float delay = 0.1f;

    public TextMeshProUGUI msgText;

    private int currentLine = 0;
    private bool isClicked = false;
    private bool isMsgFullText = false;

    public bool IsMsgFullText
    {
        get { return isMsgFullText; }
        set { isMsgFullText = value; }
    }

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
        SetMessage(EnumData.Speaker.NONE, "偉大なる音楽の先人たちよ@僕らは知りたい");
        SetMessage(EnumData.Speaker.NONE, "あの頃のあなたたちは@一体何を想って@音楽を奏でていたの？");
        SetMessage(EnumData.Speaker.NONE, "あの名曲はどうやって作られたのか");
        SetMessage(EnumData.Speaker.NONE, "あのバンドはどうして解散したのか");
        SetMessage(EnumData.Speaker.NONE, "あのソロはあれで正解なのか");
        SetMessage(EnumData.Speaker.NONE, "聴きたいことは山ほどあるのに@タイムマシンがまだない今@昔に戻って聞くことも叶わない");
        SetMessage(EnumData.Speaker.NONE, "愛するあなたとも@逢えなくなってから早数年...");
        SetMessage(EnumData.Speaker.NONE, "今はどこにいるの？");
        SetMessage(EnumData.Speaker.NONE, "何をしているの？");
        SetMessage(EnumData.Speaker.NONE, "あなたのいない世界で@僕らはなんとか暮らしている");
        SetMessage(EnumData.Speaker.NONE, "それにしても@肉体を離れた彼らは今@どこで何をしているのだろうか");
        SetMessage(EnumData.Speaker.NONE, "噂によると@彼らは今もどこかで集まって@音楽をやっているらしい");
        SetMessage(EnumData.Speaker.NONE, "そこは宇宙?@天国?");
        SetMessage(EnumData.Speaker.NONE, "ぼくらは大きな船に乗って@彼らのところへ向かう...");
        SetMessage(EnumData.Speaker.NONE, "あなたのいない世界で@僕らはなんとか暮らしている");

    }

    private void SetEndingMessageList()
    {
        SetMessage(EnumData.Speaker.JUON, "やっとここまできたぞ");
        SetMessage(EnumData.Speaker.SATOKO, "ぼくらのお父さんとお母さんはどこ？");
        SetMessage(EnumData.Speaker.JUON, "あれ？上の方から何か音が聞こえる…");
        SetMessage(EnumData.Speaker.SATOKO, "行ってみましょう");
        SetMessage(EnumData.Speaker.NONE, "STOP");
        SetMessage(EnumData.Speaker.JUON, "あっ！ジミ？@あそこにいるのはブライアンジョーンズ？");
        SetMessage(EnumData.Speaker.SATOKO, "あそこに座ってるのはジャニスじゃない？@あれは…うそ！ジムモリソン？");
        SetMessage(EnumData.Speaker.JUON, "カートもいるぞ？");
        SetMessage(EnumData.Speaker.SATOKO, "あっ！");
        SetMessage(EnumData.Speaker.NONE, "君たちにはまだ早いよ…。");
        SetMessage(EnumData.Speaker.NONE, "WAIT");
        SetMessage(EnumData.Speaker.JUON, "わあーーーー！！！！！");
        SetMessage(EnumData.Speaker.NONE, "やっとその場所を見つけたと思ったのに@僕らはまたふりだしに戻されてしまった！@@つづく…");
    }

    // Start is called before the first frame update
    void Start()
    {
        if (nextText) nextText.SetActive(false);

        if (GameManager.instance.eventSceneType == EnumData.EventSceneType.OPNING)
        {
            SetOpningMessageList();
            if (messageList.Count > 0) StartCoroutine(ShowText(messageList));
        }
        else if (GameManager.instance.eventSceneType == EnumData.EventSceneType.ENDING)
        {
            SetEndingMessageList();
            if (textField) textField.SetActive(true);
            if (scoreHandImage) scoreHandImage.SetActive(false);
        }

        SettingSaveData data = SaveAndLoader.Load<SettingSaveData>();

        delay = (float)data.msgSpeed / 15;

        msgText.text = "";
    }

    public void StartMessage(List<MessageData> messageListData)
    {
        if (messageListData.Count > 0) StartCoroutine(ShowText(messageListData));
    }

    [SerializeField] private GameObject backPanel;

    // Update is called once per frame
    void Update()
    {
        //GameObject backPanel = GameObject.FindGameObjectWithTag("BackPanel");
        //if (backPanel && isLastEnding)
        //{
        //    backPanel.SetActive(true);
        //    Image backPanelImage = backPanel.GetComponent<Image>();

        //    backPanelImage.color += new Color(0, 0, 0, Time.deltaTime * 0.5f);
        //    if (backPanelImage.color.a >= 0.99f)
        //    {
        //        isLastMessage = true;
        //        backPanel.SetActive(false);

        //        Debug.Log("aaaaa");

        //        //isMsgFullText = true;
        //        //EndingPlayer player = GameObject.FindGameObjectWithTag("EndingPlayer").GetComponent<EndingPlayer>();
        //        //player.OffJoyStick();
        //        //StartMessage(messageList);

        //    }
        //}

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
            if (isTitle)
            {
                AudioManager.instance.PlaySE((int)EnumData.SeType.SELECT);
                SceneManager.LoadScene("TitleScene");
            }

            if (!isMsgFullText)
            {
                isClicked = true;
            }
            else
            {
                if (!isWaitMessage) StopAllCoroutines();
                if(messageList.Count > 0)
                {
                    if (!isWaitMessage)
                    {
                        StartCoroutine(ShowText(messageList));
                    }
                }
                else
                {
                    if (GameManager.instance) GameManager.instance.isStopped = false;
                    if(textField) textField.SetActive(false);
                    if(scoreHandImage) scoreHandImage.SetActive(false);

                    if ((SceneManager.GetActiveScene().name == "EventScene") && (currentLine == 0))
                    {
                        if (eventManager) eventManager.ToGameScene();
                    }
                }
            }
        }
    }

    private bool isLastMessage = false;
    private bool isWaitMessage = false;
    private bool isLastEnding = false;
    private bool isTitle = false;

    public bool IsWaitMessage
    {
        get { return isLastMessage; }
        set { isWaitMessage = value; }
    }

    private IEnumerator LastEnding()
    {
        while (!isLastEnding)
        {
            yield return null;
        }

        if(GameManager.instance.lastEndingPanel) GameManager.instance.lastEndingPanel.SetActive(true);

        isWaitMessage = true;
        isLastMessage = true;
    }

    public IEnumerator ShowText(List<MessageData> messages)
    {
        if (nextText) nextText.SetActive(false);

        // 1フレーム止める
        yield return null;

        textField.SetActive(true);

        isClicked = false;
        isMsgFullText = false;

        string fullText = messages[currentLine].message;
        msgText.text = "";

        while (messages[currentLine].message == "WAIT" && !isLastMessage)
        {
            StartCoroutine(LastEnding());
            isLastEnding = true;
            currentLine ++;
            fullText = messages[currentLine].message;
            yield return null;
        }

        if (messages[currentLine].message == "STOP")
        {
            if (GameManager.instance) GameManager.instance.isStopped = false;
            EndingPlayer player = GameObject.FindGameObjectWithTag("EndingPlayer").GetComponent<EndingPlayer>();
            player.OnJoyStick();
            currentLine++;
            if (eventManager) eventManager.CharImageOff();
            if (textField) textField.SetActive(false);
            yield break;
        }

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

        while (isWaitMessage)
        {
            yield return null;
        }



        currentLine = (currentLine + 1) % messages.Count;

        string msg = "Next >>";

        if (currentLine == 0)
        {
            if (GameManager.instance.eventSceneType == EnumData.EventSceneType.ENDING)
            {
                isTitle = true;
                msg = "Go To Title >>";
            }
            else
            {
                msg = "Go To Play >>";
            }

            messageList.Clear();
        }

        if (nextText)
        {
            nextText.GetComponent<TextMeshProUGUI>().text = msg;
            nextText.SetActive(true);
        }

        isMsgFullText = true;

    }

    private void SetImage(EnumData.Speaker speaker)
    {
        switch (speaker)
        {
            case EnumData.Speaker.JUON:
                if (eventManager) eventManager.ChengeImage(0, "ジュオン");
                break;
            case EnumData.Speaker.SATOKO:
                if (eventManager) eventManager.ChengeImage(1, "サトコ");
                break;
            case EnumData.Speaker.PLAYER3:
                if (eventManager) eventManager.ChengeImage(2, "Player");
                break;
            case EnumData.Speaker.NONE:
                if (eventManager) eventManager.NoneImage();
                break;
            default:
                break;
        }
    }

}
