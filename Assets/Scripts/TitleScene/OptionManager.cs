using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OptionManager : MonoBehaviour
{
    private const int MAX_MSG_SPEED = 3;
    private const int MIN_MSG_SPEED = 1;

    [SerializeField] TextMeshProUGUI msgSpd1, msgSpd2, msgSpd3, msgSpd4, msgSpd5, msgSpd6;

    Color colorInit = new Color32(147, 147, 147, 255);
    Color decisionColor = new Color32(255, 255, 255, 255);

    private int msgSpeedSelectNum = 0;

    // Start is called before the first frame update
    void Start()
    {
        SpeedColorInit();

        SaveData data = SaveAndLoader.Load();

        msgSpeedSelectNum = data.msgSpeed;

        SettingMsgSpeed(data.msgSpeed);
    }

    void SpeedColorInit()
    {
        msgSpd1.color = colorInit;
        msgSpd2.color = colorInit;
        msgSpd3.color = colorInit;
        msgSpd4.color = colorInit;
        msgSpd5.color = colorInit;
        msgSpd6.color = colorInit;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RightSelectButton()
    {
        if (MIN_MSG_SPEED >= msgSpeedSelectNum) return;

        msgSpeedSelectNum--;

        SettingMsgSpeed(msgSpeedSelectNum);

    }

    public void LeftSelectButton()
    {
        if (MAX_MSG_SPEED <= msgSpeedSelectNum) return;

        msgSpeedSelectNum++;

        SettingMsgSpeed(msgSpeedSelectNum);
    }

    void OptionSave(EnumData.MsgSpeedType msgSpeedType)
    {
        SaveData data = SaveAndLoader.Load();

        data.msgSpeed = (int)msgSpeedType;

        SaveAndLoader.Save(data);
    }


    void SettingMsgSpeed(int msgSpeed)
    {
        SpeedColorInit();

        switch (msgSpeed)
        {
            case 1:
                msgSpd1.color = decisionColor;
                OptionSave(EnumData.MsgSpeedType.SPEED_1);
                break;
            case 2:
                msgSpd2.color = decisionColor;
                OptionSave(EnumData.MsgSpeedType.SPEED_2);
                break;
            case 3:
                msgSpd3.color = decisionColor;
                OptionSave(EnumData.MsgSpeedType.SPEED_3);
                break;
            case 4:
                msgSpd4.color = decisionColor;
                OptionSave(EnumData.MsgSpeedType.SPEED_4);
                break;
            case 5:
                msgSpd5.color = decisionColor;
                OptionSave(EnumData.MsgSpeedType.SPEED_5);
                break;
            case 6:
                msgSpd6.color = decisionColor;
                OptionSave(EnumData.MsgSpeedType.SPEED_6);
                break;
            default:
                break;
        }
    }
}
