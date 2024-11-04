using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public static bool crearFlag = false;

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
        crearFlag = false;
        PlayerLv = 1;
    }

    // Update is called once per frame
    void Update()
    {
        // ÉQÅ[ÉÄÉNÉäÉAÇ©îªíË
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
