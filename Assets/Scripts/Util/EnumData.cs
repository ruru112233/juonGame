using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EnumData
{
    public enum MsgSpeedType
    {
        NONE,
        SPEED_1,
        SPEED_2,
        SPEED_3,
        SPEED_4,
        SPEED_5,
        SPEED_6,
    }

    public enum EventSceneType
    {
        OPNING,
        ENDING,
    }

    public enum InstanceObjType
    {
        PICK_BULLET,
        STICK_BULLET,
        ENEMY_BULLET,
        JIMI_ITEM,
        JOHN_ITEM,
        THUNDER_ITEM,
        AT_UP_ITEM,
        SP_UP_ITEM,
        MAGNET_ITEM,
    }

    public enum Speaker
    {
        NONE,
        JUON,
        SATOKO,
        PLAYER3,
    }

    public enum POS_TYPE
    {
        RIGHT,
        LEFT,
    }

    public enum SelectPanelType
    {
        TITLE,
        RETRY,
    }

    // �A�C�e���̎��
    public enum ItemPattern
    {
        JIMI_GUITAR,    // �W�~�w���M�^�[
        JOHN_GUITAR,    // �W�����M�^�[
        THUNDER,        // ���
        AT_POWER_UP,    // �U���̓A�b�v
        SP_POWER_UP,    // �X�s�[�h�A�b�v
        MAGNET,         // �}�O�l�b�g
        UNSETTILED,     // ���m�� 
    }

    public enum MoveAction
    {
        UP,
        DOWN,
        RIGHT,
        LEFT,
        STOP,
    }

    // �㉺���E�̈ړ�
    public enum ActionPattern
    {
        RUSH,
        MOVE_L_AND_R,
        UP_MOVE,
        INITIAL,
    }

    public enum StopDirectionType
    {
        X, // x���ŃX�g�b�v
        Y, // y���ŃX�g�b�v
    }

    public enum MoveDirectionType
    {
        TOP,            // �����
        RIGHT,          // �E����
        BOTTOM,         // ������
        LEFT,           // ������
        TOP_RIGHT,      // �E�����
        BOTTOM_RIGHT,   // �E������
        TOP_LEFT,       // �������
        BOTTOM_LEFT,    // ��������
        NO_MOVE,        // �ړ����Ȃ�
    }

    public enum MoveState
    {
        FRONT,
        RIGHT,
        LEFT,
        BACK,
    }

    public enum SaveDataType
    {
        SETTINGS,
        RANKING,
    }

    public enum SeType
    {
        CANCEL,
        HIT,
        POWER_UP,
        SCENE_SENI,
        SELECT,
    }
}
