using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossEnemy : EnemyShotPattern
{
    const float RIGHT_MOVE_LIMIT = 1.5f;
    const float LEFT_MOVE_LIMIT = -1.5f;

    private bool rightMoveFlag = false;
    //private float bossMoveSpeed = 1.0f;

    private float shotChengeTime = 5.0f;
    private float currentTime = 0;
    private int currentShotIndex = 0;

    private int bossHp_ = 0;

    private bool bossMoveFlag = false;

    public bool BossMoveFlag
    {
        get { return bossMoveFlag; }
        set { bossMoveFlag = value; }
    }

    UiManager uiManager; // UiManagerのコンポーネント格納用
    Slider bossHpSlider; // Sliderのコンポーネント格納用

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        rightMoveFlag = true;

        bossHp_ = 100;

        // UiManagerの取得
        uiManager = GameObject.FindGameObjectWithTag("UiManager").GetComponent<UiManager>();
        
        // BossHpSliderObjのSliderを取得
        bossHpSlider = uiManager.bossHpSliderObj.GetComponent<Slider>();
        
        // SliderのMaxValueに最大HPを代入
        bossHpSlider.maxValue = bossHp_;
        // SliderのValueに最大HPを代入
        bossHpSlider.value = bossHp_;

        // Sliderを表示
        bossHpSlider.gameObject.SetActive(true);

    }

    // Update is called once per frame
    public override void Update()
    {
        if (GameManager.instance.isStopped) return;

        base.Update();

        if (bossMoveFlag)
        {
            ActiveScriptByIndex(currentShotIndex);
            BossMove();
            currentTime += Time.deltaTime;

            if (shotChengeTime <= currentTime)
            {
                UpdateScriptIndex();
                ActiveScriptByIndex(currentShotIndex);
                currentTime = 0;
            }
        }
        else
        {
            base.MoveDirection(EnumData.MoveDirectionType.BOTTOM);
        }

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
            base.MoveDirection(EnumData.MoveDirectionType.RIGHT);
        }
        else
        {
            base.MoveDirection(EnumData.MoveDirectionType.LEFT);
        }
    }

    protected void UpdateScriptIndex()
    {
        currentShotIndex = (currentShotIndex + 1) % ShotScriptList.Count;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            if (!bossMoveFlag) return; // bossMoveFlagがfalseの時、ダメージを受けない 

            bossHp_--;

            bossHpSlider.value = bossHp_;

            if (bossHp_ <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

}
