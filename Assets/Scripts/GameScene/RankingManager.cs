using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RankingManager : MonoBehaviour
{
    private const int RANKING_MAX_DATA = 15;

    [SerializeField] private TextMeshProUGUI bestTimeText, clearTimeText;
    [SerializeField] private GameObject rankingTextObj, parentContent;

    private void OnEnable()
    {
        RankingSaveData data = SaveAndLoader.Load<RankingSaveData>();

        if (data.rankingData == null)
        {
            data.rankingData = new List<float>();
        }

        data.rankingData.Add(GameManager.instance.timeManager.GetTimer);

        data.rankingData.Sort();

        // ランキング数がマックスデータを超えていたらマックスデータになるまで削除する
        while (data.rankingData.Count > RANKING_MAX_DATA)
        {
            data.rankingData.RemoveAt(data.rankingData.Count - 1);
        }

        for (int i = 0; i < data.rankingData.Count; i++)
        {
            int rankingNumber = i + 1;
            float rankingValue = data.rankingData[i];

            TextMeshProUGUI rankingNumberText = rankingTextObj.GetComponent<TextMeshProUGUI>();
            rankingNumberText.text = rankingNumber.ToString() + "位:";
            
            TextMeshProUGUI rankingValueText = rankingTextObj.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            rankingValueText.text = rankingValue.ToString("F2") + "秒";
            
            Instantiate(rankingNumberText.gameObject, parentContent.transform);
        }

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
