using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public GameObject bossHpSliderObj;

    [SerializeField] private Text scorePointText;
    private int scorePoint;

    // Start is called before the first frame update
    void Start()
    {
        bossHpSliderObj.SetActive(false);

        scorePoint = 0;
        scorePointText.text = scorePoint.ToString();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetScore(int value)
    {
        scorePoint += value;
        scorePointText.text = scorePoint.ToString();
    }

}
