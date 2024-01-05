using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private int scorePoint = 0;

    public int ScorePoint
    {
        get { return scorePoint; }
    }

    // Start is called before the first frame update
    void Start()
    {
        scorePoint = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetScore(int point)
    {
        scorePoint += point;
    }
}
