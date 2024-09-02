using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OptionManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI msgSpd1, msgSpd2, msgSpd3, msgSpd4, msgSpd5, msgSpd6;

    Color colorInit = new Color32(147, 147, 147, 255);

    // Start is called before the first frame update
    void Start()
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
}
