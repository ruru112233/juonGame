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

    private bool isClear = false; // ��x�N���A������Aupdate�֐��ŏ������Ȃ��悤�ɂ��邽�߂ɗp��

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
        // �Q�[���N���A������
        if (scoreManager && scoreManager.IsClear() && !isClear)
        {
            isClear = true;
            if (joystick) joystick.SetActive(false);
            StartCoroutine(ShowRankingPanel());
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
