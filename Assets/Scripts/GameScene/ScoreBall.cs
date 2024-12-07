using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreBall : MonoBehaviour
{
    private const string LV_UP = "Level UP";

    private float magnification = 1.0f;
    private float magCut = 0.01f;

    private int scorePoint;

    // Start is called before the first frame update
    void Start()
    {
        magnification = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        TargetMove();
    }

    private void TargetMove()
    {
        float itemSpeed = 2.0f;

        GameObject target = GameObject.FindGameObjectWithTag("ScorePos");

        if (target)
        {
            Vector3 distance = (target.transform.position - this.transform.position).normalized;
            this.transform.position += (distance * itemSpeed) * (Time.deltaTime * magnification);
            magnification += magCut;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("ScorePoint"))
        {
            string scoreMsg = "       ";

            TextMeshProUGUI text = GameObject.FindGameObjectWithTag("ScorePoint").GetComponent<TextMeshProUGUI>();

            if (text)
            {
                GameManager.instance.scoreManager.SetScore(scorePoint);
                text.text = scoreMsg + GameManager.instance.scoreManager.ScorePoint;
            }

            Destroy(gameObject);
        }
    }

    public void SetScorePoint(int point)
    {
        scorePoint = point;
    }
}
