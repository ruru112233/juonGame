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
        SetMessage(EnumData.Speaker.JUON, "あっ！ジミ？@あそこにいるのはブライアンジョーンズ？");
        SetMessage(EnumData.Speaker.SATOKO, "あそこに座ってるのはジャニスじゃない？@あれは…うそ！ジムモリソン？");
        SetMessage(EnumData.Speaker.JUON, "カートもいるぞ？");
        SetMessage(EnumData.Speaker.SATOKO, "あっ！");
        SetMessage(EnumData.Speaker.NONE, "君たちにはまだ早いよ…。");
        SetMessage(EnumData.Speaker.JUON, "わあーーーー！！！！！");
        SetMessage(EnumData.Speaker.NONE, "やっとその場所を見つけたと思ったのに@僕らはまたふりだしに戻されてしまった！@@つづく…");
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
                eventManager.ChengeImage(0, "ジュオン");
                break;
            case EnumData.Speaker.SATOKO:
                eventManager.ChengeImage(1, "サトコ");
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
