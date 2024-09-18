using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingPlayer : Player
{
    private float moveSpeed = 2.0f;

    private Animator anime;
    private Vector3 defaultScale;

    private float x;
    private float y;

    private void Start()
    {
        anime = this.GetComponent<Animator>();
        defaultScale = this.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
    }

    protected override void PlayerMove()
    {
        base.PlayerMove();

        x = joystick.Horizontal;
        y = joystick.Vertical;
        Vector3 newPosition = this.transform.position;



        HorizontalPosSetting(x, ref newPosition);

        VerticalPosSetting(y, ref newPosition);
    }

    private void LateUpdate()
    {
        PlayerAnimation(x, y);
    }

    protected override void HorizontalPosSetting(float joyconX, ref Vector3 newPosition)
    {
        newPosition.x += joyconX * moveSpeed * Time.deltaTime;
    }

    protected override void VerticalPosSetting(float joyconY, ref Vector3 newPosition)
    {
        newPosition.y += joyconY * moveSpeed * Time.deltaTime;
    }

    private void PlayerAnimation(float x, float y)
    {
        if (0 < y && 0.5f < (y - x))
        {
            // è„
            anime.SetInteger("work_state", 3);
        }
        else if (0 > y && 0.5f > (y - x))
        {
            // â∫
            anime.SetInteger("work_state", 0);
        }
        else if (0 < x)
        {
            // âE
            anime.SetInteger("work_state", 1);
            
        }
        else if (0 > x)
        {
            // ç∂
            anime.SetInteger("work_state", 2);
        }
        
    }

}
