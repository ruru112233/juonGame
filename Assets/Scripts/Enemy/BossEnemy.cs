using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : EnemyShotPattern
{
    const float RIGHT_MOVE_LIMIT = 1.5f;
    const float LEFT_MOVE_LIMIT = -1.5f;

    private bool rightMoveFlag = false;
    private float bossMoveSpeed = 1.0f;

    // ----------------------------
    private float shotChengeTime = 5.0f;
    private float currentTime = 0;
    private int currentShotIndex = 0;
    // ----------------------------


    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        rightMoveFlag = true;
        // ----------------------------
        ActiveScriptByIndex(currentShotIndex);
        // ----------------------------
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        // ----------------------------
        currentTime += Time.deltaTime;

        if (shotChengeTime <= currentTime)
        {
            UpdateScriptIndex();
            ActiveScriptByIndex(currentShotIndex);
            currentTime = 0;
        }
        // ----------------------------

        BossMove();
    }

    private void BossMove()
    {
        
        if (transform.position.x > RIGHT_MOVE_LIMIT)
        {
            // RIGHT_MOVE_LIMITより右に移動したら、rightMoveFlagをfalseにする。
            rightMoveFlag = false;
        }
        else if (transform.position.x < LEFT_MOVE_LIMIT)
        {
            // LEFT_MOVE_LIMITより左に移動したら、rightMoveFlagをtrueにする。
            rightMoveFlag = true;
        }
        else
        {
            // 何もしない
        }

        // 左右に移動させる。
        if (rightMoveFlag)
        {
            base.MoveDirection(MoveDirectionType.RIGHT);
        }
        else
        {
            base.MoveDirection(MoveDirectionType.LEFT);
        }
    }
    // ----------------------------
    protected void UpdateScriptIndex()
    {
        currentShotIndex = (currentShotIndex + 1) % ShotScriptList.Count;
    }
    // ----------------------------

}
