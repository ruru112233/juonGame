using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Msg : MonoBehaviour
{
    public enum Speaker
    {
        NONE,
        JUON,
        SATOKO,
        PLAYER3,
    }

    [System.Serializable]
    public struct MessageData
    {
        public Speaker speaker;
        public string message;
    }

    public MessageData[] messages;

}
