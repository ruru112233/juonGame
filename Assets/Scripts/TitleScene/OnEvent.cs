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
        AudioManager.instance.PlaySE((int)EnumData.SeType.SELECT);
        AudioManager.instance.PlaySE((int)EnumData.SeType.SCENE_SENI);
        SceneManager.LoadScene("EventScene");
    }

    public void OptionButton()
    {
        AudioManager.instance.PlaySE((int)EnumData.SeType.SELECT);
        optionPanel.SetActive(true);
    }

    public void CloseOptionPanel()
    {
        AudioManager.instance.PlaySE((int)EnumData.SeType.SELECT);
        optionPanel.SetActive(false);
    }

}
