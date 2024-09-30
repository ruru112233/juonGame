using System;
using UnityEngine;

public class ChengeVectol : MobChar
{
 
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        // 全方向にランダムで向かせる
        if (randomVectolFlag)
        {
            if (IsStateChenge())
            {
                RandomState();
            }
        }
        else if (upMoveFlag)
        {
            MovePatternChenge(MoveState.BACK);
        }
        else if (downMoveFlag)
        {
            MovePatternChenge(MoveState.FRONT);
        }
        else if (rightMoveFlag)
        {
            MovePatternChenge(MoveState.RIGHT);
        }
        else if (leftMoveFlag)
        {
            MovePatternChenge(MoveState.LEFT);
        }




    }
   

    

}
