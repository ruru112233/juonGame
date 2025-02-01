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

    // アイテムの種類
    public enum ItemPattern
    {
        JIMI_GUITAR,    // ジミヘンギター
        JOHN_GUITAR,    // ジョンギター
        THUNDER,        // 稲妻
        AT_POWER_UP,    // 攻撃力アップ
        SP_POWER_UP,    // スピードアップ
        MAGNET,         // マグネット
        UNSETTILED,     // 未確定 
    }

    public enum MoveAction
    {
        UP,
        DOWN,
        RIGHT,
        LEFT,
        STOP,
    }

    // 上下左右の移動
    public enum ActionPattern
    {
        RUSH,
        MOVE_L_AND_R,
        UP_MOVE,
        INITIAL,
    }

    public enum StopDirectionType
    {
        X, // x軸でストップ
        Y, // y軸でストップ
    }

    public enum MoveDirectionType
    {
        TOP,            // 上方向
        RIGHT,          // 右方向
        BOTTOM,         // 下方向
        LEFT,           // 左方向
        TOP_RIGHT,      // 右上方向
        BOTTOM_RIGHT,   // 右下方向
        TOP_LEFT,       // 左上方向
        BOTTOM_LEFT,    // 左下方向
        NO_MOVE,        // 移動しない
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
