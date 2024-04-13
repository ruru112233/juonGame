using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnEvent : MonoBehaviour
{
    public void StartBtn()
    {
        SceneManager.LoadScene("EventScene");
    }


}
