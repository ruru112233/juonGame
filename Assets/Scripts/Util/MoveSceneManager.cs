using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MoveSceneManager : MonoBehaviour
{
    public GameObject titleSelectviewPanel, retrySelectviewPanel, volumePanel;
    public GameObject joyPad;

    private void Start()
    {
        if (titleSelectviewPanel) titleSelectviewPanel.SetActive(false);
        if (retrySelectviewPanel) retrySelectviewPanel.SetActive(false);
        if (volumePanel) volumePanel.SetActive(false);
    }

    public void SelfSceneButton()
    {
        float time = 0;
        CloseSelectPanelView();
        AudioManager.instance.PlaySE((int)EnumData.SeType.SELECT);
        StartCoroutine(MoveScene(time, SceneManager.GetActiveScene().name));
    }

    public void SelfSceneButton(float time)
    {
        CloseSelectPanelView();
        AudioManager.instance.PlaySE((int)EnumData.SeType.SELECT);
        StartCoroutine(MoveScene(time, SceneManager.GetActiveScene().name));
    }

    public void TitleSceneButton()
    {
        float time = 0;
        CloseSelectPanelView();
        AudioManager.instance.PlaySE((int)EnumData.SeType.SELECT);
        StartCoroutine(MoveScene(time, "TitleScene"));
    }

    public void TitleSceneButton(float time)
    {
        CloseSelectPanelView();
        AudioManager.instance.PlaySE((int)EnumData.SeType.SELECT);
        StartCoroutine(MoveScene(time, "TitleScene"));
    }

    public void EndingSceneButton(float time)
    {
        CloseSelectPanelView();
        AudioManager.instance.PlaySE((int)EnumData.SeType.SELECT);
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

    public void CloseButton()
    {
        AudioManager.instance.PlaySE((int)EnumData.SeType.CANCEL);
        if (titleSelectviewPanel) titleSelectviewPanel.SetActive(false);
        if (retrySelectviewPanel) retrySelectviewPanel.SetActive(false);
        if (volumePanel) volumePanel.SetActive(false);
        if (joyPad) joyPad.SetActive(true);
        Time.timeScale = 1;
    }

    public void CloseSelectPanelView()
    {
        if (titleSelectviewPanel) titleSelectviewPanel.SetActive(false);
        if (retrySelectviewPanel) retrySelectviewPanel.SetActive(false);
        if (volumePanel) volumePanel.SetActive(false);
        if (joyPad) joyPad.SetActive(true);
        Time.timeScale = 1;
    }


    public void ShowSecletPanel(int num)
    {
        if (titleSelectviewPanel && retrySelectviewPanel && volumePanel)
        {
            AudioManager.instance.PlaySE((int)EnumData.SeType.SELECT);
            CloseSelectPanelView();
            switch (num)
            {
                case 0:
                    titleSelectviewPanel.SetActive(true);
                    break;
                case 1:
                    retrySelectviewPanel.SetActive(true);
                    break;
                case 2:
                    volumePanel.SetActive(true);
                    break;
                default:
                    break;
            }
        }

        Time.timeScale = 0;
        joyPad.SetActive(false);
    }
}
