using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Msg : MonoBehaviour
{

    [System.Serializable]
    public struct MessageData
    {
        public EnumData.Speaker speaker;
        public string message;
    }

    public MessageData[] messages;

}
