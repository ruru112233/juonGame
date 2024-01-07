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

    private Image charRight, charLeft;

    public enum ImagePosition
    {
        RIGHT,
        LEFT,
        NONE,
    }

    // Start is called before the first frame update
    void Start()
    {
        
        //CharImageOff();
    }

    public void ChengeImage(ImagePosition imagePos, int num, string charNmae)
    {
        if (imagePos == ImagePosition.RIGHT)
        {
            CharImageOff();
            charImageRight.SetActive(true);
            charRight = charImageRight.GetComponent<Image>();
            charNameRight.SetActive(true);
            TextMeshProUGUI rightText = charNameRight.GetComponentInChildren<TextMeshProUGUI>();
            rightText.text = charNmae;
            charRight.sprite = charImageList[num];
        }
        else if (imagePos == ImagePosition.LEFT)
        {
            CharImageOff();
            charImageLeft.SetActive(true);
            charLeft = charImageLeft.GetComponent<Image>();
            charNameLeft.SetActive(true);
            TextMeshProUGUI leftText = charNameLeft.GetComponentInChildren<TextMeshProUGUI>();
            leftText.text = charNmae;
            charLeft.sprite = charImageList[num];
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
