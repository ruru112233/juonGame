using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool isStopped = false;
    public int unlockCounter = 1;
    public ScoreManager scoreManager;
    public ItemStock itemStock;
    public Player player;
    public GameObject joystick;
    public TimeManager timeManager;
    public RankingManager rankingManager;

    public bool isEnding = false;

    public int PlayerLv { get; set; }

    public EnumData.EventSceneType eventSceneType;

    private bool isClear = false; // 一度クリアしたら、update関数で処理しないようにするために用意

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
        isClear = false;

        PlayerLv = 1;

        //if (joystick) joystick.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        // ゲームクリアか判定
        if (scoreManager && scoreManager.IsClear() && !isClear)
        {
            isClear = true;
            if (joystick) joystick.SetActive(false);
            if(rankingManager) rankingManager.gameObject.SetActive(true);
        }
    }

    IEnumerator MoveGameCrear()
    {
        yield return new WaitForSeconds(1.0f);

        SceneManager.LoadScene("GameCrearScene");
    }
 
}
