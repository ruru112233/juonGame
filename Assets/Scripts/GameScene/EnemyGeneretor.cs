using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGeneretor : StageManager
{

    // Start is called before the first frame update
    public override void Start()
    {

        StartCoroutine(EnemyPattern1Stage());
    }


}
