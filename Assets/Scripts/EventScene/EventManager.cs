using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EventManager : MonoBehaviour
{
    [SerializeField] private List<Sprite> charImageList;

    [SerializeField] private GameObject charImageRight, charImageLeft;

    [SerializeField] private GameObject charNameRight, charNameLeft;

    private TextMeshProUGUI rightText, leftText;

    public Image charRight, charLeft;

    private Color defaultColor = new Color(1f, 1f, 1f, 1f);
    private Color alfaColor = new Color(0.3f, 0.3f, 0.3f, 0.9f);

    private string nowMsgPlayer = null;
    private bool msgLeftFlag = false;

    // Start is called before the first frame update
    void Start()
    {
        msgLeftFlag = true;
        CharImageOff();
    }

    public void ChengeImage(int num, string charNmae)
    {
        rightText = charNameRight.GetComponentInChildren<TextMeshProUGUI>();
        leftText = charNameLeft.GetComponentInChildren<TextMeshProUGUI>();

        ImagePosCheck(charNmae);

        if (msgLeftFlag)
        {
            // 左側にキャラクターを表示
            leftText.text = charNmae;
            rightText.text = "";
            charLeft.sprite = charImageList[num];
            charLeft.color = defaultColor;
            charRight.color = alfaColor;

            charImageLeft.SetActive(true);

            charNameLeft.SetActive(true);

        }
        else
        {
            // 右側にキャラクターを表示
            rightText.text = charNmae;
            leftText.text = "";
            charRight.sprite = charImageList[num];
            charRight.color = defaultColor;
            charLeft.color = alfaColor;

            charImageRight.SetActive(true);

            charNameRight.SetActive(true);
        }
    }

    private void CharImageOff()
    {
        charImageRight.SetActive(false);
        charImageLeft.SetActive(false);
        charNameRight.SetActive(false);
        charNameLeft.SetActive(false);
    }

    private void ImagePosCheck( string msgPlayer )
    {
        if (nowMsgPlayer != msgPlayer)
        {
            nowMsgPlayer = msgPlayer;
            msgLeftFlag = !msgLeftFlag;
        }
    }

}
