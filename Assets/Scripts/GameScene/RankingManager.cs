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
        RankingSaveData data = SaveAndLoader.Load<RankingSaveData>();

        data.rankingData.Add(GameManager.instance.timeManager.GetTimer);

        Debug.Log(data.rankingData[0]);

        SaveAndLoader.Save(data);
    }

    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
