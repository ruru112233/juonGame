using System.Collections.Generic;
using UnityEngine;

public class EnemyShotPattern : EnemyMove
{
    private List<MonoBehaviour> shotScriptList = new List<MonoBehaviour>();
    // ----------------------
    public List<MonoBehaviour> ShotScriptList
    {
        get { return shotScriptList; }
    }
    // ----------------------

    // Start is called before the first frame update
    public virtual void Start()
    {
        InitializeScriptList();
    }

    // �X�N���v�g���X�g�̏�����
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
        // �S�ẴX�N���v�g�𖳌��ɂ���
        foreach (var script in shotScriptList)
        {
            script.enabled = false;
        }

        // �w�肳�ꂽ�X�N���v�g��L���ɂ���
        shotScriptList[index].enabled = true;
    }

    // ----------------------
    
    // Update is called once per frame
    public override void Update()
    {
        // --------------------

        // --------------------
    }

}
