using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    IEnumerator MoveScene(float time, string sceneName)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(sceneName);
    }
}
