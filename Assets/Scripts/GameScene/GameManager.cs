using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static bool crearFlag = false;

    public bool isStopped = false;

    public int unlockCounter = 1;

    public ScoreManager scoreManager;

    public ItemStock itemStock;

    public int PlayerLv { get; set; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        isStopped = false;

        crearFlag = false;
        PlayerLv = 1;
    }

    // Update is called once per frame
    void Update()
    {
        // ゲームクリアか判定
        if (scoreManager.CrearCheck())
        {
            StartCoroutine(MoveGameCrear());
        }
    }

    IEnumerator MoveGameCrear()
    {
        crearFlag = true;

        yield return new WaitForSeconds(1.0f);

        SceneManager.LoadScene("GameCrearScene");
    }
 
}
