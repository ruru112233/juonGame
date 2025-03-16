using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
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
    public GameObject lastEndingPanel;

    public GameObject loadingPanel;

    public GameObject swipeObj;

    public bool isEnding = false;
    public bool isStart = false;

    private float startTime = 6.0f;
    private float latestTime = 0f;

    // アイテムがプレイヤーの方向に移動する
    private const float ITEM_MOVE_SEC = 15.0f;
    private float currentItemMoveSec = 0.0f;
    private bool isItemMove = false;

    public bool IsItemMove { get { return isItemMove; } set { isItemMove = value; } }
    public float CurrentItemMoveSec { get { return currentItemMoveSec; } set { currentItemMoveSec = value; } }

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

        if (loadingPanel) loadingPanel.SetActive(true);
        if (joystick) joystick.SetActive(false);
        if (lastEndingPanel) lastEndingPanel.SetActive(false);
        if (swipeObj) swipeObj.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isStart)
        {
            if (loadingPanel) loadingPanel.SetActive(false);
            if (joystick) joystick.SetActive(true);
            if (swipeObj) swipeObj.SetActive(true);
        }

        if (latestTime > startTime)
        {
            isStart = true;
        }
        else
        {
            latestTime += Time.deltaTime;
        }

        // ゲームクリアか判定
        if (scoreManager && scoreManager.IsClear() && !isClear)
        {
            isClear = true;
            if (joystick) joystick.SetActive(false);
            StartCoroutine(ShowRankingPanel());
        }

        // アイテム移動の管理
        if (isItemMove)
        {
            currentItemMoveSec += Time.deltaTime;
            if (ITEM_MOVE_SEC <= currentItemMoveSec)
            {
                currentItemMoveSec = 0;
                isItemMove = false;
            }
        }
    }

    private IEnumerator ShowRankingPanel()
    {
        yield return new WaitForSeconds(3.0f);
        if (rankingManager) rankingManager.gameObject.SetActive(true);
    }

    IEnumerator MoveGameCrear()
    {
        yield return new WaitForSeconds(1.0f);

        SceneManager.LoadScene("GameCrearScene");
    }
 
}
