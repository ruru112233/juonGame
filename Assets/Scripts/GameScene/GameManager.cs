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

    public int PlayerLv { get; set; }

    public EnumData.EventSceneType eventSceneType;

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

        PlayerLv = 1;

        //if (joystick) joystick.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        // ÉQÅ[ÉÄÉNÉäÉAÇ©îªíË
        if (scoreManager && scoreManager.IsClear())
        {
            if(joystick) joystick.SetActive(false);
            // StartCoroutine(MoveGameCrear());
        }
    }

    IEnumerator MoveGameCrear()
    {
        yield return new WaitForSeconds(1.0f);

        SceneManager.LoadScene("GameCrearScene");
    }
 
}
