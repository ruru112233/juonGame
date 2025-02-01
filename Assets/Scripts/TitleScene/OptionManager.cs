using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class OptionManager : MonoBehaviour
{
    private const int MAX_MSG_SPEED = 3;
    private const int MIN_MSG_SPEED = 1;
    private const float MAX_VOLUME = 0.9f;
    private const float MIN_VOLUME = 0.1f;
    private const float VOLUME_DISTANCE = 0.2f;

    [SerializeField] TextMeshProUGUI msgSpd1, msgSpd2, msgSpd3, msgSpd4, msgSpd5, msgSpd6;

    [SerializeField] private Scrollbar bgmVolumeScrollbar, seVolumeScrollbar;

    Color colorInit = new Color32(147, 147, 147, 255);
    Color decisionColor = new Color32(255, 255, 255, 255);

    private int msgSpeedSelectNum = 0;
    private float bgmVolumeNum = 0.6f;
    private float seVolumeNum = 0.6f;

    struct VolumeData
    {
        public float bgmVolume;
        public float seVolume;
    }

    // Start is called before the first frame update
    void Start()
    {
        SpeedColorInit();

        SettingSaveData data = SaveAndLoader.Load<SettingSaveData>();

        msgSpeedSelectNum = data.msgSpeed;
        bgmVolumeNum = data.bgmVolume;
        seVolumeNum = data.seVolume;

        bgmVolumeScrollbar.value = bgmVolumeNum;
        seVolumeScrollbar.value = seVolumeNum;

        AudioManager.instance.BgmSliderVolume(bgmVolumeNum);
        AudioManager.instance.SeSliderVolume(seVolumeNum);

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

        bgmVolumeNum -= VOLUME_DISTANCE;

        VolumeSave();
    }

    public void RightBgmSelectButton()
    {
        if (MAX_VOLUME <= bgmVolumeNum) return;

        AudioManager.instance.PlaySE((int)EnumData.SeType.SELECT);

        bgmVolumeNum += VOLUME_DISTANCE;

        VolumeSave();
    }

    public void LeftSeSelectButton()
    {
        if (MIN_VOLUME >= seVolumeNum) return;

        AudioManager.instance.PlaySE((int)EnumData.SeType.SELECT);

        seVolumeNum -= VOLUME_DISTANCE;

        VolumeSave();
    }

    public void RightSeSelectButton()
    {
        if (MAX_VOLUME <= seVolumeNum) return;

        AudioManager.instance.PlaySE((int)EnumData.SeType.SELECT);

        seVolumeNum += VOLUME_DISTANCE;

        VolumeSave();
    }

    void SetVolumeScrollBar(VolumeData volumeData)
    {
        bgmVolumeScrollbar.value = volumeData.bgmVolume;
        seVolumeScrollbar.value = volumeData.seVolume;

        AudioManager.instance.BgmSliderVolume(volumeData.bgmVolume);
        AudioManager.instance.SeSliderVolume(volumeData.seVolume);
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
