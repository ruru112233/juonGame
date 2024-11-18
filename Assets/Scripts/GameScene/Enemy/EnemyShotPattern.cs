using System.Collections.Generic;
using UnityEngine;

public class EnemyShotPattern : EnemyMove
{
    private List<MonoBehaviour> shotScriptList = new List<MonoBehaviour>();

    public List<MonoBehaviour> ShotScriptList
    {
        get { return shotScriptList; }
    }

    // Start is called before the first frame update
    public override void Start()
    {
        //InitializeScriptList();
        base.Start();
    }

    // スクリプトリストの初期化
    private void InitializeScriptList()
    {
        shotScriptList.Add(GetComponent<CircleShoot>());
        shotScriptList.Add(GetComponent<PlayerTargetShot>());
        shotScriptList.Add(GetComponent<TornadoShoot>());
        shotScriptList.Add(GetComponent<WaveShoot>());

        foreach (var script in shotScriptList)
        {
            script.enabled = false;
        }
    }
    
    // ----------------------
    protected void ActiveScriptByIndex(int index)
    {
        // 全てのスクリプトを無効にする
        foreach (var script in shotScriptList)
        {
            script.enabled = false;
        }

        // 指定されたスクリプトを有効にする
        shotScriptList[index].enabled = true;
    }

    // ----------------------
    
    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }

}
