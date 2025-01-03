using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MoveSceneManager : MonoBehaviour
{
    public void SelfSceneButton(float time)
    {
        StartCoroutine(MoveScene(time, SceneManager.GetActiveScene().name));
    }

    public void TitleSceneButton(float time)
    {
        StartCoroutine(MoveScene(time, "TitleScene"));
    }

    public void EndingSceneButton(float time)
    {
        StartCoroutine(MoveScene(time, "GameCrearScene"));
    }

    public void Rankinghidden()
    {
        GameObject rankingPanel = GameObject.FindGameObjectWithTag("RankingPanel");

        if (rankingPanel)
        {
            RetryButtonHidden();
            EndingButtonHidden();
            rankingPanel.SetActive(false);
        }
    }

    public void OnEnding()
    {
        GameManager.instance.isEnding = true;
    }

    private void RetryButtonHidden()
    {
        GameObject retryButton = GameObject.FindGameObjectWithTag("RetryButton");

        if (retryButton)
        {
            Button button = retryButton.GetComponent<Button>();
            button.interactable = false;

            Image buttnImage = retryButton.GetComponent<Image>();
            buttnImage.color = new Color(0, 0, 0, 0);

            TextMeshProUGUI text = retryButton.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
            text.color = new Color(0, 0, 0, 0);
        }
    }

    private void EndingButtonHidden()
    {
        GameObject endingButton = GameObject.FindGameObjectWithTag("EndingButton");

        if (endingButton)
        {
            Button button = endingButton.GetComponent<Button>();
            button.interactable = false;

            Image buttnImage = endingButton.GetComponent<Image>();
            buttnImage.color = new Color(0, 0, 0, 0);

            TextMeshProUGUI text = endingButton.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
            text.color = new Color(0, 0, 0, 0);
        }
    }

    IEnumerator MoveScene(float time, string sceneName)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(sceneName);
    }
}
