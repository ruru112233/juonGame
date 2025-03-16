using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullMaxFlag : MonoBehaviour
{
    public bool FullMax { get; set; }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        FullMax = false;
    }
}
