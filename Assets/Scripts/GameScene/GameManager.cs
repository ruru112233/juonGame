using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

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
        PlayerLv = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
