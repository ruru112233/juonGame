using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EventManager : MonoBehaviour
{
    [SerializeField] private List<Sprite> charImageList;

    [SerializeField] private GameObject charImageRight, charImageLeft;

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
        charRight = charImageRight.GetComponent<Image>();
        charLeft = charImageLeft.GetComponent<Image>();
    }

    public void ChengeImage(ImagePosition imagePos, int num)
    {
        if (imagePos == ImagePosition.RIGHT)
        {
            charRight.sprite = charImageList[num];
        }
        else if (imagePos == ImagePosition.LEFT)
        {
            charLeft.sprite = charImageList[num];
        }
        else
        {

        }
    }

}
