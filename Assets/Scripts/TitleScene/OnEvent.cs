using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnEvent : MonoBehaviour
{
    [SerializeField] GameObject optionPanel;

    private void Start()
    {
        optionPanel.SetActive(false);
    }

    public void StartBtn()
    {
        SceneManager.LoadScene("EventScene");
    }

    public void OptionButton()
    {
        optionPanel.SetActive(true);
    }

    public void CloseOptionPanel()
    {
        optionPanel.SetActive(false);
    }

}
