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

    public enum ImagePosition
    {
        RIGHT,
        LEFT,
        NONE,
    }

    // Start is called before the first frame update
    void Start()
    {
        
       CharImageOff();
    }

    public void ChengeImage(ImagePosition imagePos, int num, string charNmae)
    {
        rightText = charNameRight.GetComponentInChildren<TextMeshProUGUI>();
        leftText = charNameLeft.GetComponentInChildren<TextMeshProUGUI>();
        
        if (imagePos == ImagePosition.RIGHT)
        {
            //CharImageOff();
            
            rightText.text = charNmae;
            leftText.text = "";
            charRight.sprite = charImageList[num];
            charRight.color = defaultColor;
            charLeft.color = alfaColor;

            charImageRight.SetActive(true);

            charNameRight.SetActive(true);
        }
        else if (imagePos == ImagePosition.LEFT)
        {
            //CharImageOff();
            
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

        }
    }

    private void CharImageOff()
    {
        charImageRight.SetActive(false);
        charImageLeft.SetActive(false);
        charNameRight.SetActive(false);
        charNameLeft.SetActive(false);
    }

}
