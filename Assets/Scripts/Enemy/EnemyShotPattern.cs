using System.Collections.Generic;
using UnityEngine;

public class EnemyShotPattern : EnemyMove
{
    private List<MonoBehaviour> shotScriptList = new List<MonoBehaviour>();
    private float shotChengeTime = 5.0f;
    private float currentTime = 0;
    private int currentShotIndex = 0;

    // Start is called before the first frame update
    public virtual void Start()
    {
        InitializeScriptList();
        ActiveScriptByIndex(currentShotIndex);
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

    private void ActiveScriptByIndex(int index)
    {
        // �S�ẴX�N���v�g�𖳌��ɂ���
        foreach (var script in shotScriptList)
        {
            script.enabled = false;
        }

        // �w�肳�ꂽ�X�N���v�g��L���ɂ���
        shotScriptList[index].enabled = true;
    }

    private void UpdateScriptIndex()
    {
        currentShotIndex = (currentShotIndex + 1) % shotScriptList.Count;
    }

    // Update is called once per frame
    public virtual void Update()
    {
        currentTime += Time.deltaTime;

        if (shotChengeTime <= currentTime)
        {
            UpdateScriptIndex();
            ActiveScriptByIndex(currentShotIndex);
            currentTime = 0;
        }

    }

}
