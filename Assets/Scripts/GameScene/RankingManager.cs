using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RankingManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI bestTimeText, clearTimeText;
    [SerializeField] private GameObject rankingTextObj, parentContent;

    private void OnEnable()
    {
        SaveData data = SaveAndLoader.Load();

        data.rankingTime.Add(GameManager.instance.timeManager.GetTimer);

        Debug.Log(data.rankingTime[0]);
    }

    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
