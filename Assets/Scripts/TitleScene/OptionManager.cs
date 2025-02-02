using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class OptionManager : MonoBehaviour
{
    private const int MAX_MSG_SPEED = 3;
    private const int MIN_MSG_SPEED = 1;
    private const int MAX_VOLUME = 10;
    private const int MIN_VOLUME = 0;

    [SerializeField] TextMeshProUGUI msgSpd1, msgSpd2, msgSpd3, msgSpd4, msgSpd5, msgSpd6;

    [SerializeField] private Scrollbar bgmVolumeScrollbar, seVolumeScrollbar;

    Color colorInit = new Color32(147, 147, 147, 255);
    Color decisionColor = new Color32(255, 255, 255, 255);

    private int msgSpeedSelectNum = 0;
    private int bgmVolumeNum = 3;
    private int seVolumeNum = 3;

    struct VolumeData
    {
        public int bgmVolume;
        public int seVolume;
    }

    // Start is called before the first frame update
    void Start()
    {
        SpeedColorInit();

        SettingSaveData data = SaveAndLoader.Load<SettingSaveData>();

        msgSpeedSelectNum = data.msgSpeed;
        bgmVolumeNum = data.bgmVolume;
        seVolumeNum = data.seVolume;

        bgmVolumeScrollbar.value = SetVolume(bgmVolumeNum) ;
        seVolumeScrollbar.value = SetVolume(seVolumeNum);

        AudioManager.instance.BgmSliderVolume(bgmVolumeScrollbar.value);
        AudioManager.instance.SeSliderVolume(seVolumeScrollbar.value);

        SettingMsgSpeed(data.msgSpeed);
    }

    void SpeedColorInit()
    {
        if (!IsMsgObj()) return;

        msgSpd1.color = colorInit;
        msgSpd2.color = colorInit;
        msgSpd3.color = colorInit;
        msgSpd4.color = colorInit;
        msgSpd5.color = colorInit;
        msgSpd6.color = colorInit;
    }

    public void RightSelectButton()
    {
        if (MIN_MSG_SPEED >= msgSpeedSelectNum) return;

        AudioManager.instance.PlaySE((int)EnumData.SeType.SELECT);

        msgSpeedSelectNum--;

        SettingMsgSpeed(msgSpeedSelectNum);

    }

    public void LeftSelectButton()
    {
        if (MAX_MSG_SPEED <= msgSpeedSelectNum) return;

        AudioManager.instance.PlaySE((int)EnumData.SeType.SELECT);

        msgSpeedSelectNum++;

        SettingMsgSpeed(msgSpeedSelectNum);
    }

    public void LeftBgmSelectButton()
    {
        if (MIN_VOLUME >= bgmVolumeNum) return;

        AudioManager.instance.PlaySE((int)EnumData.SeType.SELECT);

        bgmVolumeNum--;

        VolumeSave();
    }

    public void RightBgmSelectButton()
    {
        if (MAX_VOLUME <= bgmVolumeNum) return;

        AudioManager.instance.PlaySE((int)EnumData.SeType.SELECT);

        bgmVolumeNum++;

        VolumeSave();
    }

    public void LeftSeSelectButton()
    {
        if (MIN_VOLUME >= seVolumeNum) return;

        AudioManager.instance.PlaySE((int)EnumData.SeType.SELECT);

        seVolumeNum--;

        VolumeSave();
    }

    public void RightSeSelectButton()
    {
        if (MAX_VOLUME <= seVolumeNum) return;

        AudioManager.instance.PlaySE((int)EnumData.SeType.SELECT);

        seVolumeNum++;

        VolumeSave();
    }

    void SetVolumeScrollBar(VolumeData volumeData)
    {
        bgmVolumeScrollbar.value = SetVolume(volumeData.bgmVolume);
        seVolumeScrollbar.value = SetVolume(volumeData.seVolume);

        AudioManager.instance.BgmSliderVolume(bgmVolumeScrollbar.value);
        AudioManager.instance.SeSliderVolume(seVolumeScrollbar.value);
    }

    void VolumeSave()
    {
        VolumeData volumeData = new VolumeData();

        volumeData.bgmVolume = bgmVolumeNum;
        volumeData.seVolume = seVolumeNum;

        OptionSave(volumeData);
    }

    void OptionSave(EnumData.MsgSpeedType msgSpeedType)
    {
        SettingSaveData data = SaveAndLoader.Load<SettingSaveData>();

        data.msgSpeed = (int)msgSpeedType;

        SaveAndLoader.Save(data);
    }

    void OptionSave(VolumeData volumeData)
    {
        SettingSaveData data = SaveAndLoader.Load<SettingSaveData>();

        data.bgmVolume = volumeData.bgmVolume;
        data.seVolume = volumeData.seVolume;

        SetVolumeScrollBar(volumeData);

        SaveAndLoader.Save(data);
    }

    private bool IsMsgObj()
    {
        return msgSpd1 || msgSpd2 || msgSpd3 || msgSpd4 || msgSpd5 || msgSpd6;
    }

    private float SetVolume(int num)
    {
        switch (num)
        {
            case 0:
                return 0.0f;
            case 1:
                return 0.1f;
            case 2:
                return 0.2f;
            case 3:
                return 0.3f;
            case 4:
                return 0.4f;
            case 5:
                return 0.5f;
            case 6:
                return 0.6f;
            case 7:
                return 0.7f;
            case 8:
                return 0.8f;
            case 9:
                return 0.9f;
            case 10:
                return 1.0f;
        }
        return 0.0f;
    }


    void SettingMsgSpeed(int msgSpeed)
    {
        SpeedColorInit();

        if (!IsMsgObj()) return;

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
